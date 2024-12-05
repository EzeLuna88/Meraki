using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Xml.Linq;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections;
using System.Xml.Serialization;
using System.Security.Cryptography;
using Servicios;
using System.Globalization;

namespace DAL
{
    public class DALCliente
    {
        public void GuardarCliente(BECliente cliente)
        {
            try
            {
                // Obtener la ruta del archivo XML usando PathManager
                string rutaArchivo = PathManager.GetFilePath("clientes.xml");

                // Carga el archivo XML de clientes
                XDocument doc = XDocument.Load(rutaArchivo);

                // Genera un código único para el cliente utilizando la clase Guid
                cliente.Codigo = Guid.NewGuid().ToString();

                doc.Element("clientes").Add(new XElement("cliente",
                                            new XAttribute("codigo", cliente.Codigo.ToString().Trim()),
                                            new XElement("nombre", cliente.Nombre.Trim()),
                                            new XElement("direccion", cliente.Direccion.Trim()),
                                            new XElement("localidad", cliente.Localidad.Trim()),
                                            new XElement("telefono", cliente.Telefono.Trim()),
                                            new XElement("telefono_alternativo", cliente.TelefonoAlternativo.Trim()),
                                            new XElement("horario_de_apertura", cliente.HorarioDeApertura.ToString().Trim()),
                                            new XElement("horario_de_cierre", cliente.HorarioDeCierre.ToString().Trim())));

                // Guarda el documento XML en el archivo "clientes.xml"
                doc.Save(rutaArchivo);
            }
            catch (Exception)
            {
                throw;
            }


        }

        public void GuardarCompraMayoristaTemporal(BECliente beCliente, BECompraMayorista compraMayoristaTemporal)
        {
            string rutaArchivo = PathManager.GetFilePath("clientes.xml");
            XDocument doc = XDocument.Load(rutaArchivo);


            var clienteElemento = doc.Descendants("cliente")
                                 .FirstOrDefault(c => c.Attribute("codigo").Value == beCliente.Codigo);

            if (clienteElemento != null)
            {
                clienteElemento.Element("compraMayoristaTemporal")?.Remove();

                XElement compraMayoristaElemento = new XElement("compraMayoristaTemporal");
                foreach (var item in compraMayoristaTemporal.ListaCarrito)
                {
                    XElement productoElemento = new XElement("producto",
                        new XElement("codigo_producto", item.Producto.Codigo),
                        new XElement("cantidad", item.Cantidad),
                        new XElement("total", item.Total.ToString(CultureInfo.InvariantCulture))
                    );
                    compraMayoristaElemento.Add(productoElemento);
                }
                clienteElemento.Add(compraMayoristaElemento);
                doc.Save(rutaArchivo);

            }

        }

        public List<BECliente> listaClientes()
        {

            try
            {
                // Obtener la ruta del archivo XML usando PathManager
                string rutaArchivo = PathManager.GetFilePath("clientes.xml");

                XDocument doc;
                if (File.Exists(rutaArchivo))
                {
                    // Carga el archivo XML de clientes
                    doc = XDocument.Load(rutaArchivo);
                }
                else
                {
                    doc = new XDocument(new XElement("clientes"));

                }
                doc.Save(rutaArchivo);

                var consulta = from Cliente in doc.Element("clientes").Elements("cliente")
                               select new BECliente
                               {
                                   Codigo = Convert.ToString(Cliente.Attribute("codigo").Value).Trim(),
                                   Nombre = Convert.ToString(Cliente.Element("nombre").Value).Trim(),
                                   Direccion = Convert.ToString(Cliente.Element("direccion").Value).Trim(),
                                   Localidad = Convert.ToString(Cliente.Element("localidad").Value).Trim(),
                                   Telefono = Convert.ToString(Cliente.Element("telefono").Value).Trim(),
                                   TelefonoAlternativo = Convert.ToString(Cliente.Element("telefono_alternativo").Value).Trim(),
                                   HorarioDeApertura = TimeSpan.Parse(Cliente.Element("horario_de_apertura").Value),
                                   HorarioDeCierre = TimeSpan.Parse(Cliente.Element("horario_de_cierre").Value),
                                   Comentarios = Cliente.Element("comentarios") != null
                                         ? (string)Cliente.Element("comentarios").Value
                                         : string.Empty,
                                   CompraMayoristaTemp = Cliente.Element("compraMayoristaTemporal") != null
                               ? new BECompraMayorista
                               {
                                   ListaCarrito = (from Producto in Cliente.Element("compraMayoristaTemporal").Elements("producto")
                                                   select new BECarrito
                                                   {
                                                       Producto = RecuperarProducto(Producto),
                                                       Cantidad = int.Parse(Producto.Element("cantidad").Value),
                                                       Total = decimal.Parse(Producto.Element("total").Value, CultureInfo.InvariantCulture)
                                                   }).ToList()
                               }
                               : null
                               };
                List<BECliente> listaClientes = consulta.ToList<BECliente>();
                return listaClientes;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private BEProducto RecuperarProducto(XElement productoElement)
        {
            string rutaArchivo = PathManager.GetFilePath("productos.xml");
            XDocument docProductos = XDocument.Load(rutaArchivo);
            

            string codigoProducto = productoElement.Element("codigo_producto").Value.Trim();

            XElement productoXML = docProductos.Descendants("producto")
        .FirstOrDefault(p => p.Attribute("codigo").Value.Trim() == codigoProducto);

            string tipoProducto = productoXML.Element("tipo").Value.Trim();

            if (tipoProducto == "individual")
            {
                return new BEProductoIndividual
                {
                    Codigo = productoXML.Attribute("codigo")?.Value.Trim(),
                    Unidad = int.TryParse(productoXML.Element("unidad")?.Value.Trim(), out int unidad) ? unidad : 0,
                    PrecioMayorista = decimal.TryParse(productoXML.Element("precio_mayorista")?.Value.Trim(), out decimal precioMayorista) ? precioMayorista : 0m,
                    PrecioMinorista = decimal.TryParse(productoXML.Element("precio_minorista")?.Value.Trim(), out decimal precioMinorista) ? precioMinorista : 0m,
                    Tipo = tipoProducto,
                    Stock = new BEStock
                    {
                        Codigo = productoXML.Element("stock")?.Attribute("codigo")?.Value.Trim(),
                        Nombre = productoXML.Element("stock")?.Element("nombre")?.Value.Trim(),
                        Medida = double.TryParse(productoXML.Element("stock")?.Element("medida")?.Value.Trim(), out double medida) ? medida : 0.0,
                        TipoMedida = productoXML.Element("stock")?.Element("tipoMedida")?.Value.Trim(),
                        CantidadActual = int.TryParse(productoXML.Element("stock")?.Element("cantidadActual")?.Value.Trim(), out int cantidadActual) ? cantidadActual : 0,
                        CantidadIngresada = int.TryParse(productoXML.Element("stock")?.Element("cantidadIngresada")?.Value.Trim(), out int cantidadIngresada) ? cantidadIngresada : 0,
                        FechaIngreso = DateTime.TryParse(productoXML.Element("stock")?.Element("fechaIngreso")?.Value.Trim(), out DateTime fechaIngreso) ? fechaIngreso : DateTime.MinValue
                    }
                };
            }
            else if (tipoProducto == "combo")
            {
                return new BEProductoCombo
                {
                    Codigo = productoXML.Attribute("codigo").Value.Trim(),
                    Nombre = productoXML.Element("nombre").Value.Trim(),
                    Unidad = int.Parse(productoXML.Element("unidad").Value.Trim()),
                    PrecioMayorista = decimal.Parse(productoXML.Element("precio_mayorista").Value.Trim()),
                    PrecioMinorista = decimal.Parse(productoXML.Element("precio_minorista").Value.Trim()),
                    Tipo = tipoProducto,
                    ListaProductos = (from item in productoXML.Element("items").Elements("stock")
                                      select new BEStock
                                      {
                                          Codigo = item.Attribute("codigo").Value.Trim(),
                                          Nombre = item.Element("nombre").Value.Trim(),
                                          Medida = double.Parse(item.Element("medida").Value.Trim()),
                                          TipoMedida = item.Element("tipoMedida").Value.Trim(),
                                          CantidadActual = int.Parse(item.Element("cantidadActual").Value.Trim()),
                                          CantidadIngresada = int.Parse(item.Element("cantidadIngresada").Value.Trim()),
                                          FechaIngreso = DateTime.Parse(item.Element("fechaIngreso").Value.Trim()),
                                          CantidadReservada = item.Element("cantidad_reservada") != null
                                                ? int.Parse(item.Element("cantidad_reservada").Value)
                                                : 0
                                      }).ToList()
                };
            }

            return null;
        }

        public void BorrarCliente(BECliente beCliente)
        {
            string rutaArchivo = PathManager.GetFilePath("clientes.xml");
            XDocument doc = XDocument.Load(rutaArchivo);
            var consulta = from cliente in doc.Descendants("cliente")
                           where cliente.Attribute("codigo").Value == beCliente.Codigo
                           select cliente;
            consulta.Remove();
            doc.Save(rutaArchivo);
        }

        public void ModificarCliente(BECliente beCliente)
        {
            string rutaArchivo = PathManager.GetFilePath("clientes.xml");
            XDocument doc = XDocument.Load(rutaArchivo);
            var consulta = from cliente in doc.Descendants("cliente")
                           where cliente.Attribute("codigo").Value == beCliente.Codigo
                           select cliente;
            foreach (XElement modificar in consulta)
            {
                modificar.Element("nombre").Value = beCliente.Nombre;
                modificar.Element("direccion").Value = beCliente.Direccion;
                modificar.Element("localidad").Value = beCliente.Localidad;
                modificar.Element("telefono").Value = beCliente.Telefono;
                modificar.Element("telefono_alternativo").Value = beCliente.TelefonoAlternativo;
                modificar.Element("horario_de_apertura").Value = beCliente.HorarioDeApertura.ToString();
                modificar.Element("horario_de_cierre").Value = beCliente.HorarioDeCierre.ToString();
            }

            doc.Save(rutaArchivo);
        }

        public void AgregarModificarComentarios(BECliente cliente)
        {
            string rutaArchivo = PathManager.GetFilePath("clientes.xml");
            XDocument doc = XDocument.Load(rutaArchivo);

            var clienteElement = doc.Descendants("cliente")
                            .FirstOrDefault(c => c.Attribute("codigo").Value == cliente.Codigo);


            if (clienteElement != null)
            {
                // Buscar el elemento "comentarios" dentro del cliente
                XElement comentariosElement = clienteElement.Element("comentarios");

                // Si el elemento "comentarios" no existe, crearlo
                if (comentariosElement == null)
                {
                    comentariosElement = new XElement("comentarios", cliente.Comentarios);
                    clienteElement.Add(comentariosElement);
                }
                else
                {
                    // Si el elemento "comentarios" ya existe, modificar su valor
                    comentariosElement.Value = cliente.Comentarios;
                }

                // Guardar los cambios en el archivo XML
                doc.Save(rutaArchivo);
            }
            else
            {
                throw new Exception("Cliente no encontrado en el archivo XML.");
            }
        }
    }
}
