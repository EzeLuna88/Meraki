using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCompraMinorista
    {
        DALCompraMinorista dalCompraMinorista;

        public BLLCompraMinorista()
        {
            dalCompraMinorista= new DALCompraMinorista();
        }

        public void GuardarCompraMinorista(BECompraMinorista compraMinorista
            )
        {
            dalCompraMinorista.GuardarCompraMinorista(compraMinorista);
        }
    }
}
