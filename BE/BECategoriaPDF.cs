using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BECategoriaPDF
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public BECategoriaPDF() { }

        public BECategoriaPDF(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }
    }
}
