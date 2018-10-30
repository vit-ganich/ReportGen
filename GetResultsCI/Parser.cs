using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GetResultsCI
{
    class Parser
    {
        // Temporary variable for groups dividing
        static string temp = "";
        public static void Parse()
        {
            Logger.Log.Info("Program started -----------------------------------------------------------------------");

            List<string[]> parsedFiles = null;

            int len = 0;

            parsedFiles = GetFileNamesListOfArraysSplitBySlash();

            if (parsedFiles == null)
            {
                throw new Exception("Test results directory is empty.");
            }

            len = parsedFiles[0].Length;
            // Report name equals the parent folder name (for instance: 10_22_2018)
            ReportWriter.ReportName = parsedFiles[0][len - 3];

            WriteTableHeader();

            Logger.Log.Info("Test results recording started.");
            try
            {
                foreach (var parsedFile in parsedFiles)
                {
                    #region Parts of parsed name of the trx-file
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
                    var skipped = tail[3].Split('.')[0]; // cut the .trx extension part
                    var result = (failed == "0") ? "PASSED" : "FAILED";
                    var errors = "-";
                    if (result.Equals("FAILED"))
                    {
                        errors = TRXreader.ExtractErrorStrings(parsedFile);
                    }
                    #endregion
                    string stringToWrite = $"{client},{CIgroup},{testName},{build},{passed},{failed},{skipped},{result},{errors}\n";
                    WriteWithSeparationByGroups(stringToWrite, CIgroup);
                }
            }
            catch
            {
                Logger.Log.Error("Unsupported format of TRX-filename.");
                throw new FormatException();
            }
            Logger.Log.Info($"All tasks were finished successfully, report file was created in {ConfigReader.GetReportFolder()} folder.");
        }

        public static List<string[]> GetFileNamesListOfArraysSplitBySlash()
        {
            var parsedFiles = new List<string[]>();

            var files = FolderScanner.ScanFolder();

            foreach (var file in files)
            {
                string[] parcedFile = file.Split('\\');
                parsedFiles.Add(parcedFile);
            }
            Logger.Log.Info("All filenames in the folder were splitted by '\\' and added in list succesfully.");

            return parsedFiles;
        }

        public static void WriteTableHeader()
        {
            ReportWriter.WriteToReportFile("Summary\n");
            ReportWriter.WriteToReportFile("QA_Client,CI Group,Test name,Build Version,Passed,Failed,Skipped,Result,Errors\n");
            Logger.Log.Info("Table header was successfully created.");
        }

        public static string RegExClient(string rawFileName)
        {
            Regex rx = new Regex(@"CLTQACLIENT\d*");
            Match match = rx.Match(rawFileName);
            return match.ToString();
        }

        /// <summary>
        /// For instanse: with rawFileName = 'xxxxx_B7.7.195.1-141-87-0-0.trx', returns the last four digits with dashes (141-87-0-0)
        /// </summary>
        /// <param name="rawFileName">Raw file name</param>
        /// <returns>Extracted string with digit sequense</returns>
        public static string RegExTail(string rawFileName)
        {
            Regex rx = new Regex(@"\d\d*-\d\d*-\d\d*-\d\d*.trx");
            Match match = rx.Match(rawFileName);
            return match.ToString();
        }

        /// <summary>
        /// For instanse: with rawFileName = 'xxxxx_B7.7.195.1-141-87-0-0.trx', returns the build version (B7.7.195.1)
        /// </summary>
        /// <param name="rawFileName">Raw file name</param>
        /// <returns>Extracted string with build version</returns>
        public static string RegExVersion(string rawFileName)
        {
            Regex rx = new Regex(@"B\d\d*.\d\d*.\d\d*.\d\d*");
            Match match = rx.Match(rawFileName);
            return match.ToString();
        }

        public static void WriteWithSeparationByGroups(string stringToWrite, string CIgroup)
        {
            // If the group is not equal to pervious group (temp) - write blank space
            if (!CIgroup.Equals(temp) && !temp.Equals(""))
            {
                ReportWriter.WriteToReportFile("\n");
            }
            ReportWriter.WriteToReportFile(stringToWrite);
            temp = CIgroup;

            Logger.Log.Debug("Test result was successfully recorded to the Report file:");
            Logger.Log.Debug(stringToWrite);
        }
    }
}
