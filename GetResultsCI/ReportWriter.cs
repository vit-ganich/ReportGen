using System;
using System.IO;

namespace GetResultsCI
{
    class ReportWriter
    {
        public static string ReportFolder { get; set; }
        public static string ReportName { get; set; }

        public static void WriteToReportFile(string message)
        {
            ReportFolder = CreateReportFolder();
            var reportExtension = ConfigReader.GetReportFileExtension();
            var reportFile = string.Format($"{ReportName}.{reportExtension}");

            if (ReportFolder != null)
            {
                using (StreamWriter file =
                new StreamWriter(Path.Combine(ReportFolder, reportFile), true))
                {
                    file.Write(message);
                }
            }
            else
            {
                Console.WriteLine("Error while report file creation.");
                throw new FileNotFoundException();
            }
        }
        public static string CreateReportFolder(string reportFolder="C:\\")
        {
            try
            {
                reportFolder = ConfigReader.GetReportFolder();
                if (!Directory.Exists(reportFolder))
                {
                    var myDir = Directory.CreateDirectory(reportFolder);
                    Console.WriteLine(string.Format("\nLog file folder: {0} was succesfully created...\n", reportFolder));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Report folder creation failure. File will be saved in C:\\ root");
                throw;
            }
            return reportFolder;
        }

        public static string GetCurrentTime()
        {
            return DateTime.Now.ToString(ConfigReader.GetDateTimeFormat());
        }
    }
}
