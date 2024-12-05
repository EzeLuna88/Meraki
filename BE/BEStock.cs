using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEStock
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double Medida { get; set; }
        public string TipoMedida { get; set; }
        public int CantidadActual { get; set; }
        public int CantidadIngresada { get; set; }

        public int CantidadReservada { get; set; }
        public DateTime FechaIngreso { get; set; }
        

        public override string ToString()
        {
            return Nombre + " " + Medida.ToString() + TipoMedida.ToString();
        }

    }
}
