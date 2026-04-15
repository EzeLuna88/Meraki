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
                WHERE p.activo = 1                
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
                        int cantidadEnCombo = row["CantidadEnCombo"] == DBNull.Value ? 1 : Convert.ToInt32(row["CantidadEnCombo"]);
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
            return lista.OrderBy(p => p is BEProductoIndividual ind ? ind.Stock.Nombre : ((BEProductoCombo)p).Nombre).ToList();
        }

        public void GuardarProducto(BEProductoIndividual producto)
        {
            try
            {
                string codigoFinal = producto.Codigo;
                bool codigoDisponible = false;
                char sufijo = 'A';

                // 1. Bucle para encontrar el primer código libre
                while (!codigoDisponible)
                {
                    string checkQuery = "SELECT COUNT(*) FROM producto WHERE id = @id";
                    List<MySqlParameter> pCheck = new List<MySqlParameter> { new MySqlParameter("@id", codigoFinal) };

                    // EjecutarConsulta devuelve un DataTable, vemos si el conteo es 0
                    DataTable dt = acceso.EjecutarConsulta(checkQuery, pCheck.ToArray());
                    int existe = Convert.ToInt32(dt.Rows[0][0]);

                    if (existe == 0)
                    {
                        codigoDisponible = true; // ¡Lo encontramos!
                    }
                    else
                    {
                        // Si el código existe (activo o no), probamos con la siguiente letra
                        // Si el código original era "1161", el primero será "1161-A", luego "1161-B"...
                        string baseCodigo = producto.Codigo.Contains("-") ? producto.Codigo.Split('-')[0] : producto.Codigo;
                        codigoFinal = $"{baseCodigo}-{sufijo}";
                        sufijo++; // Pasa de 'A' a 'B', de 'B' a 'C', etc.
                    }
                }

                // 2. Una vez que tenemos el codigoFinal único, hacemos el INSERT
                string insertQuery = @"
            INSERT INTO producto (id, stock_id, tipo, unidad, precio_mayorista, precio_minorista, activo)
            VALUES (@id, @stock_id, @tipo, @unidad, @precioMayorista, @precioMinorista, 1);";

                List<MySqlParameter> parametros = new List<MySqlParameter>
        {
            new MySqlParameter("@id", codigoFinal),
            new MySqlParameter("@stock_id", producto.Stock.Codigo),
            new MySqlParameter("@tipo", producto.Tipo),
            new MySqlParameter("@unidad", producto.Unidad),
            new MySqlParameter("@precioMayorista", producto.PrecioMayorista),
            new MySqlParameter("@precioMinorista", producto.PrecioMinorista)
        };

                acceso.EjecutarNonQuery(insertQuery, parametros.ToArray());

                // Opcional: Actualizar el objeto en memoria para que el usuario vea el código real asignado
                producto.Codigo = codigoFinal;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar o guardar el nuevo producto", ex);
            }
        }

        public void BorrarProducto(BEProducto beProducto)
        {
            try
            {
                if (beProducto is BEProductoIndividual)
                {
                    string query = "UPDATE producto  SET activo = 0 WHERE id = @id AND tipo = 'individual';";

                    var parametros = new List<MySqlParameter>
            {
                new MySqlParameter("@id", beProducto.Codigo)
            };

                    acceso.EjecutarNonQuery(query, parametros.ToArray());
                }
                else if (beProducto is BEProductoCombo)
                {
                    // Primero se eliminan los vínculos del combo en la tabla producto_stock
                    string queryEliminarRelaciones = "DELETE FROM producto_stock WHERE producto_id = @id;";

                    var parametrosRelaciones = new List<MySqlParameter>
            {
                new MySqlParameter("@id", beProducto.Codigo)
            };

                    acceso.EjecutarNonQuery(queryEliminarRelaciones, parametrosRelaciones.ToArray());

                    // Luego se elimina el producto combo
                    string queryEliminarCombo = "UPDATE producto SET activo = 0 WHERE id = @id AND tipo = 'combo';";

                    var parametrosCombo = new List<MySqlParameter>
            {
                new MySqlParameter("@id", beProducto.Codigo)
            };

                    acceso.EjecutarNonQuery(queryEliminarCombo, parametrosCombo.ToArray());
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

                acceso.EjecutarNonQuery(query, parametros.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el producto en la base de datos.", ex);
            }
        }








    }
}
