using BE;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DAL;
using System.Data;


namespace MPP
{
    public class MPPCliente
    {
        private readonly AccesoDAL acceso;

        public MPPCliente()
        {
            acceso = new AccesoDAL();
        }


        public bool GuardarCliente(BECliente cliente)
        {
            cliente.Codigo = Guid.NewGuid().ToString();

            var parametros = new MySqlParameter[]
            {
                new MySqlParameter("@codigo", cliente.Codigo),
                new MySqlParameter("@nombre", cliente.Nombre),
                new MySqlParameter("@direccion", cliente.Direccion),
                new MySqlParameter("@localidad", cliente.Localidad),
                new MySqlParameter("@telefono", cliente.Telefono),
                new MySqlParameter("@telefonoAlternativo", cliente.TelefonoAlternativo),
                new MySqlParameter("@apertura", cliente.HorarioDeApertura),
                new MySqlParameter("@cierre", cliente.HorarioDeCierre)
            };

            return acceso.EjecutarNonQuery(SQLQueryConstants.Cliente_Insertar, parametros) > 0;
        }

        public void GuardarCompraMayoristaTemporal(BECliente cliente, BECompraMayorista compraTemporal)
        {
            // Removed redundant AccesoDAL instantiation, use class member 'acceso'

            var parametrosEliminar = new MySqlParameter[]
            {
                new MySqlParameter("@id_cliente", cliente.Codigo)
            };
            acceso.EjecutarNonQuery(SQLQueryConstants.CompraTemporal_EliminarPorCliente, parametrosEliminar);

            foreach (var item in compraTemporal.ListaCarrito)
            {
                var parametrosInsertar = new MySqlParameter[]
                {
                    new MySqlParameter("@id_cliente", cliente.Codigo),
                    new MySqlParameter("@id_producto", item.Producto.Codigo),
                    new MySqlParameter("@cantidad", item.Cantidad),
                    new MySqlParameter("@precio_total", item.Total)
                };

                acceso.EjecutarNonQuery(SQLQueryConstants.CompraTemporal_Insertar, parametrosInsertar);
            }
        }

        public List<BECliente> ListarClientes()
        {
            List<BECliente> listaClientes = new List<BECliente>();

            DataTable tabla = acceso.EjecutarConsulta(SQLQueryConstants.Cliente_Listar);

            foreach (DataRow row in tabla.Rows)
            {
                BECliente cliente = new BECliente
                {
                    Codigo = row["codigo"].ToString(),
                    Nombre = row["nombre"].ToString(),
                    Direccion = row["direccion"].ToString(),
                    Localidad = row["localidad"].ToString(),
                    Telefono = row["telefono"].ToString(),
                    TelefonoAlternativo = row["telefono_alternativo"].ToString(),
                    HorarioDeApertura = TimeSpan.Parse(row["horario_de_apertura"].ToString()),
                    HorarioDeCierre = TimeSpan.Parse(row["horario_de_cierre"].ToString()),
                    Comentarios = row["comentarios"].ToString(),
                };

                cliente.CompraMayoristaTemp = ObtenerCompraTemporalDelCliente(cliente.Codigo);

                listaClientes.Add(cliente);
            }

            return listaClientes;
        }

        private BECompraMayorista ObtenerCompraTemporalDelCliente(string idCliente)
        {
            var parametros = new List<MySqlParameter>
            {
                new MySqlParameter("@idCliente", idCliente)
            };

            DataTable tabla = acceso.EjecutarConsulta(SQLQueryConstants.CompraTemporal_ObtenerPorCliente, parametros.ToArray());

            BECompraMayorista compra = new BECompraMayorista
            {
                Cliente = new BECliente { Codigo = idCliente }
            };

            foreach (DataRow row in tabla.Rows)
            {
                string stockId = row["stock_id"].ToString();
                string productoId = row["id_producto"].ToString();

                if (string.IsNullOrEmpty(stockId))
                {
                    // Combo product
                    BEProductoCombo productoCombo = ObtenerProductoComboPorId(productoId);
                    
                    BECarrito item = new BECarrito
                    {
                        Producto = productoCombo,
                        Cantidad = Convert.ToInt32(row["cantidad"]),
                        Total = Convert.ToDecimal(row["precio_total"])
                    };

                    compra.ListaCarrito.Add(item);
                }
                else
                {
                    // Individual product
                    BEStock stock = new BEStock
                    {
                        Codigo = row["stock_id"].ToString(),
                        Nombre = row["nombre"].ToString(),
                        Medida = Convert.ToDouble(row["medida"]),
                        TipoMedida = row["tipo_medida"].ToString(),
                        CantidadActual = Convert.ToInt32(row["cantidad_actual"]),
                        CantidadReservada = Convert.ToInt32(row["cantidad_reservada"])
                    };

                    BEProductoIndividual producto = new BEProductoIndividual
                    {
                        Tipo = row["tipo"].ToString(),
                        Codigo = row["id_producto"].ToString(),
                        Unidad = Convert.ToInt32(row["unidad"]),
                        PrecioMayorista = Convert.ToDecimal(row["precio_mayorista"]),
                        PrecioMinorista = Convert.ToDecimal(row["precio_minorista"]),
                        Stock = stock
                    };

                    BECarrito item = new BECarrito
                    {
                        Producto = producto,
                        Cantidad = Convert.ToInt32(row["cantidad"]),
                        Total = Convert.ToDecimal(row["precio_total"])
                    };

                    compra.ListaCarrito.Add(item);
                }
            }

            compra.Total = compra.ListaCarrito.Sum(c => c.Total);

            return compra;
        }

        public BEProductoCombo ObtenerProductoComboPorId(string idCombo)
        {
            var parametrosCombo = new List<MySqlParameter>
            {
                new MySqlParameter("@idCombo", idCombo)
            };

            DataTable tablaCombo = acceso.EjecutarConsulta(SQLQueryConstants.Producto_ObtenerPorId, parametrosCombo.ToArray());

            if (tablaCombo.Rows.Count == 0)
                return null;

            DataRow rowCombo = tablaCombo.Rows[0];

            BEProductoCombo combo = new BEProductoCombo
            {
                Codigo = rowCombo["id"].ToString(),
                Nombre = rowCombo["nombre"].ToString(),
                Unidad = Convert.ToInt32(rowCombo["unidad"]),
                PrecioMayorista = Convert.ToDecimal(rowCombo["precio_mayorista"]),
                PrecioMinorista = Convert.ToDecimal(rowCombo["precio_minorista"]),
                Tipo = rowCombo["tipo"].ToString(),
                ListaProductos = new List<BEStock>()
            };

            DataTable tablaProductos = acceso.EjecutarConsulta(SQLQueryConstants.ProductoStock_ObtenerPorProductoId, parametrosCombo.ToArray());

            foreach (DataRow row in tablaProductos.Rows)
            {
                int stockId = Convert.ToInt32(row["stock_id"]);
                int cantidad = Convert.ToInt32(row["cantidad"]);

                BEStock stock = ObtenerStockPorId(stockId);

                for (int i = 0; i < cantidad; i++)
                {
                    BEStock stockClonado = new BEStock
                    {
                        Codigo = stock.Codigo,
                        Nombre = stock.Nombre,
                        Medida = stock.Medida,
                        TipoMedida = stock.TipoMedida,
                        CantidadActual = stock.CantidadActual,
                        CantidadReservada = stock.CantidadReservada
                    };

                    combo.ListaProductos.Add(stockClonado);
                }
            }

            return combo;
        }

        public BEStock ObtenerStockPorId(int stockId)
        {
            var parametros = new List<MySqlParameter>
            {
                new MySqlParameter("@id", stockId)
            };

            DataTable tabla = acceso.EjecutarConsulta(SQLQueryConstants.Stock_ObtenerPorId, parametros.ToArray());

            if (tabla.Rows.Count == 0)
                throw new Exception($"No se encontró stock con ID: {stockId}");

            DataRow row = tabla.Rows[0];

            BEStock stock = new BEStock
            {
                Codigo = row["id"].ToString(),
                Nombre = row["nombre"].ToString(),
                Medida = Convert.ToDouble(row["medida"]),
                TipoMedida = row["tipo_medida"].ToString(),
                CantidadActual = Convert.ToInt32(row["cantidad_actual"]),
                CantidadReservada = Convert.ToInt32(row["cantidad_reservada"])
            };

            return stock;
        }

        public bool BorrarCliente(BECliente cliente)
        {
            var parametros = new MySqlParameter[]
            {
                new MySqlParameter("@codigo", cliente.Codigo)
            };

            return acceso.EjecutarNonQuery(SQLQueryConstants.Cliente_Eliminar, parametros) > 0;
        }

        public bool ModificarCliente(BECliente cliente)
        {
            var parametros = new MySqlParameter[]
            {
                new MySqlParameter("@nombre", cliente.Nombre),
                new MySqlParameter("@direccion", cliente.Direccion),
                new MySqlParameter("@localidad", cliente.Localidad),
                new MySqlParameter("@telefono", cliente.Telefono),
                new MySqlParameter("@telefonoAlternativo", cliente.TelefonoAlternativo),
                new MySqlParameter("@apertura", cliente.HorarioDeApertura),
                new MySqlParameter("@cierre", cliente.HorarioDeCierre),
                new MySqlParameter("@codigo", cliente.Codigo)
            };

            return acceso.EjecutarNonQuery(SQLQueryConstants.Cliente_Modificar, parametros) > 0;
        }

        public void AgregarModificarComentarios(BECliente cliente)
        {
            // Removed redundant AccesoDAL instantiation, use class member 'acceso'

            var parametros = new MySqlParameter[]
            {
                new MySqlParameter("@comentarios", cliente.Comentarios),
                new MySqlParameter("@codigo", cliente.Codigo)
            };

            int filasAfectadas = acceso.EjecutarNonQuery(SQLQueryConstants.Cliente_ActualizarComentarios, parametros);

            if (filasAfectadas == 0)
            {
                throw new Exception("No se encontró el cliente para modificar los comentarios.");
            }
        }
    }
}
