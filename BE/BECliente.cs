using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BECliente
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string Telefono { get; set; }
        public string TelefonoAlternativo { get; set; }
        public TimeSpan HorarioDeApertura { get; set; }
        public TimeSpan HorarioDeCierre { get; set; }
        public string Comentarios { get; set; }

        public BECompraMayorista CompraMayoristaTemp { get; set; }

    }
}
