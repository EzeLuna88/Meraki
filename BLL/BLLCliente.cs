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

        public void GuardarCliente(BECliente cliente)
        {
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
            mppCliente.ModificarCliente(cliente);
        }

        public void AgregarModificarComentarios(BECliente cliente)
        {
            mppCliente.AgregarModificarComentarios(cliente);
        }
    }



}
