using BE;
using DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MPP
{
    public class MPPProducto
    {

        private readonly AccesoDAL acceso;

        public MPPProducto()
        {
            acceso = new AccesoDAL();
        }

        public List<BEProducto> ListaProductos()
        {
            List<BEProducto> lista = new List<BEProducto>();
            string query = @"
            SELECT 
                p.id AS ProductoId, 
                p.stock_id AS StockIdProducto,
                p.tipo, 
                p.unidad, 
                p.precio_mayorista, 
                p.precio_minorista,
                p.nombre AS NombreProducto,
                s.id AS StockId,
                s.nombre, 
                s.medida, 
                s.tipo_medida, 
                s.cantidad_actual,
                ps.cantidad AS CantidadEnCombo,
                ps.stock_id AS StockIdCombo
                FROM producto p
                LEFT JOIN producto_stock ps ON p.id = ps.producto_id
                LEFT JOIN stock s ON 
                (p.tipo = 'individual' AND p.stock_id = s.id) OR 
                (p.tipo = 'combo' AND ps.stock_id = s.id)
                ORDER BY p.id;
    ";

            DataTable tabla = acceso.EjecutarConsulta(query);

            Dictionary<string, BEProductoCombo> combos = new Dictionary<string, BEProductoCombo>();

            foreach (DataRow row in tabla.Rows)
            {
                string productoId = row["ProductoId"].ToString();
                string tipo = row["tipo"].ToString();

                if (tipo == "individual")
                {
                    BEProductoIndividual prod = new BEProductoIndividual
                    {
                        Codigo = productoId.ToString(),
                        Unidad = Convert.ToInt32(row["unidad"]),
                        Tipo = tipo,
                        PrecioMayorista = Convert.ToDecimal(row["precio_mayorista"]),
                        PrecioMinorista = Convert.ToDecimal(row["precio_minorista"]),
                        Stock = new BEStock
                        {
                            Codigo = row["StockId"].ToString(),
                            Nombre = row["nombre"].ToString(),
                            Medida = Convert.ToDouble(row["medida"]),
                            TipoMedida = row["tipo_medida"].ToString(),
                            CantidadActual = Convert.ToInt32(row["cantidad_actual"])
                        }
                    };
                    lista.Add(prod);
                }
                else if (tipo == "combo")
                {
                    if (!combos.ContainsKey(productoId))
                    {
                        combos[productoId] = new BEProductoCombo
                        {
                            Codigo = productoId.ToString(),
                            Unidad = Convert.ToInt32(row["unidad"]),
                            Tipo = tipo,
                            PrecioMayorista = Convert.ToDecimal(row["precio_mayorista"]),
                            PrecioMinorista = Convert.ToDecimal(row["precio_minorista"]),
                            Nombre = row["NombreProducto"].ToString(),
                            ListaProductos = new List<BEStock>()
                        };
                    }

                    if (row["StockId"] != DBNull.Value)
                    {
                        int cantidadEnCombo = Convert.ToInt32(row["CantidadEnCombo"]);

                        for (int i = 0; i < cantidadEnCombo; i++)
                        {
                            combos[productoId].ListaProductos.Add(new BEStock
                            {
                                Codigo = row["StockId"].ToString(),
                                Nombre = row["nombre"].ToString(),
                                Medida = Convert.ToDouble(row["medida"]),
                                TipoMedida = row["tipo_medida"].ToString(),
                                CantidadActual = Convert.ToInt32(row["cantidad_actual"])
                            });
                        }
                    }
                }
            }

            lista.AddRange(combos.Values);
            return lista;
        }

        public void GuardarProducto(BEProductoIndividual producto)
        {
            try
            {
                // 1. Insertar en la tabla producto
                string insertProductoQuery = @"
            INSERT INTO producto (id, stock_id, tipo, unidad, precio_mayorista, precio_minorista)
            VALUES (@id, @stock_id, @tipo, @unidad, @precioMayorista, @precioMinorista);
            SELECT LAST_INSERT_ID();";

                List<MySqlParameter> parametrosProducto = new List<MySqlParameter>
        {

                    new MySqlParameter("@id", producto.Codigo),
                    new MySqlParameter("@stock_id", producto.Stock.Codigo),
                    new MySqlParameter("@tipo", producto.Tipo),
                    new MySqlParameter("@unidad", producto.Unidad),
                    new MySqlParameter("@precioMayorista", producto.PrecioMayorista),
                    new MySqlParameter("@precioMinorista", producto.PrecioMinorista)
        };

                // Ejecutamos la inserción y obtenemos el ID del producto insertado
                DataTable resultadoProducto = acceso.EjecutarConsulta(insertProductoQuery, parametrosProducto.ToArray());
                int productoId = Convert.ToInt32(resultadoProducto.Rows[0][0]);

               

                // 3. Insertar en la tabla producto_stock
                string updateProductoQuery = @"
                    UPDATE producto 
                    SET stock_id = @stockId 
                    WHERE id = @productoId;";

                List<MySqlParameter> parametrosUpdate = new List<MySqlParameter>
{
                    new MySqlParameter("@stockId", producto.Stock.Codigo),
                    new MySqlParameter("@productoId", productoId)
};

                acceso.EjecutarNonQueryConParametros(updateProductoQuery, parametrosUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el producto individual en base de datos", ex);
            }
        }

        public void BorrarProducto(BEProducto beProducto)
        {
            try
            {
                if (beProducto is BEProductoIndividual)
                {
                    string query = "DELETE FROM producto WHERE id = @id AND tipo = 'individual';";

                    var parametros = new List<MySqlParameter>
            {
                new MySqlParameter("@id", beProducto.Codigo)
            };

                    acceso.EjecutarNonQueryConParametros(query, parametros);
                }
                else if (beProducto is BEProductoCombo)
                {
                    // Primero se eliminan los vínculos del combo en la tabla producto_stock
                    string queryEliminarRelaciones = "DELETE FROM producto_stock WHERE producto_id = @id;";

                    var parametrosRelaciones = new List<MySqlParameter>
            {
                new MySqlParameter("@id", beProducto.Codigo)
            };

                    acceso.EjecutarNonQueryConParametros(queryEliminarRelaciones, parametrosRelaciones);

                    // Luego se elimina el producto combo
                    string queryEliminarCombo = "DELETE FROM producto WHERE id = @id AND tipo = 'combo';";

                    var parametrosCombo = new List<MySqlParameter>
            {
                new MySqlParameter("@id", beProducto.Codigo)
            };

                    acceso.EjecutarNonQueryConParametros(queryEliminarCombo, parametrosCombo);
                }
                else
                {
                    throw new Exception("Tipo de producto desconocido.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al borrar el producto de la base de datos.", ex);
            }
        }

        public void ModificarProducto(BEProducto beProducto)
        {
            try
            {
                string query = @"
            UPDATE producto
            SET unidad = @unidad,
                precio_mayorista = @precioMayorista,
                precio_minorista = @precioMinorista
            WHERE id = @id AND tipo = 'individual';
        ";

                List<MySqlParameter> parametros = new List<MySqlParameter>
        {
            new MySqlParameter("@unidad", beProducto.Unidad),
            new MySqlParameter("@precioMayorista", beProducto.PrecioMayorista),
            new MySqlParameter("@precioMinorista", beProducto.PrecioMinorista),
            new MySqlParameter("@id", beProducto.Codigo)
        };

                acceso.EjecutarNonQueryConParametros(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el producto en la base de datos.", ex);
            }
        }

        /*public BEProducto BuscarProducto(string codigo)
        {
            try
            {
                string query = @"
            SELECT 
                p.id AS ProductoId,
                p.tipo,
                p.unidad,
                p.precio_mayorista,
                p.precio_minorista,
                s.id AS StockId,
                s.nombre,
                s.medida,
                s.tipo_medida,
                s.cantidad_actual
            FROM producto p
            LEFT JOIN producto_stock ps ON p.id = ps.producto_id
            LEFT JOIN stock s ON ps.stock_id = s.id
            WHERE p.id = @codigo AND p.tipo = 'individual';
        ";

                List<MySqlParameter> parametros = new List<MySqlParameter>
        {
            new MySqlParameter("@codigo", codigo)
        };

                DataTable tabla = acceso.EjecutarConsulta(query, parametros.ToArray());

                if (tabla.Rows.Count == 0)
                    return null;

                DataRow row = tabla.Rows[0];

                return new BEProductoIndividual
                {
                    Codigo = row["ProductoId"].ToString(),
                    Tipo = row["tipo"].ToString(),
                    Unidad = Convert.ToInt32(row["unidad"]),
                    PrecioMayorista = Convert.ToDecimal(row["precio_mayorista"]),
                    PrecioMinorista = Convert.ToDecimal(row["precio_minorista"]),
                    Stock = new BEStock
                    {
                        Codigo = row["StockId"].ToString(),
                        Nombre = row["nombre"].ToString(),
                        Medida = Convert.ToDouble(row["medida"]),
                        TipoMedida = row["tipo_medida"].ToString(),
                        CantidadActual = Convert.ToInt32(row["cantidad_actual"])
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el producto en la base de datos.", ex);
            }
        }*/






    }
}
