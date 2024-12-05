using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections;
using System.Xml.Serialization;
using System.Security.Cryptography;
using Servicios;

namespace DAL
{
    public class DALComprobante
    {
        public List<BEComprobante> listaComprobantes()
        {
            try
            {
                string rutaArchivo = PathManager.GetFilePath("comprobante.xml");

                XDocument doc;
                if (File.Exists(rutaArchivo))
                {
                    // Carga el archivo XML de clientes
                    doc = XDocument.Load(rutaArchivo);
                }
                else
                {
                    doc = new XDocument(new XElement("comprobantes"));
                }
                doc.Save(rutaArchivo);




                var consulta = from Comprobante in doc.Element("comprobantes").Elements("comprobante")
                               select new BEComprobante
                               {
                                   Numero = Convert.ToString(Comprobante.Attribute("numero").Value),
                                   Fecha = Convert.ToDateTime(Comprobante.Element("fecha").Value),
                                   Cliente = ((BECliente)(from Cliente in Comprobante.Elements("cliente")
                                                          select new BECliente
                                                          {
                                                              Codigo = Convert.ToString(Cliente.Attribute("codigo").Value).Trim(),
                                                              Nombre = Convert.ToString(Cliente.Element("nombre").Value).Trim(),
                                                              Direccion = Convert.ToString(Cliente.Element("direccion").Value).Trim(),
                                                              Localidad = Convert.ToString(Cliente.Element("localidad").Value).Trim(),
                                                              Telefono = Convert.ToString(Cliente.Element("telefono").Value).Trim(),
                                                              TelefonoAlternativo = Convert.ToString(Cliente.Element("telefono_alternativo").Value.Trim()),
                                                              HorarioDeApertura = TimeSpan.Parse(Cliente.Element("horario_de_apertura").Value),
                                                              Comentarios = Cliente.Element("comentarios") != null ? Convert.ToString(Cliente.Element("comentarios").Value) : string.Empty,
                                                              HorarioDeCierre = TimeSpan.Parse(Cliente.Element("horario_de_cierre").Value)

                                                          }).FirstOrDefault()),


                                   ListaItems = (from Item in Comprobante.Elements("item")
                                                 select new BEItem
                                                 {
                                                     Codigo = Convert.ToString(Item.Attribute("codigo").Value)?.Trim(),
                                                     Cantidad = Convert.ToInt32(Item.Element("cantidad").Value),
                                                     Nombre = Convert.ToString(Item.Element("nombre").Value)?.Trim(),
                                                     Precio = Convert.ToDecimal(Item.Element("precio").Value)
                                                 }).ToList(),
                                   PagoEfectivo = Convert.ToBoolean(Comprobante.Element("pago_efectivo").Value),
                                   Total = Convert.ToDecimal(Comprobante.Element("total").Value),


                               };
                List<BEComprobante> comprobantes = consulta.ToList<BEComprobante>();
                return comprobantes;
            }
            catch (Exception)
            {
                throw;

            }
        }

        public void GuardarNuevoComprobante(BEComprobante beComprobante)
        {
            try
            {
                string rutaArchivo = PathManager.GetFilePath("comprobante.xml");

                XDocument doc;
                if (File.Exists(rutaArchivo))
                {
                    // Carga el archivo XML de clientes
                    doc = XDocument.Load(rutaArchivo);
                }
                else
                {
                    doc = new XDocument(new XElement("comprobantes"));
                }
                doc.Save(rutaArchivo);


                // Crear el elemento <comprobante> y sus hijos
                XElement comprobanteElement = new XElement("comprobante",
                    new XAttribute("numero", beComprobante.Numero.ToString().Trim()),
                    new XElement("fecha", beComprobante.Fecha.ToString().Trim()),
                    new XElement("cliente",
                    new XAttribute("codigo", beComprobante.Cliente.Codigo.ToString().Trim()),
                    new XElement("nombre", beComprobante.Cliente.Nombre.ToString().Trim()),
                    new XElement("direccion", beComprobante.Cliente.Direccion.ToString().Trim()),
                    new XElement("localidad", beComprobante.Cliente.Localidad.ToString().Trim()),
                    new XElement("direccion", beComprobante.Cliente.Direccion.ToString().Trim()),
                    new XElement("telefono", beComprobante.Cliente.Telefono.ToString().Trim()),
                    new XElement("telefono_alternativo", beComprobante.Cliente.TelefonoAlternativo.ToString().Trim()),
                    new XElement("horario_de_apertura", beComprobante.Cliente.HorarioDeApertura.ToString().Trim()),
                    new XElement("comentarios", beComprobante.Cliente.Comentarios.ToString().Trim()),
                    new XElement("horario_de_cierre", beComprobante.Cliente.HorarioDeCierre.ToString().Trim()))


                    );

                // Iterar sobre la lista de ítems y crear un elemento <item> para cada uno
                foreach (var item in beComprobante.ListaItems)
                {
                    XElement itemElement = new XElement("item",
                        new XAttribute("codigo", item.Codigo.ToString().Trim()),
                        new XElement("cantidad", item.Cantidad.ToString().Trim()),
                        new XElement("nombre", item.Nombre.ToString().Trim()),
                        new XElement("precio", item.Precio.ToString().Trim()));

                    // Agregar el elemento <item> al elemento <comprobante>
                    comprobanteElement.Add(itemElement);
                }

                comprobanteElement.Add(new XElement("pago_efectivo", beComprobante.PagoEfectivo.ToString().Trim()));
                // Agregar el elemento <total> al elemento <comprobante>
                comprobanteElement.Add(new XElement("total", beComprobante.Total.ToString().Trim()));

                // Agregar el elemento <comprobante> al elemento <comprobantes>
                doc.Element("comprobantes").Add(comprobanteElement);

                // Guardar el documento XML en el archivo "comprobante.xml"
                doc.Save(rutaArchivo);



            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BEComprobante> FiltroComprobantes(DateTime inicio, DateTime final, BECliente cliente)
        {
            try
            {
                string rutaArchivo = PathManager.GetFilePath("comprobante.xml");

                XDocument doc;
                if (File.Exists(rutaArchivo))
                {
                    // Carga el archivo XML de clientes
                    doc = XDocument.Load(rutaArchivo);
                }
                else
                {
                    doc = new XDocument(new XElement("comprobantes"));
                }
                doc.Save(rutaArchivo);

                var consulta = from Comprobante in doc.Element("comprobantes").Elements("comprobante")
                               let fechaComprobante = Convert.ToDateTime(Comprobante.Element("fecha").Value)
                               let clienteComprobante = (from Cliente in Comprobante.Elements("cliente")
                                                         select new BECliente
                                                         {
                                                             Codigo = Convert.ToString(Cliente.Attribute("codigo").Value).Trim(),
                                                             Nombre = Convert.ToString(Cliente.Element("nombre").Value).Trim(),
                                                             Direccion = Convert.ToString(Cliente.Element("direccion").Value).Trim(),
                                                             Localidad = Convert.ToString(Cliente.Element("localidad").Value).Trim(),
                                                             Telefono = Convert.ToString(Cliente.Element("telefono").Value).Trim(),
                                                             TelefonoAlternativo = Convert.ToString(Cliente.Element("telefono_alternativo").Value.Trim()),
                                                             HorarioDeApertura = TimeSpan.Parse(Cliente.Element("horario_de_apertura").Value),
                                                             Comentarios = Cliente.Element("comentarios") != null ? Convert.ToString(Cliente.Element("comentarios").Value) : string.Empty,
                                                             HorarioDeCierre = TimeSpan.Parse(Cliente.Element("horario_de_cierre").Value)
                                                         }).FirstOrDefault()
                               where fechaComprobante >= inicio && fechaComprobante <= final
                                     && (cliente == null || (clienteComprobante != null && clienteComprobante.Codigo == cliente.Codigo)) // Condición para filtrar por cliente solo si no es null
                               select new BEComprobante
                               {
                                   Numero = Convert.ToString(Comprobante.Attribute("numero").Value),
                                   Fecha = fechaComprobante,
                                   Cliente = clienteComprobante, // Asignar el cliente filtrado
                                   ListaItems = (from Item in Comprobante.Elements("item")
                                                 select new BEItem
                                                 {
                                                     Codigo = Convert.ToString(Item.Attribute("codigo").Value)?.Trim(),
                                                     Cantidad = Convert.ToInt32(Item.Element("cantidad").Value),
                                                     Nombre = Convert.ToString(Item.Element("nombre").Value)?.Trim(),
                                                     Precio = Convert.ToDecimal(Item.Element("precio").Value)
                                                 }).ToList(),
                                   PagoEfectivo = Convert.ToBoolean(Comprobante.Element("pago_efectivo").Value),
                                   Total = Convert.ToDecimal(Comprobante.Element("total").Value)
                               };

                // Convertir la consulta a una lista y devolverla
                List<BEComprobante> comprobantesFiltrados = consulta.ToList();
                return comprobantesFiltrados;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
