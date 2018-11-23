using System;
using System.IO;

namespace GetResultsCI
{
    class ReportWriter
    {
        public static string ReportFolder { get; set; }
        public static string ReportName { get; set; }

        public static string WriteWithSeparationByGroups(string[] textAndCIgroup)
        {
            var stringToWrite = textAndCIgroup[0];
            var CIgroup = textAndCIgroup[1];

            // If the group is not equal to pervious group (temp) - add blank space for better readability
            if (!CIgroup.Equals(Parser.temp) && !Parser.temp.Equals("")) { stringToWrite = "\n" + stringToWrite; }

            Parser.temp = CIgroup;

            Logger.Log.Debug("Test result was successfully extracted");
            Logger.Log.Debug(stringToWrite);

            return stringToWrite.Replace("\r", "");
        }

        public static void WriteToReportFile(string message)
        {
            ReportFolder = CreateReportFolder();

            var reportExtension = ConfigReader.ReportFileExtension;

            var reportFile = string.Format($"{ReportName}.{reportExtension}");

            if (ReportFolder != null)
            {
                File.WriteAllText(Path.Combine(ReportFolder, reportFile), message);
            }
            else
            {
                Logger.Log.Error("Error while report file creation.");
                throw new FileNotFoundException();
            }
        }

        public static string CreateReportFolder(string reportFolder="C:\\")
        {
            try
            {
                reportFolder = ConfigReader.ReportFolder;

                if (!Directory.Exists(reportFolder))
                {
                    var myDir = Directory.CreateDirectory(reportFolder);
                    Logger.Log.Info((string.Format("\nLog file folder: {0} was succesfully created...\n", reportFolder)));
                }
            }
            catch (Exception)
            {
                Logger.Log.Error("Report folder creation failure. File will be saved in C:\\ root");
                throw;
            }
            return reportFolder;
        }
    }
}
