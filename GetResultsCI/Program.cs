using System;
using System.Collections.Generic;
using System.Text;

namespace GetResultsCI
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<string[]> parsedFiles = Parser.GetFileNamesListOfArraysSplitBySlash();

                int len = parsedFiles[0].Length;

                StringBuilder textToWrite = new StringBuilder();

                textToWrite.Append(Parser.GetReportNameAndWriteTableHeader(parsedFiles, len));

                foreach (var parsedFile in parsedFiles)
                {
                    string[] strToWrite = Parser.Parse(parsedFile, len);
                    textToWrite.Append(ReportWriter.WriteWithSeparationByGroups(strToWrite));
                }

                ReportWriter.WriteToReportFile(textToWrite.ToString());

                Logger.Log.Info($"All tasks were finished successfully, report file was created in {ConfigReader.ReportFolder} folder.");

                if (ConfigReader.IsSmtpEnabled == true) { Postman.EmailSend(); }

                else { Logger.Log.Info("SMTP emails disabled in App.config, email wasn't sent."); }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
            }
        }
    }
}