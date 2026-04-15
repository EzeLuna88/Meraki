using BE;
using DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MPP
{
    public class MPPProductoCombo
    {

        private readonly AccesoDAL acceso;

        public MPPProductoCombo()
        {
            acceso = new AccesoDAL();
        }

        public void GuardarProducto(BEProductoCombo producto)
        {
            try
            {
                // 1. Insertar el producto combo en la tabla producto
                string queryProducto = @"
            INSERT INTO producto (id, tipo, nombre, unidad, precio_mayorista, precio_minorista)
            VALUES (@id, @tipo, @nombre, @unidad, @precio_mayorista, @precio_minorista);
        ";

                List<MySqlParameter> parametrosProducto = new List<MySqlParameter>
        {
            new MySqlParameter("@id", producto.Codigo),
            new MySqlParameter("@tipo", producto.Tipo),
            new MySqlParameter("@nombre", producto.Nombre),
            new MySqlParameter("@unidad", producto.Unidad),
            new MySqlParameter("@precio_mayorista", producto.PrecioMayorista),
            new MySqlParameter("@precio_minorista", producto.PrecioMinorista)
        };

                acceso.EjecutarNonQuery(queryProducto, parametrosProducto.ToArray());

                // 2. Insertar cada producto del combo en la tabla producto_combo
                foreach (var stock in producto.ListaProductos)
                {
                    string queryCombo = @"
                INSERT INTO producto_stock (producto_id, stock_id)
                VALUES (@producto_id, @stock_id);
            ";

                    List<MySqlParameter> parametrosCombo = new List<MySqlParameter>
            {
                new MySqlParameter("@producto_id", producto.Codigo),
                new MySqlParameter("@stock_id", stock.Codigo)
            };

                    acceso.EjecutarNonQuery(queryCombo, parametrosCombo.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el producto combo en la base de datos.", ex);
            }
        }

        public bool CodigoYaExiste(string codigo)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM producto WHERE id = @id;";
                MySqlParameter parametro = new MySqlParameter("@id", codigo);

                DataTable resultado = acceso.EjecutarConsulta(query, parametro);
                int cantidad = Convert.ToInt32(resultado.Rows[0][0]);

                return cantidad > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar si el código del producto ya existe.", ex);
            }
        }

        public void ModificarProducto(BEProductoCombo beProducto)
        {
            try
            {
                // Solo actualizamos los datos del combo en la tabla producto
                string queryProducto = @"UPDATE producto 
                                 SET unidad = @unidad, 
                                     nombre = @nombre,
                                     precio_mayorista = @precioMayorista,
                                     precio_minorista = @precioMinorista
                                 WHERE id = @id;";

                List<MySqlParameter> parametrosProducto = new List<MySqlParameter>
        {
            new MySqlParameter("@id", beProducto.Codigo),
            new MySqlParameter("@unidad", beProducto.Unidad),
            new MySqlParameter("@nombre", beProducto.Nombre),
            new MySqlParameter("@precioMayorista", beProducto.PrecioMayorista),
            new MySqlParameter("@precioMinorista", beProducto.PrecioMinorista)
        };

                acceso.EjecutarNonQuery(queryProducto, parametrosProducto.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el producto combo.", ex);
            }
        }

        public void BorrarProducto(BEProductoCombo beProducto)
        {
            try
            {
                // MAGIA DEL HISTORIAL:
                // No borramos las relaciones en la tabla producto_stock con un DELETE.
                // Al darle de baja (activo = 0) a la cabecera en la tabla 'producto', 
                // el combo desaparece del sistema para nuevas ventas, pero conserva 
                // sus ingredientes intactos para poder consultar comprobantes históricos.

                string queryEliminarCombo = @"UPDATE producto SET activo = 0 WHERE id = @id;";

                MySqlParameter param = new MySqlParameter("@id", beProducto.Codigo);

                acceso.EjecutarNonQuery(queryEliminarCombo, param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al borrar el producto combo.", ex);
            }
        }

        public bool ValidarStockEnComboActivo(string codigoStock)
        {
            bool estaEnComboActivo = false;

            // Consulta adaptada a tus tablas exactas
            // ps = producto_stock (el detalle)
            // p = producto (la cabecera del combo)
            string query = @"
        SELECT COUNT(*) 
        FROM producto_stock ps
        INNER JOIN producto p ON ps.producto_id = p.id
        WHERE ps.stock_id = @CodigoStock 
        AND p.activo = 1"; // <-- OJO: Recordá agregar la columna 'activo' a tu tabla producto en MySQL

            // Creamos el parámetro para MySQL
            MySqlParameter param = new MySqlParameter("@CodigoStock", codigoStock);
            var parametros = new List<MySqlParameter> { param };

            try
            {
                // Llamamos a tu DAL
                object resultado = acceso.EjecutarEscalar(query, parametros);

                // Si el COUNT da mayor a 0, significa que este stock está metido en un combo que sigue activo
                if (resultado != null && Convert.ToInt32(resultado) > 0)
                {
                    estaEnComboActivo = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el stock en la base de datos: " + ex.Message);
            }

            return estaEnComboActivo;
        }
    }
}
