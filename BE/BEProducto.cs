using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class BEProducto
    {
        
        public int Unidad { get; set; }
        public decimal PrecioMayorista { get; set; }
        public decimal PrecioMinorista { get; set; }
        public string Codigo { get; set; }
        public string Tipo { get; set; }

        public abstract string NombreMostrar { get; }


    }
}
