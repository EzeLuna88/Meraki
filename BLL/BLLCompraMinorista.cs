using BE;
using MPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCompraMinorista
    {
        MPPCompraMinorista mppCompraMinorista;

        public BLLCompraMinorista()
        {
            mppCompraMinorista= new MPPCompraMinorista();
        }

        public void GuardarCompraMinorista(BECompraMinorista compraMinorista
            )
        {
            mppCompraMinorista.GuardarCompraMinorista(compraMinorista);
        }
    }
}
