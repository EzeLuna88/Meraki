using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BLL
{
    

    public class BLLCliente
    {
        DALCliente dalCliente;

        public BLLCliente()
        { dalCliente = new DALCliente(); }

        public void GuardarCliente(BECliente cliente)
        {
            dalCliente.GuardarCliente(cliente);
        }

        public List<BECliente> ListaClientes()
        {
            return dalCliente.listaClientes();
        }

        public void BorrarCliente(BECliente cliente)
        {
            dalCliente.BorrarCliente(cliente);
        }

        public void ModificarCliente(BECliente cliente)
        {
            dalCliente.ModificarCliente(cliente);
        }
    }

   

}
