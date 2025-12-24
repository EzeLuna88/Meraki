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

                acceso.EjecutarNonQueryConParametros(queryProducto, parametrosProducto);

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

                    acceso.EjecutarNonQueryConParametros(queryCombo, parametrosCombo);
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

                acceso.EjecutarNonQueryConParametros(queryProducto, parametrosProducto);
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
                // 1. Eliminar componentes del combo en combo_producto
                string queryEliminarComponentes = @"DELETE FROM combo_producto WHERE id_combo = @id_combo;";
                MySqlParameter param1 = new MySqlParameter("@id_combo", beProducto.Codigo);
                acceso.EjecutarNonQuery(queryEliminarComponentes, param1);

                // 2. Eliminar el combo de la tabla producto
                string queryEliminarCombo = @"DELETE FROM producto WHERE id = @id;";
                MySqlParameter param2 = new MySqlParameter("@id", beProducto.Codigo);
                acceso.EjecutarNonQuery(queryEliminarCombo, param2);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al borrar el producto combo.", ex);
            }
        }
    }
}
