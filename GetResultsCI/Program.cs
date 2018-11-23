using System;
using System.Text;

namespace GetResultsCI
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var parsedFiles = Parser.GetFileNamesListOfArraysSplitBySlash();

                int len = parsedFiles[0].Length;

                Parser.GetReportNameAndWriteTableHeader(parsedFiles, len);

                StringBuilder textToWrite = new StringBuilder();

                foreach (var parsedFile in parsedFiles)
                {
                    string[] strToWrite = Parser.Parse(parsedFile, len);
                    textToWrite.Append(ReportWriter.WriteWithSeparationByGroups(strToWrite));
                }

                ReportWriter.WriteToReportFile(textToWrite.ToString());

                Logger.Log.Info($"All tasks were finished successfully, report file was created in {ConfigReader.ReportFolder} folder.");

                Postman.EmailSend();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
            }
        }
    }
}