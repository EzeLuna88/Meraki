using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEComprobante
    {
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
        public BECliente Cliente { get; set; }

        public List<BEItem> ListaItems { get; set; }

        public decimal Total { get; set; }

        public bool PagoEfectivo { get; set; }


    }
}
