﻿using System;
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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Globalization;

namespace DAL
{
    public class DALProducto
    {
       

        public List<BEProducto> listaProductos()
        {

            try
            {
                

                XDocument doc;
                if (File.Exists("productos.xml"))
                {
                    // Carga el archivo XML de clientes
                    doc = XDocument.Load("productos.xml");
                }
                else
                {
                    doc = new XDocument(new XElement("productos"));
                }
                doc.Save("productos.xml");

                       

                var consulta = from Producto in XElement.Load("productos.xml").Elements("producto")
                               select Producto.Element("tipo")?.Value == "individual"
                               ? (BEProducto) new BEProductoIndividual

                               {
                                   Stock = (from stock in Producto.Elements("stock")
                                            select new BEStock
                                            {
                                                Codigo = stock.Attribute("codigo").Value,
                                                Nombre = stock.Element("nombre").Value,
                                                Medida = Convert.ToDouble(stock.Element("medida").Value),
                                                TipoMedida = stock.Element("tipoMedida").Value,
                                                CantidadActual = Convert.ToInt32(stock.Element("cantidadActual").Value)

                                            }).FirstOrDefault(),
                                   Codigo = Producto.Attribute("codigo").Value,
                                   Unidad = Convert.ToInt32(Producto.Element("unidad").Value),
                                   Tipo = Producto.Element("tipo").Value,
                                   PrecioMayorista = Convert.ToDecimal(Producto.Element("precio_mayorista").Value),
                                   PrecioMinorista = Convert.ToDecimal(Producto.Element("precio_minorista").Value)

                                   
                               }
                               : (BEProducto) new BEProductoCombo
                               {
                                   ListaProductos = (from stock in Producto.Element("items").Elements("stock")
                                                     select new BEStock
                                                     {
                                                         Codigo = stock.Attribute("codigo").Value,
                                                         Nombre = stock.Element("nombre").Value,
                                                         Medida = Convert.ToDouble(stock.Element("medida").Value),
                                                         TipoMedida = stock.Element("tipoMedida").Value,
                                                         CantidadActual = Convert.ToInt32(stock.Element("cantidadActual").Value)
                                                     }).ToList(),
                                   Codigo = Producto.Attribute("codigo").Value,
                                   Unidad = Convert.ToInt32(Producto.Element("unidad").Value),
                                   Tipo = Producto.Element("tipo").Value,
                                   Nombre = Producto.Element("nombre").Value,
                                   PrecioMayorista = Convert.ToDecimal(Producto.Element("precio_mayorista").Value),
                                   PrecioMinorista = Convert.ToDecimal(Producto.Element("precio_minorista").Value)
                               
                               };
                List<BEProducto> listaProductos = consulta.ToList<BEProducto>();
                return listaProductos;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void GuardarProducto(BEProductoIndividual producto)
        {
            try
            {
                XDocument doc = XDocument.Load("productos.xml");
                producto.Codigo = Guid.NewGuid().ToString();

                doc.Element("productos").Add(new XElement("producto",
                                            new XAttribute("codigo", producto.Codigo.ToString().Trim()),
                                           new XElement("stock",
                                                new XAttribute("codigo", producto.Stock.Codigo),
                                                new XElement("nombre", producto.Stock.Nombre),
                                                new XElement("medida", producto.Stock.Medida),
                                                new XElement("tipoMedida", producto.Stock.TipoMedida),
                                                new XElement("cantidadActual", producto.Stock.CantidadActual),
                                                new XElement("cantidadIngresada", producto.Stock.CantidadIngresada),
                                                new XElement("fechaIngreso", producto.Stock.FechaIngreso)
                                            ),
                                           new XElement("unidad", producto.Unidad.ToString().Trim()),
                                           new XElement("tipo", producto.Tipo.ToString().Trim()),
                                            new XElement("precio_mayorista", producto.PrecioMayorista.ToString().Trim()),
                                            new XElement("precio_minorista", producto.PrecioMinorista.ToString().Trim())));

                // Guarda el documento XML en el archivo "clientes.xml"
                doc.Save("productos.xml");
            }
            catch (Exception)
            {

                throw;
            }


        }

        public void BorrarProducto(BEProductoIndividual beProducto)
        {
            XDocument doc = XDocument.Load("productos.xml");
            var consulta = from producto in doc.Descendants("producto")
                           where producto.Element("stock").Attribute("codigo").Value == beProducto.Stock.Codigo
                           && producto.Element("unidad").Value == beProducto.Unidad.ToString()
                           && producto.Element("precio_mayorista").Value == beProducto.PrecioMayorista.ToString()
                           && producto.Element("precio_minorista").Value == beProducto.PrecioMinorista.ToString()
                           select producto;
            consulta.Remove();
            doc.Save("productos.xml");
        }

        public void ModificarProducto(BEProducto beProducto)
        {
            XDocument doc = XDocument.Load("productos.xml");
            var consulta = from producto in doc.Descendants("producto")
                           where producto.Attribute("codigo").Value == beProducto.Codigo
                           select producto;
            foreach (XElement modificar in consulta)
            {
                
                modificar.Element("unidad").Value = beProducto.Unidad.ToString();
               
                modificar.Element("precio_mayorista").Value = beProducto.PrecioMayorista.ToString();
                modificar.Element("precio_minorista").Value = beProducto.PrecioMinorista.ToString();

                
            }

            doc.Save("productos.xml");
        }

        public BEProducto BuscarProducto(string codigo)
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






