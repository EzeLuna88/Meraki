using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios
{
    public static class GoogleDriveService
    {
        public static async Task ActualizarCatalogoEnDrive(string idArchivoDrive, string rutaLocalPdf)
        {
            // 1. Buscamos las credenciales maestras
            string rutaCredenciales = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "credentials.json");
            string rutaToken = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Drive.Api.Auth.Store");

            if (!File.Exists(rutaCredenciales))
                throw new FileNotFoundException("No se encontró el archivo credentials.json en Resources.");

            string[] permisos = { DriveService.Scope.Drive };
            UserCredential credencial;

            // 2. Leemos el token que el usuario ya autorizó en la configuración
            using (var stream = new FileStream(rutaCredenciales, FileMode.Open, FileAccess.Read))
            {
                credencial = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    permisos,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(rutaToken, true)
                );
            }

            // 3. Inicializamos el servicio oficial de Google Drive
            var servicio = new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credencial,
                ApplicationName = "Meraki"
            });

            // 4. PREPARACIÓN DEL REEMPLAZO (Metadatos vacíos porque solo pisamos el contenido)
            var archivoMeta = new Google.Apis.Drive.v3.Data.File();

            using (var streamArchivo = new FileStream(rutaLocalPdf, FileMode.Open))
            {
                // Creamos la solicitud de actualización (Update) apuntando al ID del Linktree
                var solicitud = servicio.Files.Update(archivoMeta, idArchivoDrive, streamArchivo, "application/pdf");

                // Ejecutamos la subida asíncrona a la nube
                var resultado = await solicitud.UploadAsync();

                // Si la subida falla por algún motivo (ej: borraron el archivo de Drive a mano), tiramos el error
                if (resultado.Status == Google.Apis.Upload.UploadStatus.Failed)
                {
                    throw new Exception("Google Drive rechazó la subida: " + resultado.Exception?.Message);
                }
            }
        }
    }
}
