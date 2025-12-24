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
    public class MPPStock
    {
        public List<BEStock> CargarStock()
{
            try
            {
                string consulta = "SELECT id, nombre, medida, tipo_medida, cantidad_actual, cantidad_reservada, aviso FROM stock";

                AccesoDAL acceso = new AccesoDAL();
                DataTable tabla = acceso.EjecutarConsulta(consulta);

                List<BEStock> listaStock = new List<BEStock>();

                foreach (DataRow fila in tabla.Rows)
                {
                    BEStock stock = new BEStock
                    {
                        Codigo = fila["id"].ToString(),
                        Nombre = fila["nombre"].ToString(),
                        Medida = Convert.ToDouble(fila["medida"]),
                        TipoMedida = fila["tipo_medida"].ToString(),
                        CantidadActual = Convert.ToInt32(fila["cantidad_actual"]),
                        CantidadReservada = Convert.ToInt32(fila["cantidad_reservada"]),
                        AvisoCantidadStock = fila.IsNull("aviso") ? 0 : Convert.ToInt32(fila["aviso"])
                    };

                    listaStock.Add(stock);
                }

                return listaStock;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar el stock desde la base de datos.", ex);
            }
        }

        public void BorrarProductoDeStock(BEStock beStock)
        {
            try
            {
                string consulta = "DELETE FROM stock WHERE id = @id";

                AccesoDAL acceso = new AccesoDAL();
                MySqlParameter parametro = new MySqlParameter("@id", beStock.Codigo);

                int filasAfectadas = acceso.EjecutarNonQuery(consulta, parametro);

                if (filasAfectadas == 0)
                {
                    throw new Exception("No se encontró el producto con el ID especificado.");
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1451) // clave foránea violada
                {
                    throw new Exception("No se puede borrar el producto porque está asociado a un producto u otro registro.");
                }
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al borrar el producto del stock: " + ex.Message, ex);
            }
        }

        public void AgregarStock(BEStock beStock, int unidades)
        {
            try
            {
                string consulta = @"
            UPDATE stock 
            SET cantidad_actual = cantidad_actual + @unidades 
            WHERE id = @id";

                AccesoDAL acceso = new AccesoDAL();
                MySqlParameter[] parametros =
                {
            new MySqlParameter("@unidades", unidades),
            new MySqlParameter("@id", beStock.Codigo)
        };

                int filasAfectadas = acceso.EjecutarNonQuery(consulta, parametros);

                if (filasAfectadas == 0)
                {
                    throw new Exception("No se encontró el producto con el ID especificado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar stock en la base de datos.", ex);
            }
        }

        public bool ComprobarRepetido(BEStock beStock)
        {
            try
            {
                string consulta = @"
            SELECT COUNT(*) 
            FROM stock 
            WHERE nombre = @nombre AND medida = @medida";

                AccesoDAL acceso = new AccesoDAL();
                MySqlParameter[] parametros =
                {
            new MySqlParameter("@nombre", beStock.Nombre.Trim()),
            new MySqlParameter("@medida", beStock.Medida)
        };

                DataTable resultado = acceso.EjecutarConsulta(consulta, parametros);

                int cantidad = Convert.ToInt32(resultado.Rows[0][0]);

                return cantidad > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al comprobar si el producto ya existe en la base de datos.", ex);
            }
        }


        public int GuardarNuevoProducto(BEStock beStock)
        {
            try
            {
                string consulta = @"
            INSERT INTO stock (nombre, medida, tipo_medida, cantidad_actual, cantidad_reservada)
            VALUES (@nombre, @medida, @tipo_medida, @cantidad_actual, @cantidad_reservada);
            SELECT LAST_INSERT_ID();";

                var parametros = new List<MySqlParameter>()
        {
            new MySqlParameter("@nombre", beStock.Nombre.Trim()),
            new MySqlParameter("@medida", beStock.Medida),
            new MySqlParameter("@tipo_medida", beStock.TipoMedida.Trim()),
            new MySqlParameter("@cantidad_actual", beStock.CantidadActual),
            new MySqlParameter("@cantidad_reservada", beStock.CantidadReservada)
        };

                AccesoDAL acceso = new AccesoDAL();

                object resultado = acceso.EjecutarEscalarConParametros(consulta, parametros);

                return Convert.ToInt32(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el nuevo producto en la base de datos.", ex);
            }
        }

        public void ActualizarStock(List<BEStock> listaStock)
        {
            try
            {
                AccesoDAL acceso = new AccesoDAL();

                foreach (BEStock beStock in listaStock)
                {
                    string consulta = @"
                UPDATE stock
                SET cantidad_actual = @cantidad_actual
                WHERE id = @id";

                    MySqlParameter[] parametros =
                    {
                new MySqlParameter("@cantidad_actual", beStock.CantidadActual),
                new MySqlParameter("@id", int.Parse(beStock.Codigo))
            };

                    acceso.EjecutarNonQuery(consulta, parametros);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el stock en la base de datos.", ex);
            }
        }

        public void AcomodarCantidadReservadaStock()
        {
            try
            {
                AccesoDAL acceso = new AccesoDAL();

                string consulta = @"
            UPDATE stock
            SET cantidad_reservada = 0";

                acceso.EjecutarNonQuery(consulta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al reiniciar la cantidad reservada en stock.", ex);
            }
        }


        public void ModificarStock(BEStock beStock)
        {
            try
            {
                AccesoDAL acceso = new AccesoDAL();

                string consulta = @"
            UPDATE stock
            SET nombre = @nombre,
                medida = @medida,
                tipo_medida = @tipo_medida
            WHERE id = @codigo;";

                List<MySqlParameter> parametros = new List<MySqlParameter>
        {
            new MySqlParameter("@nombre", beStock.Nombre),
            new MySqlParameter("@medida", beStock.Medida),
            new MySqlParameter("@tipo_medida", beStock.TipoMedida),
            new MySqlParameter("@codigo", beStock.Codigo)
        };

                acceso.EjecutarNonQueryConParametros(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el stock.", ex);
            }
        }

        public void CantidadReservadaStock(BEStock beStock)
        {
            try
            {
                AccesoDAL acceso = new AccesoDAL();

                string consulta = @"
            UPDATE stock
            SET cantidad_reservada = @cantidadReservada
            WHERE id = @codigo;";

                List<MySqlParameter> parametros = new List<MySqlParameter>
        {
            new MySqlParameter("@cantidadReservada", beStock.CantidadReservada),
            new MySqlParameter("@codigo", beStock.Codigo)
        };

                acceso.EjecutarNonQueryConParametros(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la cantidad reservada del producto.", ex);
            }
        }

        public void CargarFechaDeVencimiento(BEStock beStock, DateTime fechaDeVencimiento, int totalUnidades)
        {
            try
            {
                AccesoDAL acceso = new AccesoDAL();

                // Primero, verificamos si ya existe el registro
                string querySelect = @"SELECT cantidad FROM stock_vencimiento 
                               WHERE stock_id = @stock_id AND fecha_de_vencimiento = @fecha_de_vencimiento;";

                List<MySqlParameter> parametrosSelect = new List<MySqlParameter>
        {
            new MySqlParameter("@stock_id", beStock.Codigo),
            new MySqlParameter("@fecha_de_vencimiento", fechaDeVencimiento)
        };

                object resultado = acceso.EjecutarEscalarConParametros(querySelect, parametrosSelect);

                if (resultado != null && resultado != DBNull.Value)
                {
                    // Ya existe, actualizamos sumando la cantidad
                    int cantidadExistente = Convert.ToInt32(resultado);
                    int nuevaCantidad = cantidadExistente + totalUnidades;

                    string queryUpdate = @"UPDATE stock_vencimiento 
                                   SET cantidad = @cantidad 
                                   WHERE stock_id = @stock_id AND fecha_de_vencimiento = @fecha_de_vencimiento;";

                    List<MySqlParameter> parametrosUpdate = new List<MySqlParameter>
            {
                new MySqlParameter("@cantidad", nuevaCantidad),
                new MySqlParameter("@stock_id", beStock.Codigo),
                new MySqlParameter("@fecha_de_vencimiento", fechaDeVencimiento)
            };

                    acceso.EjecutarNonQueryConParametros(queryUpdate, parametrosUpdate);
                }
                else
                {
                    // No existe, insertamos
                    string queryInsert = @"INSERT INTO stock_vencimiento (stock_id, fecha_de_vencimiento, cantidad)
                                   VALUES (@stock_id, @fecha_de_vencimiento, @cantidad);";

                    List<MySqlParameter> parametrosInsert = new List<MySqlParameter>
            {
                new MySqlParameter("@stock_id", beStock.Codigo),
                new MySqlParameter("@fecha_de_vencimiento", fechaDeVencimiento),
                new MySqlParameter("@cantidad", totalUnidades)
            };

                    acceso.EjecutarNonQueryConParametros(queryInsert, parametrosInsert);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la fecha de vencimiento para el stock.", ex);
            }
        }

        public void DescontarStockPorVencimiento(BECompraMayorista compra)
        {
            try
            {
                AccesoDAL acceso = new AccesoDAL();

                foreach (var itemCarrito in compra.ListaCarrito)
                {
                    var producto = itemCarrito.Producto;
                    int cantidadVendida = itemCarrito.Cantidad;

                    if (producto is BEProductoIndividual individual)
                    {
                        // Se multiplica por la cantidad de unidades que representa cada producto
                        int totalUnidades = cantidadVendida * individual.Unidad;
                        DescontarStockDeProducto(acceso, individual.Stock.Codigo, totalUnidades);
                    }
                    else if (producto is BEProductoCombo combo)
                    {
                        foreach (var stock in combo.ListaProductos)
                        {
                            // Se asume que cada combo lleva 1 unidad de cada componente (ajustar si varía)
                            int totalUnidades = cantidadVendida;
                            DescontarStockDeProducto(acceso, stock.Codigo, totalUnidades);
                        }
                    }
                }

                string queryDelete = @"
    DELETE FROM stock_vencimiento
    WHERE cantidad = 0";

                acceso.EjecutarNonQuery(queryDelete);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al descontar stock por vencimiento.", ex);
            }
        }

        private void DescontarStockDeProducto(AccesoDAL acceso, string stockId, int cantidadADescontar)
        {
            string query = @"
        SELECT id, cantidad, fecha_de_vencimiento
        FROM stock_vencimiento
        WHERE stock_id = @stockId AND cantidad > 0
        ORDER BY fecha_de_vencimiento ASC";

            List<MySqlParameter> parametros = new List<MySqlParameter>
    {
        new MySqlParameter("@stockId", stockId)
    };

            DataTable tabla = acceso.EjecutarConsulta(query, parametros.ToArray());

            if (tabla.Rows.Count == 0)
            {
                // Si no hay stock con fecha de vencimiento, salteamos silenciosamente
                return;
            }

            foreach (DataRow fila in tabla.Rows)
            {
                int id = Convert.ToInt32(fila["id"]);
                int cantidadDisponible = Convert.ToInt32(fila["cantidad"]);

                int cantidadADescontarAhora = Math.Min(cantidadADescontar, cantidadDisponible);

                string update = "UPDATE stock_vencimiento SET cantidad = cantidad - @cant WHERE id = @id";
                List<MySqlParameter> parametrosUpdate = new List<MySqlParameter>
        {
            new MySqlParameter("@cant", cantidadADescontarAhora),
            new MySqlParameter("@id", id)
        };

                acceso.EjecutarNonQuery(update, parametrosUpdate.ToArray());

                cantidadADescontar -= cantidadADescontarAhora;

                if (cantidadADescontar <= 0)
                    break;
            }

            // Si todavía queda algo por descontar, lo ignoramos y no lanzamos excepción
        }

        public DataTable ObtenerStockConVencimiento()
        {
            try
            {
                AccesoDAL acceso = new AccesoDAL();

                string query = @"
            SELECT 
                s.id AS StockID,
                s.nombre AS NombreProducto,
                s.tipo_medida AS TipoMedida,
                s.medida AS Medida,
                sv.id AS StockVencimientoID,
                sv.cantidad AS CantidadDisponible,
                sv.fecha_de_vencimiento AS FechaVencimiento
            FROM 
                stock s
            JOIN 
                stock_vencimiento sv ON s.id = sv.stock_id
            ORDER BY 
                sv.fecha_de_vencimiento ASC";

                return acceso.EjecutarConsulta(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el stock con vencimientos", ex);
            }
        }

        public void CantidadAviso(BEStock beStock)
        {
            try
            {
                AccesoDAL acceso = new AccesoDAL();

                string query = @"
            UPDATE stock 
            SET aviso = @aviso 
            WHERE id = @id";

                List<MySqlParameter> parametros = new List<MySqlParameter>
        {
            new MySqlParameter("@aviso", beStock.AvisoCantidadStock),
            new MySqlParameter("@id", beStock.Codigo)
        };

                acceso.EjecutarNonQueryConParametros(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar los días de aviso.", ex);
            }
        }

        public List<string> ObtenerProductosProximosAVencer()
        {
            int diasAviso;

            string queryDiasAviso = "SELECT valor FROM configuracion WHERE clave = 'dias_aviso_vencimiento'";
            AccesoDAL acceso = new AccesoDAL();
            DataTable tabla = acceso.EjecutarConsulta(queryDiasAviso);
            if (tabla.Rows.Count > 0)
            {
                diasAviso = Convert.ToInt32(tabla.Rows[0]["valor"]);
            }
            else
            {
                string query2 = "INSERT INTO configuracion(clave, valor) VALUES('dias_aviso_vencimiento', '7')";
                acceso.EjecutarNonQuery(query2);
                diasAviso = 7; // valor por defecto si no está configurado
            }

            string query = @"
        SELECT s.nombre, s.medida, s.tipo_medida, sv.fecha_de_vencimiento
        FROM stock_vencimiento sv
        INNER JOIN stock s ON sv.stock_id = s.id
        WHERE sv.cantidad > 0 AND DATEDIFF(sv.fecha_de_vencimiento, CURDATE()) <= @diasAviso
        AND DATEDIFF(sv.fecha_de_vencimiento, CURDATE()) >= 0
        ORDER BY sv.fecha_de_vencimiento ASC";

            List<MySqlParameter> parametros = new List<MySqlParameter>
    {
        new MySqlParameter("@diasAviso", diasAviso)
    };

            DataTable tablaStock = acceso.EjecutarConsulta(query, parametros.ToArray());

            List<string> productos = new List<string>();
            foreach (DataRow fila in tablaStock.Rows)
            {
                string producto = $"{fila["nombre"]} {fila["medida"]} {fila["tipo_medida"]} - vence el {Convert.ToDateTime(fila["fecha_de_vencimiento"]).ToShortDateString()}";
                productos.Add(producto);
            }

            return productos;
        }

        public List<string> ObtenerProductosConStockBajo()
        {
            string query = @"
        SELECT nombre, medida, tipo_medida, cantidad_actual, aviso
        FROM stock
        WHERE cantidad_actual <= aviso";

            AccesoDAL acceso = new AccesoDAL();
            DataTable tabla = acceso.EjecutarConsulta(query);

            List<string> productos = new List<string>();
            foreach (DataRow fila in tabla.Rows)
            {
                string producto = $"{fila["nombre"]} {fila["medida"]} {fila["tipo_medida"]} - stock actual: {fila["cantidad_actual"]}";
                productos.Add(producto);
            }

            return productos;
        }
    }
}
