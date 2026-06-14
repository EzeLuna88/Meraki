using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meraki
{
    public partial class UC_ConfigRutaArchivos : UserControl
    {
        public UC_ConfigRutaArchivos()
        {
            InitializeComponent();
        }

        private void UC_ConfigRutaArchivos_Load(object sender, EventArgs e)
        {
            // Al abrir la pantalla, mostramos la ruta que ya estaba guardada (si existe)
            string rutaGuardada = Properties.Settings.Default.CarpetaDestinoPDF;

            if (!string.IsNullOrEmpty(rutaGuardada))
            {
                textBoxRutaBase.Text = rutaGuardada;
            }
            else
            {
                textBoxRutaBase.Text = "No hay una carpeta configurada todavía...";
            }
        }

        private void iconButtonExaminar_Click(object sender, EventArgs e)
        {
            // Instanciamos el cuadro de diálogo nativo de Windows para buscar carpetas
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Seleccioná la carpeta raíz donde Meraki guardará los PDFs";

                // Si el usuario elige una carpeta y le da a "Aceptar"
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    // 1. Mostramos la nueva ruta en el TextBox
                    textBoxRutaBase.Text = fbd.SelectedPath;

                    // 2. Guardamos físicamente la ruta en las Properties
                    Properties.Settings.Default.CarpetaDestinoPDF = fbd.SelectedPath;
                    Properties.Settings.Default.Save(); // Clave para que no se borre al cerrar

                    MessageBox.Show("¡La carpeta de destino se configuró correctamente!",
                                    "Configuración Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
