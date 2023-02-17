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
    public class DALCompraMinorista
    {

        public void GuardarCompraMinorista(BECompraMinorista compraMinorista)
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

                foreach (BECarrito item in compraMinorista.ListaCarrito)
                {
                    if(item.Producto is BEProductoIndividual)
                    { 
                        XElement producto = new XElement("producto",
                                                  new XElement("nombre", item.Producto.ToString().Trim()),
                                                  new XElement("unidades", item.Cantidad),
                                                  new XElement("monto", item.Total));
                        productos.Add(producto);
                    }
                    else if (item.Producto is BEProductoCombo)
                    {
                        BEProductoCombo productoCombo = (BEProductoCombo) item.Producto;
                        XElement producto = new XElement("producto",
                                                new XElement("nombre", productoCombo.Nombre.ToString().Trim()),
                                                new XElement("unidades", item.Cantidad),
                                                new XElement("monto", item.Total));
                        productos.Add(producto);
                    }
                }
                doc.Element("compras").Add(new XElement("compra",
                                               new XAttribute("codigo", compraMinorista.Codigo.ToString().Trim()),
                                               new XElement("fecha", compraMinorista.Fecha.ToString().Trim()),
                                               new XElement("total", compraMinorista.Total.ToString().Trim()),
                                               productos));
                                               ;
                doc.Save("compras.xml");
            }
			catch (Exception)
			{
				throw;
			}
        }
    }
}
