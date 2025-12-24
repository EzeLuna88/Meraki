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
    public class MPPCompraMinorista
    {

        private readonly AccesoDAL acceso;

        public MPPCompraMinorista()
        {
            acceso = new AccesoDAL();
        }

        public void GuardarCompraMinorista(BECompraMinorista compraMinorista)
        {
            try
            {
                // 1. Insertar la compra minorista y obtener el ID generado
                string queryCompra = @"INSERT INTO compra_minorista (codigo, fecha, total)
                               VALUES (@codigo, @fecha, @total);
                               SELECT LAST_INSERT_ID();";

                List<MySqlParameter> parametrosCompra = new List<MySqlParameter>
        {
            new MySqlParameter("@codigo", compraMinorista.Codigo),
            new MySqlParameter("@fecha", compraMinorista.Fecha),
            new MySqlParameter("@total", compraMinorista.Total)
        };

                // Ejecutar el query que retorna el último ID insertado
                DataTable resultado = acceso.EjecutarConsulta(queryCompra, parametrosCompra.ToArray());
                int idCompra = Convert.ToInt32(resultado.Rows[0][0]);

                // 2. Insertar los detalles de la compra
                foreach (BECarrito item in compraMinorista.ListaCarrito)
                {
                    string nombreProducto = item.Producto is BEProductoIndividual
                        ? item.Producto.ToString().Trim()
                        : ((BEProductoCombo)item.Producto).Nombre.Trim();

                    string queryDetalle = @"INSERT INTO detalle_compra_minorista (id_compra, nombre_producto, cantidad, monto)
                                    VALUES (@id_compra, @nombre_producto, @cantidad, @monto);";

                    List<MySqlParameter> parametrosDetalle = new List<MySqlParameter>
            {
                new MySqlParameter("@id_compra", idCompra),
                new MySqlParameter("@nombre_producto", nombreProducto),
                new MySqlParameter("@cantidad", item.Cantidad),
                new MySqlParameter("@monto", item.Total)
            };

                    acceso.EjecutarNonQueryConParametros(queryDetalle, parametrosDetalle);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la compra minorista.", ex);
            }
        }
    }
}
