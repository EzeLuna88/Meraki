using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCompraMayorista
    {
        DALCompraMayorista dalCompraMayorista;
        public BLLCompraMayorista()
        {
            dalCompraMayorista = new DALCompraMayorista();
        }

        public void GuardarCompraMayorista(BECompraMayorista compraMayorista)
        {
            dalCompraMayorista.GuardarCompraMayorista(compraMayorista);
        }
    }
}
