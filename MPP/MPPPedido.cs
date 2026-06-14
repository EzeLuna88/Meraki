using BE;
using DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP
{
    public class MPPPedido
    {
        private readonly AccesoDAL acceso;

        public MPPPedido()
        {
            acceso = new AccesoDAL();
        }

        
        public bool GuardarPedido(BEPedido pedido)
        {
            try
            {
                
                string queryPedido = @"INSERT INTO pedidos 
                                     (numero, fecha, id_cliente, total, pago_efectivo, fecha_envio, estado) 
                                     VALUES 
                                     (@numero, @fecha, @id_cliente, @total, @pago_efectivo, @fecha_envio, @estado)";

                
                MySqlParameter[] parametrosPedido = new MySqlParameter[]
                {
                    new MySqlParameter("@numero", pedido.Numero),
                    new MySqlParameter("@fecha", pedido.Fecha),
                    new MySqlParameter("@id_cliente", pedido.Cliente.Codigo),
                    new MySqlParameter("@total", pedido.Total),
                    new MySqlParameter("@pago_efectivo", pedido.PagoEfectivo ? 1 : 0),
                    new MySqlParameter("@fecha_envio", pedido.FechaEnvio),
                    new MySqlParameter("@estado", (int)pedido.Estado) 
                };

                
                acceso.EjecutarNonQuery(queryPedido, parametrosPedido);

                
                string queryDetalle = @"INSERT INTO pedidos_productos 
                                      (id_pedido, id_producto, cantidad, precio_unitario, precio_total, nombre_producto) 
                                      VALUES 
                                      (@id_pedido, @id_producto, @cantidad, @precio_unitario, @precio_total, @nombre_producto)";

                // Recorremos el carrito del pedido
                foreach (BEItem item in pedido.ListaItems)
                {
                    MySqlParameter[] parametrosDetalle = new MySqlParameter[]
                    {
                        new MySqlParameter("@id_pedido", pedido.Numero),
                        new MySqlParameter("@id_producto", item.Codigo),
                        new MySqlParameter("@cantidad", item.Cantidad),
                        new MySqlParameter("@precio_unitario", item.PrecioUnitario),
                        new MySqlParameter("@precio_total", item.Subtotal),
                        new MySqlParameter("@nombre_producto", item.Nombre)
                    };

                    // Ejecutamos por cada producto en la lista
                    acceso.EjecutarNonQuery(queryDetalle, parametrosDetalle);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el pedido en BD: " + ex.Message);
            }
        }

        public string ObtenerProximoNumeroPedido()
        {
            try
            {
                // Le decimos a MySQL: "Agarrá el texto desde la 3ra letra (después del 'P-'),
                // convertilo a número entero (UNSIGNED) y dame el más alto (MAX)"
                string query = "SELECT MAX(CAST(SUBSTRING(numero, 3) AS UNSIGNED)) FROM pedidos";

                // Ejecutamos usando tu método escalar (devuelve un solo valor)
                object resultado = acceso.EjecutarEscalar(query);

                int proximoNumero = 1; // Arrancamos en 1 por si la tabla está vacía

                // Si ya hay pedidos guardados en la tabla, le sumamos 1 al máximo
                if (resultado != null && resultado != DBNull.Value)
                {
                    proximoNumero = Convert.ToInt32(resultado) + 1;
                }

                // Lo volvemos a armar como texto con la P- y los ceros a la izquierda
                return "P-" + proximoNumero.ToString().PadLeft(8, '0');
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el número de pedido: " + ex.Message);
            }
        }

        public List<BEPedido> ListarPedidosPorEstado(int estado)
        {
            List<BEPedido> listaPedidos = new List<BEPedido>();
            try
            {
                // El JOIN une el pedido con el cliente para tener el nombre a mano
                // IMPORTANTE: Ajustá "clientes" y "id_cliente" a los nombres reales de tu tabla de clientes si se llaman distinto
                string query = @"SELECT p.numero, p.fecha, p.total, p.fecha_envio, p.estado, 
                                p.id_cliente, c.nombre as nombre_cliente
                         FROM pedidos p
                         INNER JOIN clientes c ON p.id_cliente = c.codigo 
                         WHERE p.estado = @estado";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
            new MySqlParameter("@estado", estado)
                };

                DataTable dt = acceso.EjecutarConsulta(query, parametros);

                foreach (DataRow fila in dt.Rows)
                {
                    BEPedido pedido = new BEPedido();
                    pedido.Numero = fila["numero"].ToString();
                    pedido.Fecha = Convert.ToDateTime(fila["fecha"]);
                    pedido.Total = Convert.ToDecimal(fila["total"]);
                    pedido.FechaEnvio = Convert.ToDateTime(fila["fecha_envio"]);
                    pedido.Estado = (EstadoPedido)Convert.ToInt32(fila["estado"]);

                    // Armamos el cliente con los datos que trajimos
                    pedido.Cliente = new BECliente();
                    pedido.Cliente.Codigo = fila["id_cliente"].ToString();
                    pedido.Cliente.Nombre = fila["nombre_cliente"].ToString();

                    listaPedidos.Add(pedido);
                }

                return listaPedidos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BD al listar pedidos: " + ex.Message);
            }
        }

        public BEPedido ObtenerPedidoCompleto(string numeroPedido)
        {
            BEPedido pedido = new BEPedido();
            try
            {
                // 1. Buscamos la cabecera (Agregamos TODO lo del cliente al SELECT)
                string queryCabecera = @"SELECT p.numero, p.fecha, p.total, p.fecha_envio, p.estado,
                                        p.id_cliente, c.nombre, c.direccion, c.localidad,
                                        c.telefono, c.telefono_alternativo, c.horario_de_apertura, 
                                        c.horario_de_cierre, c.comentarios
                                 FROM pedidos p
                                 INNER JOIN clientes c ON p.id_cliente = c.codigo
                                 WHERE p.numero = @numero";

                MySqlParameter[] param = { new MySqlParameter("@numero", numeroPedido) };
                DataTable dtCabecera = acceso.EjecutarConsulta(queryCabecera, param);

                if (dtCabecera.Rows.Count > 0)
                {
                    DataRow fila = dtCabecera.Rows[0];
                    pedido.Numero = fila["numero"].ToString();
                    pedido.Fecha = Convert.ToDateTime(fila["fecha"]);
                    pedido.Total = Convert.ToDecimal(fila["total"]);
                    pedido.FechaEnvio = Convert.ToDateTime(fila["fecha_envio"]);
                    pedido.Estado = (EstadoPedido)Convert.ToInt32(fila["estado"]);

                    // 🛠️ ¡LA CORRECCIÓN!: Llenamos el cliente con todos los chiches
                    pedido.Cliente = new BECliente
                    {
                        Codigo = fila["id_cliente"].ToString(),
                        Nombre = fila["nombre"].ToString(),
                        Direccion = fila["direccion"].ToString(),
                        Localidad = fila["localidad"].ToString(),
                        Telefono = fila["telefono"].ToString(),
                        TelefonoAlternativo = fila["telefono_alternativo"].ToString(),
                        HorarioDeApertura = fila["horario_de_apertura"] != DBNull.Value ? TimeSpan.Parse(fila["horario_de_apertura"].ToString()) : TimeSpan.Zero,
                        HorarioDeCierre = fila["horario_de_cierre"] != DBNull.Value ? TimeSpan.Parse(fila["horario_de_cierre"].ToString()) : TimeSpan.Zero,
                        Comentarios = fila["comentarios"].ToString()
                    };
                }

                // 2. Buscamos el detalle (Esto ya lo habíamos arreglado antes, queda igual)
                string queryDetalle = @"SELECT id_producto, cantidad, precio_unitario, precio_total, nombre_producto
                                FROM pedidos_productos
                                WHERE id_pedido = @numero";

                DataTable dtDetalle = acceso.EjecutarConsulta(queryDetalle, param);
                pedido.ListaItems = new List<BEItem>();

                foreach (DataRow fila in dtDetalle.Rows)
                {
                    BEItem item = new BEItem
                    {
                        Codigo = fila["id_producto"].ToString(),
                        Cantidad = Convert.ToInt32(fila["cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(fila["precio_unitario"]),
                        Subtotal = Convert.ToDecimal(fila["precio_total"]),
                        Nombre = fila["nombre_producto"].ToString()
                    };
                    pedido.ListaItems.Add(item);
                }

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el pedido: " + ex.Message);
            }
        }

        public void ActualizarPedidoEditado(BEPedido pedido)
        {
            try
            {
                // 1. Actualizar el total y fecha en la tabla pedidos
                string queryUpdate = @"UPDATE pedidos 
                       SET total = @total, fecha_envio = @fecha_envio
                       WHERE numero = @numero";
                List<MySqlParameter> paramUpdate = new List<MySqlParameter>
        {
            new MySqlParameter("@total", pedido.Total),
            new MySqlParameter("@fecha_envio", pedido.FechaEnvio),
            new MySqlParameter("@numero", pedido.Numero)
        };
                acceso.EjecutarNonQuery(queryUpdate, paramUpdate.ToArray());

                // 2. Borrar los items viejos de este pedido específico
                string queryDelete = "DELETE FROM pedidos_productos WHERE id_pedido = @numero";
                acceso.EjecutarNonQuery(queryDelete, new MySqlParameter[] { new MySqlParameter("@numero", pedido.Numero) });

                // 3. Insertar los items nuevos
                foreach (BEItem item in pedido.ListaItems)
                {
                    string queryInsert = @"INSERT INTO pedidos_productos 
                          (id_pedido, id_producto, cantidad, precio_unitario, precio_total, nombre_producto) 
                          VALUES 
                          (@id_pedido, @id_producto, @cantidad, @precio_unitario, @precio_total, @nombre_producto)";

                    List<MySqlParameter> paramInsert = new List<MySqlParameter>
            {
                new MySqlParameter("@id_pedido", pedido.Numero),
                new MySqlParameter("@id_producto", item.Codigo),
                new MySqlParameter("@cantidad", item.Cantidad),
                
                // 🛠️ ¡CHAU MATEMÁTICA RARA! 
                // Pasamos las propiedades exactas de tu clase refactorizada
                new MySqlParameter("@precio_unitario", item.PrecioUnitario),
                new MySqlParameter("@precio_total", item.Subtotal),

                new MySqlParameter("@nombre_producto", item.Nombre)
            };
                    acceso.EjecutarNonQuery(queryInsert, paramInsert.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el pedido en BD: " + ex.Message);
            }
        }

        public void CambiarEstadoPedido(string numeroPedido, EstadoPedido nuevoEstado)
        {
            try
            {
                string query = "UPDATE pedidos SET estado = @estado WHERE numero = @numero";
                List<MySqlParameter> param = new List<MySqlParameter>
        {
            // Casteamos el enumerador a int para guardarlo en la BD (como hiciste en compras)
            new MySqlParameter("@estado", (int)nuevoEstado),
            new MySqlParameter("@numero", numeroPedido)
        };

                acceso.EjecutarNonQuery(query, param.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar el estado del pedido: " + ex.Message);
            }
        }

    }


    }

