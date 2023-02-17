using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEProductoIndividual : BEProducto
    {
        public BEStock Stock { get; set; }

        public override string ToString()
        {
            return Stock.Nombre + " " + Stock.Medida.ToString() + Stock.TipoMedida.ToString() + " x " + Unidad;
        }
    }
}
