using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                var resultsCIfolder = GetTheLastResultsFolder();
                Console.WriteLine($"CI results folder: {resultsCIfolder}");
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

        public static string GetTheLastResultsFolder()
        {
            var parentDir = ConfigReader.GetTestResultsDir();
            var dateFormat = ConfigReader.GetDateTimeFormat();
            int searchRange = 10; // days from today for search, if the results folder for today is absent
            for (int i = 0; i > -searchRange; i--)
            {
                try
                {
                    return Directory.GetDirectories(parentDir, DateTime.Now.AddDays(i).ToString(dateFormat))[0];
                }
                catch { }
            }
            return null;
        }
    }
}
