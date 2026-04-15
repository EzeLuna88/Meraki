using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;

namespace BLL
{


    public class BLLCliente
    {
        MPPCliente mppCliente;

        public BLLCliente()
        { mppCliente = new MPPCliente(); }

        private void ValidarYNormalizarCliente(BECliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new ArgumentException("El nombre del cliente es obligatorio.");

            cliente.Nombre = cliente.Nombre.Trim().ToUpper();

            if (!string.IsNullOrWhiteSpace(cliente.Direccion))
                cliente.Direccion = cliente.Direccion.Trim().ToUpper();

            if (!string.IsNullOrWhiteSpace(cliente.Localidad))
                cliente.Localidad = cliente.Localidad.Trim().ToUpper();

            if (!string.IsNullOrWhiteSpace(cliente.Direccion) && string.IsNullOrWhiteSpace(cliente.Telefono))
                throw new ArgumentException("Si ingresa una dirección de entrega, es obligatorio cargar un teléfono de contacto.");

            if (cliente.HorarioDeApertura.TotalMinutes > 0 || cliente.HorarioDeCierre.TotalMinutes > 0)
            {
                if (cliente.HorarioDeCierre <= cliente.HorarioDeApertura)
                {
                    throw new ArgumentException("El horario de cierre debe ser posterior al horario de apertura.");
                }
            }
        }

        public void GuardarCliente(BECliente cliente)
        {
            ValidarYNormalizarCliente(cliente);
            mppCliente.GuardarCliente(cliente);
        }

        public void GuardarCompraMayoristaTemporal(BECliente beCliente, BECompraMayorista beCompraMayoristaTemporal)
        {
            mppCliente.GuardarCompraMayoristaTemporal(beCliente, beCompraMayoristaTemporal);
        }

        public List<BECliente> ListaClientes()
        {
            return mppCliente.ListarClientes();
        }

        public void BorrarCliente(BECliente cliente)
        {
            mppCliente.BorrarCliente(cliente);
        }

        public void ModificarCliente(BECliente cliente)
        {
            ValidarYNormalizarCliente(cliente);
            mppCliente.ModificarCliente(cliente);
        }

        public void AgregarModificarComentarios(BECliente cliente)
        {
            if (cliente.Comentarios != null)
                cliente.Comentarios = cliente.Comentarios.Trim();
            mppCliente.AgregarModificarComentarios(cliente);
        }

        public BECliente ListarObjetoCliente(string codigoABuscar)
        {
            try
            {
                // 1. Traemos todos los clientes usando tu método que ya funciona
                List<BECliente> todosLosClientes = ListaClientes();

                // 2. Buscamos el primero que coincida con el código (ignorando espacios por las dudas)
                BECliente clienteEncontrado = todosLosClientes.Find(c =>
                    c.Codigo != null &&
                    c.Codigo.Trim() == codigoABuscar.Trim()
                );

                // 3. Lo devolvemos (si no lo encuentra, devolverá null automáticamente)
                return clienteEncontrado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el cliente por defecto: " + ex.Message);
            }
        }
    }



}
