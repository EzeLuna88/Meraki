using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class BECompra
    {
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public List<BECarrito> ListaCarrito { get; set; }

        public Decimal Total { get; set; }

        public BECompra()
        {
           ListaCarrito = new List<BECarrito>();
            
        }
    }
}
