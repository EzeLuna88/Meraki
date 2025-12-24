using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEProductoCombo : BEProducto
    {
        public string Nombre { get; set; }

        public List<BEStock> ListaProductos { get; set; }

        public override string NombreMostrar => this.Nombre;
    
        public BEProductoCombo()
        {
            ListaProductos = new List<BEStock>();
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
