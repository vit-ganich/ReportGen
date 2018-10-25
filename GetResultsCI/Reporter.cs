using System;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace GetResultsCI
{
    class Reporter
    {
        public static string ReportName { get; set; }
          
        public static void WriteToReportFile(string message)
        {
            var reportFolder = CreateReportFolder();
            var reportExtension = ConfigReader.GetReportFileExtension();
            var reportFile = string.Format($"{ReportName}.{reportExtension}");

            if (reportFolder != null)
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(Path.Combine(reportFolder, reportFile), true))
                {
                    file.Write(message);
                }
            }
            else
            {
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
