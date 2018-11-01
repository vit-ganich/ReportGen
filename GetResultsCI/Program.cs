using System;

namespace GetResultsCI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var parsedFiles = Parser.GetFileNamesListOfArraysSplitBySlash();

                int len = parsedFiles[0].Length;
                
                Parser.GetReportNameAndWriteTableHeader(parsedFiles, len);

                foreach (var parsedFile in parsedFiles)
                {
                    var strToWrite = Parser.Parse(parsedFile, len);

                    Parser.WriteWithSeparationByGroups(strToWrite);
                }
                Logger.Log.Info($"All tasks were finished successfully, report file was created in {ConfigReader.GetReportFolder()} folder.");

                Postman.EmailSend();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
            }
        }
    }
}