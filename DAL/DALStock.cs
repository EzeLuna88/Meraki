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

namespace DAL
{
    public class DALStock
    {
        public List<BEStock> cargarStock()
        {
            try
            {
                string rutaArchivo = PathManager.GetFilePath("stock.xml");

                XDocument doc;
                if (File.Exists(rutaArchivo))
                {
                    // Carga el archivo XML de clientes
                    doc = XDocument.Load(rutaArchivo);
                }
                else
                {
                    doc = new XDocument(new XElement("productos"));
                }
                doc.Save(rutaArchivo);

                var consulta = from Producto in doc.Element("productos").Elements("producto")
                               select new BEStock
                               {
                                   Codigo = Convert.ToString(Producto.Attribute("codigo").Value).Trim(),
                                   CantidadActual = int.Parse(Producto.Element("cantidad_actual").Value),
                                   Nombre = Convert.ToString(Producto.Element("nombre").Value).Trim(),
                                   Medida = Convert.ToDouble(Producto.Element("medida").Value),
                                   TipoMedida = Convert.ToString(Producto.Element("tipo_medida").Value).Trim(),
                                   CantidadReservada = Producto.Element("cantidad_reservada") != null
                                                ? int.Parse(Producto.Element("cantidad_reservada").Value)
                                                : 0

                               };
                List<BEStock> stock = consulta.ToList<BEStock>();
                return stock;
            }
            catch (Exception)
            {
                throw;

            }
        }

        public void BorrarProductoDeStock(BEStock beStock)
        {
            try
            {
                string rutaArchivo = PathManager.GetFilePath("stock.xml");
                XDocument doc = XDocument.Load(rutaArchivo);
                var consulta = from producto in doc.Descendants("producto")
                               where producto.Attribute("codigo").Value == beStock.Codigo
                               select producto;
                consulta.Remove();
                doc.Save(rutaArchivo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AgregarStock(BEStock beStock, int unidades)
        {
            string rutaArchivo = PathManager.GetFilePath("stock.xml");
            XDocument doc = XDocument.Load(rutaArchivo);
            var consulta = from producto in doc.Descendants("producto")
                           where producto.Attribute("codigo").Value == beStock.Codigo
                           select producto;
            var cantidadActual = consulta.Elements("cantidad_actual").FirstOrDefault();
            int unidadesTotales = int.Parse(cantidadActual.Value);
            unidadesTotales += unidades;

            cantidadActual.Value = unidadesTotales.ToString();




            doc.Save(rutaArchivo);
        }

        public bool ComprobarRepetido(BEStock beStock)
        {
            try
            {
                string rutaArchivo = PathManager.GetFilePath("stock.xml");
                XDocument doc = XDocument.Load(rutaArchivo);

                var repetido = doc.Root.Elements("producto").Where(p => (string)p.Element("nombre") == beStock.Nombre.Trim() && (string)p.Element("medida") == beStock.Medida.ToString().Trim()).Any();
                if (repetido)
                { return true; ; }
                else
                { return false; ; }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void GuardarNuevoProducto(BEStock beStock)
        {
            try
            {
                string rutaArchivo = PathManager.GetFilePath("stock.xml");
                XDocument doc = XDocument.Load(rutaArchivo);
                doc.Element("productos").Add(new XElement("producto",
                                               new XAttribute("codigo", beStock.Codigo.ToString().Trim()),
                                               new XElement("nombre", beStock.Nombre.Trim()),
                                               new XElement("medida", beStock.Medida.ToString().Trim()),
                                               new XElement("tipo_medida", beStock.TipoMedida.Trim()),
                                               new XElement("cantidad_actual", beStock.CantidadActual.ToString().Trim())));


                // Guarda el documento XML en el archivo "clientes.xml"
                doc.Save(rutaArchivo);



            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ActualizarStock(List<BEStock> listaStock)
        {
            string rutaArchivo = PathManager.GetFilePath("stock.xml");
            XDocument doc = XDocument.Load(rutaArchivo);
            foreach (BEStock beStock in listaStock)
            {
                var consulta = from compra in doc.Descendants("producto")
                               where compra.Attribute("codigo").Value == beStock.Codigo
                               select compra;
                var cantidadActual = consulta.Elements("cantidad_actual").FirstOrDefault();
                int unidadesTotales = int.Parse(cantidadActual.Value);
                unidadesTotales = beStock.CantidadActual;

                cantidadActual.Value = unidadesTotales.ToString();

            }


            doc.Save(rutaArchivo);
        }

        public void AcomodarCantidadReservadaStock()
        {
            string rutaArchivo = PathManager.GetFilePath("stock.xml");
            XDocument doc = XDocument.Load(rutaArchivo);
            foreach(XElement producto in doc.Descendants("producto"))
            {
                XElement cantidadReservadaElemento = producto.Element("cantidad_reservada");
                if (cantidadReservadaElemento != null)
                {
                    cantidadReservadaElemento.Value = "0"; // Seteamos la cantidad reservada a 0
                }
            }


            doc.Save(rutaArchivo);
        }


        public void ModificarStock(BEStock beStock)
        {
            string rutaArchivo = PathManager.GetFilePath("stock.xml");
            XDocument doc = XDocument.Load(rutaArchivo);

            var consulta = from producto in doc.Descendants("producto")
                           where producto.Attribute("codigo").Value == beStock.Codigo
                           select producto;
            foreach (XElement modificar in consulta)
            {
                modificar.Element("nombre").Value = beStock.Nombre;
                modificar.Element("medida").Value = beStock.Medida.ToString();
                modificar.Element("tipo_medida").Value = beStock.TipoMedida;

            }

            doc.Save(rutaArchivo);
        }

        public void CantidadReservadaStock(BEStock beStock)
        {
            string rutaArchivo = PathManager.GetFilePath("stock.xml");
            XDocument doc = XDocument.Load(rutaArchivo);

            var consulta = from producto in doc.Descendants("producto")
                           where producto.Attribute("codigo").Value == beStock.Codigo
                           select producto;
            foreach (XElement modificar in consulta)
            {
                XElement cantidadReservadaElemento = modificar.Element("cantidad_reservada");

                if (cantidadReservadaElemento == null)
                {
                    // Si no existe, lo crea
                    cantidadReservadaElemento = new XElement("cantidad_reservada", beStock.CantidadReservada.ToString());
                    modificar.Add(cantidadReservadaElemento); // Añade el nuevo elemento al producto
                }
                else
                {
                    // Si ya existe, actualiza su valor
                    cantidadReservadaElemento.Value = beStock.CantidadReservada.ToString();
                }

            }

            doc.Save(rutaArchivo);
        }
    }
}
