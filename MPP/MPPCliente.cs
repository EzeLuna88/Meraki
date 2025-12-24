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

            string query = @"INSERT INTO clientes 
                            (codigo, nombre, direccion, localidad, telefono, telefono_alternativo, horario_de_apertura, horario_de_cierre) 
                            VALUES (@codigo, @nombre, @direccion, @localidad, @telefono, @telefonoAlternativo, @apertura, @cierre)";

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

            return acceso.EjecutarNonQuery(query, parametros) > 0;
        }

        public void GuardarCompraMayoristaTemporal(BECliente cliente, BECompraMayorista compraTemporal)
        {
            var acceso = new AccesoDAL();

            string consultaEliminar = "DELETE FROM compra_producto_temporal WHERE id_cliente = @id_cliente";
            var parametrosEliminar = new MySqlParameter[]
            {
        new MySqlParameter("@id_cliente", cliente.Codigo)
            };
            acceso.EjecutarNonQuery(consultaEliminar, parametrosEliminar);

            string consultaInsertar = @"INSERT INTO compra_producto_temporal 
                                (id_cliente, id_producto, cantidad, precio_total) 
                                VALUES (@id_cliente, @id_producto, @cantidad, @precio_total)";

            foreach (var item in compraTemporal.ListaCarrito)
            {
                var parametrosInsertar = new MySqlParameter[]
                {
            new MySqlParameter("@id_cliente", cliente.Codigo),
            new MySqlParameter("@id_producto", item.Producto.Codigo),
            new MySqlParameter("@cantidad", item.Cantidad),
            new MySqlParameter("@precio_total", item.Total)
                };

                acceso.EjecutarNonQuery(consultaInsertar, parametrosInsertar);
            }
        }

        public List<BECliente> ListarClientes()
        {
            List<BECliente> listaClientes = new List<BECliente>();

            string query = @"SELECT codigo, nombre, direccion, localidad, telefono, telefono_alternativo, 
                            horario_de_apertura, horario_de_cierre, comentarios
                     FROM clientes";

            DataTable tabla = acceso.EjecutarConsulta(query);

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
            string query = @"
        SELECT 
            cpt.id_producto,
            cpt.cantidad,
            cpt.precio_total,
            p.stock_id,
            p.unidad,
            p.precio_mayorista,
            p.precio_minorista,
            p.tipo,
            s.nombre,
            s.medida,
            s.tipo_medida,
            s.cantidad_actual,
            s.cantidad_reservada
        FROM compra_producto_temporal cpt
        JOIN producto p ON cpt.id_producto = p.id
        LEFT JOIN stock s ON p.stock_id = s.id
        WHERE cpt.id_cliente = @idCliente;";

            var parametros = new List<MySqlParameter>
    {
        new MySqlParameter("@idCliente", idCliente)
    };

            DataTable tabla = acceso.EjecutarConsulta(query, parametros.ToArray());

            /*if (tabla.Rows.Count == 0)
                return null;*/

            BECompraMayorista compra = new BECompraMayorista
            {
                Cliente = new BECliente { Codigo = idCliente }
            };

            foreach (DataRow row in tabla.Rows)
            {
                // Si el stock_id es null, significa que es un producto combo
                string stockId = row["stock_id"].ToString();
                string productoId = row["id_producto"].ToString();
                if (string.IsNullOrEmpty(stockId))
                {
                    // Es un producto combo
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
                    // Es un producto individual
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
            // 1. Obtener datos del producto combo
            string queryCombo = @"
        SELECT id, nombre, unidad, precio_mayorista, precio_minorista, tipo
        FROM producto
        WHERE id = @idCombo";

            var parametrosCombo = new List<MySqlParameter>
    {
        new MySqlParameter("@idCombo", idCombo)
    };

            DataTable tablaCombo = acceso.EjecutarConsulta(queryCombo, parametrosCombo.ToArray());

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

            // 2. Obtener los productos que componen el combo y sus cantidades
            string queryProductosDelCombo = @"
        SELECT stock_id, cantidad
        FROM producto_stock
        WHERE producto_id = @idCombo";

            DataTable tablaProductos = acceso.EjecutarConsulta(queryProductosDelCombo, parametrosCombo.ToArray());

            foreach (DataRow row in tablaProductos.Rows)
            {
                int stockId = Convert.ToInt32(row["stock_id"]);
                int cantidad = Convert.ToInt32(row["cantidad"]);

                BEStock stock = ObtenerStockPorId(stockId);

                // Repetir el producto las veces necesarias
                for (int i = 0; i < cantidad; i++)
                {
                    // Clonamos el stock para evitar referencias compartidas
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
            string query = @"
        SELECT id, nombre, medida, tipo_medida, cantidad_actual, cantidad_reservada
        FROM stock
        WHERE id = @id";

            var parametros = new List<MySqlParameter>
    {
        new MySqlParameter("@id", stockId)
    };

            DataTable tabla = acceso.EjecutarConsulta(query, parametros.ToArray());

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
            string query = "DELETE FROM clientes WHERE codigo = @codigo";

            var parametros = new MySqlParameter[]
            {
                new MySqlParameter("@codigo", cliente.Codigo)
            };

            return acceso.EjecutarNonQuery(query, parametros) > 0;
        }

        public bool ModificarCliente(BECliente cliente)
        {
            string query = @"UPDATE clientes 
                     SET nombre = @nombre, 
                         direccion = @direccion, 
                         localidad = @localidad, 
                         telefono = @telefono, 
                         telefono_alternativo = @telefonoAlternativo, 
                         horario_de_apertura = @apertura, 
                         horario_de_cierre = @cierre
                     WHERE codigo = @codigo";

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

            return acceso.EjecutarNonQuery(query, parametros) > 0;
        }

        public void AgregarModificarComentarios(BECliente cliente)
        {
            var acceso = new AccesoDAL();

            string consulta = @"UPDATE clientes 
                        SET comentarios = @comentarios 
                        WHERE codigo = @codigo";

            var parametros = new MySqlParameter[]
            {
        new MySqlParameter("@comentarios", cliente.Comentarios),
        new MySqlParameter("@codigo", cliente.Codigo)
            };

            int filasAfectadas = acceso.EjecutarNonQuery(consulta, parametros);

            if (filasAfectadas == 0)
            {
                throw new Exception("No se encontró el cliente para modificar los comentarios.");
            }
        }
    }
}
