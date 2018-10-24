using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetResultsCI
{
    class Parser
    {
        public static void Parse()
        {
            var parsedFiles = GetFileNamesListOfArraysSplitBySlash();
            var len = parsedFiles[0].Length;

            // Report name equals the parent folder name (for instance: 10_22_2018)
            Logger.ReportName = parsedFiles[0][len - 3];

            // Write a table header
            Logger.WriteToReportFile(ConfigReader.GetTableHeader() + "\n");

            foreach (var parsedFile in parsedFiles)
            {
                var rawCIgroupClient = parsedFile[len - 2];
                var client = RegExClient(rawCIgroupClient);
                var CIgroup = rawCIgroupClient.Replace("_" + client, "");
                var rawBody = parsedFile[len - 1];
                var rawTail = RegExTail(rawBody);
                var tail = rawTail.Split('-');
                var rawTestNameBuild = rawBody.Replace("-" + rawTail, "");
                var build = RegExVersion(rawTestNameBuild);
                var testName = rawTestNameBuild.Replace("_" + build, "");
                //var time = tail[0]; // not nesessary
                var passed = tail[1];
                var failed = tail[2];
                var skipped = tail[3].Split('.')[0];
                var result = (failed == "0") ? "PASSED" : "FAILED";

                string stringToWrite = $"{client},{CIgroup},{testName},{build},{passed},{failed},{skipped},{result}\n";

                // Write a finalized string
                Logger.WriteToReportFile(stringToWrite);
                
            }
        }
        public static List<string[]> GetFileNamesListOfArraysSplitBySlash()
        {
            var files = FolderScanner.ScanFolder();
            var parsedFiles = new List<string[]>();
            foreach (var file in files)
            {
                string[] parcedFile = file.Split('\\');
                parsedFiles.Add(parcedFile);
            }
            return parsedFiles;
        }

        /// <summary>
        /// For instanse: with rawFileName = 'xxxxx_7.7.195.1-141-87-0-0.trx', returns the last four digits with dashes (141-87-0-0)
        /// </summary>
        /// <param name="rawFileName">Raw file name</param>
        /// <returns>Extracted string</returns>
        public static string RegExTail(string rawFileName)
        {
            Regex rx = new Regex(@"\d\d*-\d\d*-\d\d*-\d\d*.trx");

            Match match = rx.Match(rawFileName);
            return match.ToString();
        }

        public static string RegExClient(string rawFileName)
        {
            Regex rx = new Regex(@"CLTQACLIENT\d*");

            Match match = rx.Match(rawFileName);
            return match.ToString();
        }

        public static string RegExVersion(string rawFileName)
        {
            Regex rx = new Regex(@"B\d\d*.\d\d*.\d\d*.\d\d*");

            Match match = rx.Match(rawFileName);
            return match.ToString();
        }
    }
}
