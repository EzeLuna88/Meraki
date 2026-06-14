using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace Meraki
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configurar cultura global a Argentina (es-AR)
            CultureInfo culture = new CultureInfo("es-AR");
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-AR");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-AR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            QuestPDF.Settings.License = LicenseType.Community;
            try
            {
                // Ponemos la ruta completa según tu namespace de datos (seguramente AccesoDAL)
                AccesoDAL.MigradorBaseDatos migrador = new AccesoDAL.MigradorBaseDatos();
                migrador.VerificarYActualizarEstructura();
            }
            catch (Exception ex)
            {
                // Si la base de datos de la distribuidora está caída, avisa y frena el arranque
                MessageBox.Show($"{ex.Message}\nEl sistema no puede arrancar sin conexión a la base de datos.",
                                "Error Crítico de Inicio", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return; // Corta la ejecución antes del Run
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PrincipalNuevo());
        }
    }
}
