using BE;
using DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MPP
{

    public class MPPCompraMayorista
    {
        private readonly AccesoDAL acceso;

        public MPPCompraMayorista()
        {
            acceso = new AccesoDAL();
        }
        public void GuardarCompraMayorista(BECompraMayorista compraMayorista)
        {
            try
            {

                // 1. Insertar la compra mayorista y obtener el ID generado
                string queryCompra = @"INSERT INTO compra_mayorista (fecha, total, id_cliente)
                               VALUES (@fecha, @total, @id_cliente);
                               SELECT LAST_INSERT_ID();";

                List<MySqlParameter> parametrosCompra = new List<MySqlParameter>
        {
            new MySqlParameter("@fecha", compraMayorista.Fecha),
            new MySqlParameter("@total", compraMayorista.Total),
            new MySqlParameter("@id_cliente", compraMayorista.Cliente.Codigo)
        };

                int idCompra;
                using (MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString))
                {
                    conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(queryCompra, conexion))
                    {
                        comando.Parameters.AddRange(parametrosCompra.ToArray());
                        idCompra = Convert.ToInt32(comando.ExecuteScalar());
                    }

                    // 2. Insertar los detalles de la compra
                    foreach (BECarrito item in compraMayorista.ListaCarrito)
                    {
                        string queryDetalle = @"INSERT INTO detalle_compra (id_compra, id_producto, cantidad, total)
                                        VALUES (@id_compra, @id_producto, @cantidad, @total);";

                        List<MySqlParameter> parametrosDetalle = new List<MySqlParameter>
                {
                    new MySqlParameter("@id_compra", idCompra),
                    new MySqlParameter("@id_producto", item.Producto.Codigo),
                    new MySqlParameter("@cantidad", item.Cantidad),
                    new MySqlParameter("@total", item.Total)
                };

                        using (MySqlCommand comandoDetalle = new MySqlCommand(queryDetalle, conexion))
                        {
                            comandoDetalle.Parameters.AddRange(parametrosDetalle.ToArray());
                            comandoDetalle.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la compra mayorista.", ex);
            }
        }

        public BEProducto BuscarCompraMayorista(string codigo)
        {
            string query = @"SELECT p.codigo, p.unidad, p.precio_mayorista, p.precio_minorista,
                            s.codigo AS stock_codigo, s.nombre AS stock_nombre, 
                            s.medida, s.tipo_medida, s.cantidad_actual
                     FROM producto p
                     LEFT JOIN stock s ON p.codigo = s.codigo_producto
                     WHERE p.codigo = @codigo;";

            List<MySqlParameter> parametros = new List<MySqlParameter>
    {
        new MySqlParameter("@codigo", codigo)
    };

            DataTable dt = acceso.EjecutarConsulta(query, parametros.ToArray());

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            BEProductoIndividual producto = new BEProductoIndividual
            {
                Codigo = row["codigo"].ToString(),
                Unidad = Convert.ToInt32(row["unidad"]),
                PrecioMayorista = Convert.ToDecimal(row["precio_mayorista"]),
                PrecioMinorista = Convert.ToDecimal(row["precio_minorista"]),
                Stock = new BEStock
                {
                    Codigo = row["stock_codigo"].ToString(),
                    Nombre = row["stock_nombre"].ToString(),
                    Medida = Convert.ToDouble(row["medida"]),
                    TipoMedida = row["tipo_medida"].ToString(),
                    CantidadActual = Convert.ToInt32(row["cantidad_actual"])
                }
            };

            return producto;
        }

        private void ActualizarStockEnDB(BEStock s, MySqlConnection con, MySqlTransaction tra)
        {
            string sql = "UPDATE stock SET cantidad_actual = @cant, cantidad_reservada = @res WHERE id = @id;";
            using (var cmd = new MySqlCommand(sql, con, tra))
            {
                cmd.Parameters.AddWithValue("@cant", s.CantidadActual);
                cmd.Parameters.AddWithValue("@res", s.CantidadReservada);
                cmd.Parameters.AddWithValue("@id", s.Codigo);
                cmd.ExecuteNonQuery();
            }
        }

        public void GuardarCompraCompleta(BECompraMayorista compra)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
            {
                mySqlConnection.Open();
                using (MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        string cmdText = "INSERT INTO compra_mayorista (fecha, total, id_cliente) \r\n                                       VALUES (@fecha, @total, @id_cliente);\r\n                                       SELECT LAST_INSERT_ID();";
                        int num;
                        using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, mySqlConnection, mySqlTransaction))
                        {
                            mySqlCommand.Parameters.AddWithValue("@fecha", compra.Fecha);
                            mySqlCommand.Parameters.AddWithValue("@total", compra.Total);
                            mySqlCommand.Parameters.AddWithValue("@id_cliente", compra.Cliente.Codigo);
                            num = Convert.ToInt32(mySqlCommand.ExecuteScalar());
                        }
                        foreach (BECarrito becarrito in compra.ListaCarrito)
                        {
                            string cmdText2 = "INSERT INTO detalle_compra (id_compra, id_producto, cantidad, total) \r\n                                            VALUES (@id_compra, @id_producto, @cantidad, @total);";
                            using (MySqlCommand mySqlCommand2 = new MySqlCommand(cmdText2, mySqlConnection, mySqlTransaction))
                            {
                                mySqlCommand2.Parameters.AddWithValue("@id_compra", num);
                                mySqlCommand2.Parameters.AddWithValue("@id_producto", becarrito.Producto.Codigo);
                                mySqlCommand2.Parameters.AddWithValue("@cantidad", becarrito.Cantidad);
                                mySqlCommand2.Parameters.AddWithValue("@total", becarrito.Total);
                                mySqlCommand2.ExecuteNonQuery();
                            }
                            BEProductoIndividual beproductoIndividual = becarrito.Producto as BEProductoIndividual;
                            bool flag = beproductoIndividual != null;
                            if (flag)
                            {
                                string cmdText3 = "UPDATE stock SET cantidad_actual = @cantidad, cantidad_reservada = @reservada WHERE id = @id;";
                                using (MySqlCommand mySqlCommand3 = new MySqlCommand(cmdText3, mySqlConnection, mySqlTransaction))
                                {
                                    mySqlCommand3.Parameters.AddWithValue("@cantidad", beproductoIndividual.Stock.CantidadActual);
                                    mySqlCommand3.Parameters.AddWithValue("@reservada", beproductoIndividual.Stock.CantidadReservada);
                                    mySqlCommand3.Parameters.AddWithValue("@id", beproductoIndividual.Stock.Codigo);
                                    mySqlCommand3.ExecuteNonQuery();
                                }
                            }
                        }
                        mySqlTransaction.Commit();
                    }
                    catch (Exception innerException)
                    {
                        mySqlTransaction.Rollback();
                        throw new Exception("Error crítico al procesar la compra. Se revirtieron los cambios.", innerException);
                    }
                }
            }
        }

    }
}
