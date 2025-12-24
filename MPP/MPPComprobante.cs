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
    public class MPPComprobante
    {
        private readonly AccesoDAL acceso;

        public MPPComprobante()
        {
            acceso = new AccesoDAL();
        }
        public List<BEComprobante> ListaComprobantes()
        {
            try
            {
                string query = @"SELECT 
            id_comprobante, fecha, pago_efectivo, total, id_cliente,
            nombre_cliente, direccion_cliente, localidad_cliente,
            telefono_cliente, telefono_alternativo_cliente,
            horario_de_apertura_cliente, horario_de_cierre_cliente,
            comentarios_cliente
            FROM comprobantes;";

                DataTable dt = acceso.EjecutarConsulta(query);
                List<BEComprobante> lista = new List<BEComprobante>();

                foreach (DataRow row in dt.Rows)
                {
                    string numeroComprobante = row["id_comprobante"].ToString();

                    // Buscar los items asociados a este comprobante
                    string queryItems = @"
                SELECT 
                    cp.id_producto AS codigo,
                    cp.nombre_producto,
                    cp.cantidad,
                    cp.precio_unitario AS precio
                FROM comprobantes_productos cp
                WHERE cp.id_comprobante = @numero;";

                    List<MySqlParameter> parametros = new List<MySqlParameter>
            {
                new MySqlParameter("@numero", numeroComprobante)
            };

                    DataTable dtItems = acceso.EjecutarConsulta(queryItems, parametros.ToArray());

                    List<BEItem> listaItems = new List<BEItem>();
                    foreach (DataRow itemRow in dtItems.Rows)
                    {
                        listaItems.Add(new BEItem
                        {
                            Codigo = itemRow["codigo"].ToString(),
                            Nombre = itemRow["nombre_producto"].ToString(),
                            Cantidad = Convert.ToInt32(itemRow["cantidad"]),
                            Precio = Convert.ToDecimal(itemRow["precio"])
                        });
                    }

                    // Construir cliente a partir de los datos históricos guardados en el comprobante
                    BECliente cliente = new BECliente
                    {
                        Codigo = row["id_cliente"].ToString(),
                        Nombre = row["nombre_cliente"].ToString(),
                        Direccion = row["direccion_cliente"].ToString(),
                        Localidad = row["localidad_cliente"].ToString(),
                        Telefono = row["telefono_cliente"].ToString(),
                        TelefonoAlternativo = row["telefono_alternativo_cliente"].ToString(),
                        HorarioDeApertura = TimeSpan.Parse(row["horario_de_apertura_cliente"].ToString()),
                        HorarioDeCierre = TimeSpan.Parse(row["horario_de_cierre_cliente"].ToString()),
                        Comentarios = row["comentarios_cliente"] != DBNull.Value ? row["comentarios_cliente"].ToString() : string.Empty
                    };

                    // Construir comprobante
                    BEComprobante comprobante = new BEComprobante
                    {
                        Numero = numeroComprobante,
                        Fecha = Convert.ToDateTime(row["fecha"]),
                        PagoEfectivo = Convert.ToBoolean(row["pago_efectivo"]),
                        Total = Convert.ToDecimal(row["total"]),
                        Cliente = cliente,
                        ListaItems = listaItems
                    };

                    lista.Add(comprobante);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de comprobantes.", ex);
            }
        }

        public void GuardarNuevoComprobante(BEComprobante beComprobante)
        {
            try
            {
                // Guardar comprobante (incluyendo todos los datos del cliente en ese momento)
                string queryInsertComprobante = @"INSERT INTO comprobantes 
            (id_comprobante, fecha, id_cliente, pago_efectivo, total,
             nombre_cliente, direccion_cliente, telefono_cliente, 
             horario_de_apertura_cliente, comentarios_cliente, 
             localidad_cliente, telefono_alternativo_cliente, horario_de_cierre_cliente)
            VALUES 
            (@numero, @fecha, @cliente_codigo, @pago_efectivo, @total,
             @nombre_cliente, @direccion_cliente, @telefono_cliente, 
             @horario_apertura, @comentarios, 
             @localidad_cliente, @telefono_alternativo, @horario_cierre);";

                List<MySqlParameter> parametrosComprobante = new List<MySqlParameter>
        {
            new MySqlParameter("@numero", beComprobante.Numero),
            new MySqlParameter("@fecha", beComprobante.Fecha),
            new MySqlParameter("@cliente_codigo", beComprobante.Cliente.Codigo),
            new MySqlParameter("@pago_efectivo", beComprobante.PagoEfectivo),
            new MySqlParameter("@total", beComprobante.Total),

            // Datos históricos del cliente
            new MySqlParameter("@nombre_cliente", beComprobante.Cliente.Nombre),
            new MySqlParameter("@direccion_cliente", beComprobante.Cliente.Direccion),
            new MySqlParameter("@telefono_cliente", beComprobante.Cliente.Telefono),
            new MySqlParameter("@horario_apertura", beComprobante.Cliente.HorarioDeApertura),
            new MySqlParameter("@comentarios", beComprobante.Cliente.Comentarios),
            new MySqlParameter("@localidad_cliente", beComprobante.Cliente.Localidad),
            new MySqlParameter("@telefono_alternativo", beComprobante.Cliente.TelefonoAlternativo),
            new MySqlParameter("@horario_cierre", beComprobante.Cliente.HorarioDeCierre)
        };

                acceso.EjecutarNonQueryConParametros(queryInsertComprobante, parametrosComprobante);

                // Guardar productos del comprobante (como ya lo tenías)
                foreach (var item in beComprobante.ListaItems)
                {
                    string queryInsertItem = @"INSERT INTO comprobantes_productos 
                (id_comprobante, id_producto, cantidad, precio_unitario, precio_total, nombre_producto)
                VALUES 
                (@id_comprobante, @id_producto, @cantidad, @precio_unitario, @precio_total, @nombre_producto);";

                    List<MySqlParameter> parametrosItem = new List<MySqlParameter>
            {
                new MySqlParameter("@id_comprobante", beComprobante.Numero),
                new MySqlParameter("@id_producto", item.Codigo),
                new MySqlParameter("@cantidad", item.Cantidad),
                new MySqlParameter("@precio_unitario", item.Precio),
                new MySqlParameter("@precio_total", item.Cantidad * item.Precio),
                new MySqlParameter("@nombre_producto", item.Nombre)
            };

                    acceso.EjecutarNonQueryConParametros(queryInsertItem, parametrosItem);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el nuevo comprobante en la base de datos.", ex);
            }
        }

        public List<BEComprobante> FiltroComprobantes(DateTime inicio, DateTime final, BECliente cliente)
        {
            try
            {
                List<MySqlParameter> parametros = new List<MySqlParameter>
        {
            new MySqlParameter("@fechaInicio", inicio),
            new MySqlParameter("@fechaFin", final)
        };

                string consulta = @"SELECT c.id_comprobante, c.fecha, c.id_cliente, c.pago_efectivo, c.total,
                           cl.nombre, cl.direccion, cl.localidad, cl.telefono, cl.telefono_alternativo,
                           cl.horario_de_apertura, cl.comentarios, cl.horario_de_cierre
                    FROM comprobantes c
                    INNER JOIN clientes cl ON c.id_cliente = cl.codigo
                    WHERE c.fecha BETWEEN @fechaInicio AND @fechaFin";

                if (cliente != null)
                {
                    consulta += " AND c.id_cliente = @codigoCliente";
                    parametros.Add(new MySqlParameter("@codigoCliente", cliente.Codigo));
                }

                DataTable tabla = acceso.EjecutarConsulta(consulta, parametros.ToArray());

                List<BEComprobante> comprobantes = new List<BEComprobante>();

                foreach (DataRow row in tabla.Rows)
                {
                    BECliente cli = new BECliente
                    {
                        Codigo = row["id_cliente"].ToString(),
                        Nombre = row["nombre"].ToString(),
                        Direccion = row["direccion"].ToString(),
                        Localidad = row["localidad"].ToString(),
                        Telefono = row["telefono"].ToString(),
                        TelefonoAlternativo = row["telefono_alternativo"].ToString(),
                        HorarioDeApertura = TimeSpan.Parse(row["horario_de_apertura"].ToString()),
                        Comentarios = row["comentarios"].ToString(),
                        HorarioDeCierre = TimeSpan.Parse(row["horario_de_cierre"].ToString())
                    };

                    string numeroComprobante = row["id_comprobante"].ToString();

                    // Obtener los items asociados
                    string consultaItems = @"
                SELECT cp.id_producto, p.nombre, cp.cantidad, cp.precio_unitario
                FROM comprobantes_productos cp
                INNER JOIN producto p ON cp.id_producto = p.id
                WHERE cp.id_comprobante = @numero";

                    List<MySqlParameter> paramItem = new List<MySqlParameter>
            {
                new MySqlParameter("@numero", numeroComprobante)
            };

                    DataTable tablaItems = acceso.EjecutarConsulta(consultaItems, paramItem.ToArray());

                    List<BEItem> items = new List<BEItem>();
                    foreach (DataRow rowItem in tablaItems.Rows)
                    {
                        items.Add(new BEItem
                        {
                            Codigo = rowItem["id_producto"].ToString(),
                            Nombre = rowItem["nombre"].ToString(),
                            Cantidad = Convert.ToInt32(rowItem["cantidad"]),
                            Precio = Convert.ToDecimal(rowItem["precio_unitario"])
                        });
                    }

                    comprobantes.Add(new BEComprobante
                    {
                        Numero = numeroComprobante,
                        Fecha = Convert.ToDateTime(row["fecha"]),
                        Cliente = cli,
                        ListaItems = items,
                        PagoEfectivo = Convert.ToBoolean(row["pago_efectivo"]),
                        Total = Convert.ToDecimal(row["total"])
                    });
                }

                return comprobantes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar los comprobantes desde la base de datos.", ex);
            }
        }
    }
}
