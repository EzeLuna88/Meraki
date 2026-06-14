using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meraki
{
    public partial class UC_ConfigDrive : UserControl
    {
        public UC_ConfigDrive()
        {
            InitializeComponent();
        }

        private void UC_ConfigDrive_Load(object sender, EventArgs e)
        {
            // 1. Cargamos el ID si ya estaba guardado de antes
            string idGuardado = Properties.Settings.Default.GoogleDriveFileId;
            if (!string.IsNullOrEmpty(idGuardado))
            {
                textBoxDriveID.Text = idGuardado;
            }

            // 2. Chequeo visual rápido: ¿Existe el token mágico de Google?
            // (Google guarda automáticamente un token en esta ruta interna de Windows cuando te logueás)
            string rutaCarpetaToken = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Drive.Api.Auth.Store");

            if (Directory.Exists(rutaCarpetaToken) && Directory.GetFiles(rutaCarpetaToken).Length > 0)
            {
                labelEstadoDrive.Text = "Estado: Conectado ✔️";
                labelEstadoDrive.ForeColor = System.Drawing.Color.SeaGreen;
            }
            else
            {
                labelEstadoDrive.Text = "Estado: Desconectado ❌";
                labelEstadoDrive.ForeColor = System.Drawing.Color.Crimson;
            }
        }


        private void iconButtonGuardar_Click(object sender, EventArgs e)
        {
            // Validamos que no metan espacios en blanco por error
            string idLimpio = textBoxDriveID.Text.Trim();

            if (string.IsNullOrEmpty(idLimpio))
            {
                MessageBox.Show("Por favor, ingresá un ID válido de Google Drive.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Guardamos físicamente en Windows
            Properties.Settings.Default.GoogleDriveFileId = idLimpio;
            Properties.Settings.Default.Save();

            MessageBox.Show("¡El ID del catálogo se guardó correctamente!\n\nEl sistema actualizará este archivo en la próxima subida.",
                            "Configuración Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void iconButtonVincularDrive_Click(object sender, EventArgs e)
        {
            try
            {
                // Avisamos visualmente que estamos procesando
                labelEstadoDrive.Text = "Estado: Conectando...";
                labelEstadoDrive.ForeColor = System.Drawing.Color.Orange;

                // 1. Buscamos la chapa patente en la carpeta Resources
                string rutaCredenciales = Path.Combine(Application.StartupPath, "Resources", "credentials.json");

                if (!File.Exists(rutaCredenciales))
                {
                    MessageBox.Show("No se encontró el archivo credentials.json. Asegurate de que esté en la carpeta Resources y copiado al directorio de salida.", "Falta archivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    labelEstadoDrive.Text = "Estado: Desconectado ❌";
                    labelEstadoDrive.ForeColor = System.Drawing.Color.Crimson;
                    return;
                }

                // 2. Pedimos permiso total sobre Drive para poder pisar el PDF oficial
                string[] permisos = { DriveService.Scope.Drive };
                UserCredential credencial;

                using (var stream = new FileStream(rutaCredenciales, FileMode.Open, FileAccess.Read))
                {
                    // La carpeta donde Windows va a guardar el token mágico silenciosamente
                    string rutaToken = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Drive.Api.Auth.Store");

                    // 3. ¡LA MAGIA! Esto abre Chrome si no hay token, o lo lee directo si ya existe
                    credencial = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        permisos,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(rutaToken, true)
                    );
                }

                // 4. Si llegamos acá, es porque el usuario dio el OK en Chrome
                labelEstadoDrive.Text = "Estado: Conectado ✔️";
                labelEstadoDrive.ForeColor = System.Drawing.Color.SeaGreen;

                MessageBox.Show("¡Vinculación exitosa!\n\nMeraki ya tiene permiso para actualizar el catálogo en la nube.",
                                "Conexión a Drive", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con Google Drive: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelEstadoDrive.Text = "Estado: Desconectado ❌";
                labelEstadoDrive.ForeColor = System.Drawing.Color.Crimson;
            }
        }
    }
}
