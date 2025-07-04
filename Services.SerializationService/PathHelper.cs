using System;
using System.IO;

namespace NotebookMVVM.Services.SerializationService
{
    public static class PathHelper
    {
        public static string GetAppDataPath(string fileName)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string appDataDir = Path.Combine(baseDir, "AwpAppData");

            if (!Directory.Exists(appDataDir))
                Directory.CreateDirectory(appDataDir);

            return Path.Combine(appDataDir, fileName);
        }
    }
}
