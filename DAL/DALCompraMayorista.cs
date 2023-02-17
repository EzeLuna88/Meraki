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

namespace DAL
{
    public class DALCompraMayorista
    {
        public void GuardarCompraMayorista(BECompraMayorista compraMayorista)
        {
            try
            {
                XDocument doc;
                if (File.Exists("compras.xml"))
                {

                    doc = XDocument.Load("compras.xml");
                }
                else
                {
                    doc = new XDocument(new XElement("compras"));

                }
                doc.Save("compras.xml");

                XElement productos = new XElement("productos");
                foreach (BECarrito carrito in compraMayorista.ListaCarrito)
                {
                    XElement producto = new XElement("producto",
                                              new XElement("nombre", carrito.Producto.ToString()),
                                              new XElement("unidades", carrito.Cantidad),
                                              new XElement("monto", carrito.Total));
                    productos.Add(producto);
                }

                doc.Element("compras").Add(new XElement("compra",
                                               new XAttribute("codigo", compraMayorista.Codigo.ToString().Trim()),
                                               new XElement("fecha", compraMayorista.Fecha.ToString().Trim()),
                                               new XElement("total", compraMayorista.Total.ToString().Trim()),
                                               new XElement("cliente",
                                                new XElement("codigo", compraMayorista.Cliente.Codigo.Trim()),
                                                new XElement("nombre", compraMayorista.Cliente.Nombre.ToString().Trim()),
                                                new XElement("direccion",compraMayorista.Cliente.Direccion.Trim()),
                                                new XElement("localidad", compraMayorista.Cliente.Localidad.Trim()),
                                                new XElement("telefono", compraMayorista.Cliente.Telefono.Trim()),
                                                new XElement("horario_de_apertura", compraMayorista.Cliente.HorarioDeApertura.ToString().Trim()),
                                                new XElement("horario_de_cierre", compraMayorista.Cliente.HorarioDeCierre.ToString().Trim())),
                                               productos));
                ;
                doc.Save("compras.xml");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BEProducto BuscarCompraMayorista(string codigo)
        {
            XDocument doc = XDocument.Load("productos.xml");
            var consulta = from producto in doc.Descendants("producto")
                           where producto.Attribute("codigo").Value == codigo
                           select new BEProductoIndividual
                           {
                               Stock = (from stock in producto.Elements("stock")
                                        select new BEStock
                                        {
                                            Codigo = stock.Attribute("codigo").Value,
                                            Nombre = stock.Attribute("nombre").Value,
                                            Medida = Convert.ToDouble(stock.Attribute("medida").Value),
                                            TipoMedida = stock.Attribute("tipoMedida").Value,
                                            CantidadActual = Convert.ToInt32(stock.Attribute("cantidadActual").Value)

                                        }).FirstOrDefault(),
                               Codigo = producto.Attribute("codigo").Value,
                               Unidad = Convert.ToInt32(producto.Element("unidad").Value),
                               PrecioMayorista = Convert.ToDecimal(producto.Element("precio_mayorista").Value),
                               PrecioMinorista = Convert.ToDecimal(producto.Element("precio_minorista").Value)
                           };
            BEProducto beProducto = (BEProducto)consulta.FirstOrDefault();
            return beProducto;

        }
    }
}
