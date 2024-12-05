using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Servicios
{
    public static class PathManager
    {
        public static string GetAppDataPath()
        {
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appFolder = Path.Combine(localAppDataPath, "Meraki");

            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            return appFolder;
        }

        public static string GetFilePath(string fileName)
        {
            return Path.Combine(GetAppDataPath(), fileName);
        }
    }
}
