using cb_ejercicio2.Models;
using System.IO.Compression;

namespace cb_ejercicio2.Logic
{
    public class ZipLogic
    {
        private string FilesLocation;
        private string ZipsLocation;
        private string ZipName;


        public ZipLogic(IWebHostEnvironment env, string zipName)
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
            string folder = config["Paths:FilePath"];
            string zips = config["Paths:ZipsPath"];
            FilesLocation = Path.Combine(env.ContentRootPath, folder);
            ZipsLocation = Path.Combine(env.ContentRootPath, zips);
            ZipName = zipName;
        }
        public void CreateZipFromFiles(List<CbtProcesodecarga> files)
        {
            string[] paths = files.Select(f=>f.NombreArchivoRespaldo).ToArray();
            if (!Directory.Exists(FilesLocation))
            {
                throw new DirectoryNotFoundException($"No se encontró la carpeta {FilesLocation}");
            }
            List<string> filesToZip = new List<string>();
            foreach (string file in paths)
            {
                string rutaArchivo = Path.Combine(FilesLocation, file);
                if (File.Exists(rutaArchivo))
                {
                    filesToZip.Add(rutaArchivo);
                }
                
            }

            Directory.CreateDirectory(ZipsLocation);
            string zipCreationPath = Path.Combine(ZipsLocation, ZipName);

            using (FileStream archivoZipStream = new FileStream(zipCreationPath, FileMode.Create))
            {
                using (ZipArchive archivoZip = new ZipArchive(archivoZipStream, ZipArchiveMode.Create))
                {
                    foreach (string fileToZip in filesToZip)
                    {
                        string nombreArchivo = Path.GetFileName(fileToZip);
                        archivoZip.CreateEntryFromFile(fileToZip, nombreArchivo, CompressionLevel.Optimal);
                    }
                }
            }

        }
    }
}
