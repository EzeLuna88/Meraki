using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BECarrito
    {
        public BEProducto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

    }
}
