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
    public class DALCliente
    {
        public void GuardarCliente(BECliente cliente)
        {
            try
            {

                // Carga el archivo XML de clientes
                XDocument doc = XDocument.Load("clientes.xml");

                // Genera un código único para el cliente utilizando la clase Guid
                cliente.Codigo = Guid.NewGuid().ToString();

                doc.Element("clientes").Add(new XElement("cliente",
                                            new XAttribute("codigo", cliente.Codigo.ToString().Trim()),
                                            new XElement("nombre", cliente.Nombre.Trim()),
                                            new XElement("direccion", cliente.Direccion.Trim()),
                                            new XElement("localidad", cliente.Localidad.Trim()),
                                            new XElement("telefono", cliente.Telefono.Trim()),
                                            new XElement("horario_de_apertura", cliente.HorarioDeApertura.ToString().Trim()),
                                            new XElement("horario_de_cierre", cliente.HorarioDeCierre.ToString().Trim())));

                // Guarda el documento XML en el archivo "clientes.xml"
                doc.Save("clientes.xml");
            }
            catch (Exception)
            {

                throw;
            }


        }

        public List<BECliente> listaClientes()
        {

            try
            {
                XDocument doc;
                if (File.Exists("clientes.xml"))
                {
                    // Carga el archivo XML de clientes
                    doc = XDocument.Load("clientes.xml");
                }
                else
                {
                    doc = new XDocument(new XElement("clientes"));
                }
                doc.Save("clientes.xml");

                var consulta = from Cliente in XElement.Load("clientes.xml").Elements("cliente")
                               select new BECliente
                               {
                                   Codigo = Convert.ToString(Cliente.Attribute("codigo").Value).Trim(),
                                   Nombre = Convert.ToString(Cliente.Element("nombre").Value).Trim(),
                                   Direccion = Convert.ToString(Cliente.Element("direccion").Value).Trim(),
                                   Localidad = Convert.ToString(Cliente.Element("localidad").Value).Trim(),
                                   Telefono = Convert.ToString(Cliente.Element("telefono").Value).Trim(),
                                   HorarioDeApertura = TimeSpan.Parse(Cliente.Element("horario_de_apertura").Value),
                                   HorarioDeCierre = TimeSpan.Parse(Cliente.Element("horario_de_cierre").Value)

                               };
                List<BECliente> listaClientes = consulta.ToList<BECliente>();
                return listaClientes;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void BorrarCliente(BECliente beCliente)
        {
            XDocument doc = XDocument.Load("clientes.xml");
            var consulta = from cliente in doc.Descendants("cliente")
                           where cliente.Attribute("codigo").Value == beCliente.Codigo
                           select cliente;
            consulta.Remove();
            doc.Save("clientes.xml");
        }

        public void ModificarCliente(BECliente beCliente)
        {
            XDocument doc = XDocument.Load("clientes.xml");
            var consulta = from cliente in doc.Descendants("cliente")
                           where cliente.Attribute("codigo").Value == beCliente.Codigo
                           select cliente;
            foreach (XElement modificar in consulta)
            {
                modificar.Element("nombre").Value = beCliente.Nombre;
                modificar.Element("direccion").Value = beCliente.Direccion;
                modificar.Element("localidad").Value = beCliente.Localidad;
                modificar.Element("telefono").Value = beCliente.Telefono;
                modificar.Element("horario_de_apertura").Value = beCliente.HorarioDeApertura.ToString();
                modificar.Element("horario_de_cierre").Value = beCliente.HorarioDeCierre.ToString();
            }

            doc.Save("clientes.xml");
        }
    }
}
