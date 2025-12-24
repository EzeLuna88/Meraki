using BE;
using MPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCompraMayorista
    {
        MPPCompraMayorista mppCompraMayorista;
        public BLLCompraMayorista()
        {
            mppCompraMayorista = new MPPCompraMayorista();
        }

        public void GuardarCompraMayorista(BECompraMayorista compraMayorista)
        {
            mppCompraMayorista.GuardarCompraMayorista(compraMayorista);
        }
    }
}
