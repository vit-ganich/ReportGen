using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetResultsCI
{
    class FolderScanner
    {
        public static List<string> ScanFolder()
        {
            var filesInFolder = new List<string>();
            try
            {
                var resultsCIfolder = ConfigReader.GetTestResultsDir();
                var extension = string.Format($"*.{ConfigReader.GetFilesExtension()}");

                Console.WriteLine("Scanning the folder... Please, wait...");
                filesInFolder.AddRange(Directory.GetFiles(
                     resultsCIfolder, extension, SearchOption.AllDirectories));
            }
            catch (Exception)
            {
                throw;
            }

            return filesInFolder;
        }
    }
}
