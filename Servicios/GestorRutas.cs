using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public static class GestorRutas
    {


        public static string GenerarRutaDestino(string rutaBase, DateTime fechaDocumento, string tipoDocumento, string identificador)
        {
            if (string.IsNullOrEmpty(rutaBase))
            {
                rutaBase = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }

            // Usamos la fecha que nos pasan por parámetro, no la de hoy obligatoriamente
            string anio = fechaDocumento.ToString("yyyy");

            CultureInfo culturaArg = new CultureInfo("es-AR");
            string mes = fechaDocumento.ToString("MM - MMMM", culturaArg);
            mes = char.ToUpper(mes[0]) + mes.Substring(1);
            string dia = fechaDocumento.ToString("dd");

            string rutaCarpetas = Path.Combine(rutaBase, anio, mes, dia, tipoDocumento);
            Directory.CreateDirectory(rutaCarpetas);

            // Armamos el prefijo según el tipo (PEDI o COMP)
            string prefijo = tipoDocumento.Substring(0, 4).ToUpper();
            string nombreArchivo = $"{identificador}.pdf";

            return Path.Combine(rutaCarpetas, nombreArchivo);
        }
    }
    
}
