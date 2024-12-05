using BE;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class DALProductoCombo
    {
        public void GuardarProducto(BEProductoCombo producto)
        {
            try
            {
                string rutaArchivo = PathManager.GetFilePath("productos.xml");
                XDocument doc = XDocument.Load(rutaArchivo);

                XElement productos = new XElement("items");
                foreach (BEStock stock in producto.ListaProductos)
                {
                    XElement item = new XElement("stock",
                                              new XAttribute("codigo", stock.Codigo),
                                                new XElement("nombre", stock.Nombre),
                                                new XElement("medida", stock.Medida),
                                                new XElement("tipoMedida", stock.TipoMedida),
                                                new XElement("cantidadActual", stock.CantidadActual),
                                                new XElement("cantidadIngresada", stock.CantidadIngresada),
                                                new XElement("fechaIngreso", stock.FechaIngreso));
                    productos.Add(item);
                }

                doc.Element("productos").Add(new XElement("producto",
                                            new XAttribute("codigo", producto.Codigo.ToString().Trim()),
                                            productos,
                                            new XElement("nombre", producto.Nombre.ToString().Trim()),
                                            new XElement("unidad", producto.Unidad.ToString().Trim()),
                                            new XElement("tipo", producto.Tipo.ToString().Trim()),
                                            new XElement("precio_mayorista", producto.PrecioMayorista.ToString().Trim()),
                                            new XElement("precio_minorista", producto.PrecioMinorista.ToString().Trim())));

                // Guarda el documento XML en el archivo "clientes.xml"
                doc.Save(rutaArchivo);
            }
            catch (Exception)
            {

                throw;
            }


        }

        public bool CodigoYaExiste(string codigo)
        {
            try
            {
                string rutaArchivo = PathManager.GetFilePath("productos.xml");
                XDocument doc = XDocument.Load(rutaArchivo);
                var codigosExistentes = doc.Descendants("producto")
                                           .Select(p => p.Attribute("codigo")?.Value);

                return codigosExistentes.Contains(codigo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModificarProducto(BEProductoCombo beProducto)
        {
            try
            {
                string rutaArchivo = PathManager.GetFilePath("productos.xml");
                XDocument doc = XDocument.Load(rutaArchivo);
                var consulta = from producto in doc.Descendants("producto")
                               where producto.Attribute("codigo").Value == beProducto.Codigo
                               select producto;
                foreach (XElement modificar in consulta)
                {
                    modificar.Element("unidad").Value = beProducto.Unidad.ToString();
                    modificar.Element("nombre").Value = beProducto.Nombre;
                    modificar.Element("precio_mayorista").Value = beProducto.PrecioMayorista.ToString();
                    modificar.Element("precio_minorista").Value = beProducto.PrecioMinorista.ToString();
                }
                doc.Save(rutaArchivo);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void BorrarProducto(BEProductoCombo beProducto)
        {
            try
            {
                string rutaArchivo = PathManager.GetFilePath("productos.xml");
                XDocument doc = XDocument.Load(rutaArchivo);
                var consulta = from producto in doc.Descendants("producto")
                               where producto.Attribute("codigo").Value == beProducto.Codigo
                               select producto;
                consulta.Remove();
                doc.Save(rutaArchivo);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
