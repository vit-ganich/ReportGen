using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetResultsCI
{
    class TRXreader
    {
        /// <summary>
        /// Method reads the file and returns the string with errors
        /// </summary>
        /// <param name="splittedFileName">String array, consisted of parts of splitted file path</param>
        /// <returns>String with comma-separated errors</returns>
        public static string ExtractErrorStrings(string[] splittedFileName)
        {
            var TRXfileContent = ReadErrorReport(splittedFileName);

            string extractedErrors = null;
            try
            {
                int count = 0;
                foreach (Match error in ErrorsToInclude(TRXfileContent))
                {
                    // To avoid the report encumbering, the number of errors must be limited
                    if (count == ConfigReader.GetErrorsCount()) { break; }

                    var strError = error.ToString();

                    // Only proper error messages will be encluded in report
                    if (!CheckErrorToExclude(strError))
                    {
                        //old variant covered not all error messages
                        //var temp = strError.Replace("<Message>", "");
                        //temp = temp.Replace("</Message>", "") + ",";
                        //---------------------------------------------
                        var temp = strError.Replace("-&gt; error: ", "");
                        temp = temp.Replace("</StdOut>", "");
                        temp = temp.Replace("\r", ",");
                        extractedErrors += temp;
                        count++;
                    }
                }
                Logger.Log.Info("Errors extracting was finished successfully.");
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message);
                Logger.Log.Error(ex.StackTrace);
            }
            return extractedErrors;
        }

        public static bool CheckErrorToExclude(string error)
        {
            // This errors caused by skipped steps and must be skipped too
            Regex exclude = new Regex("Assert.Inconclusive failed.");
            return exclude.IsMatch(error);
        }

        public static MatchCollection ErrorsToInclude(string TRXfileContent)
        {
            // Error messages are enclosed in <Message> tags
            // old variant covered not all error messages
            //Regex include = new Regex("<Message>(.+?)</Message>");
            //------------------------------------------------------
            Regex include = new Regex("-&gt; error:(.+?)*.");
            return include.Matches(TRXfileContent);
        }

        /// <summary>
        /// Method joins splitted items of array to create full path to file
        /// </summary>
        /// <param name="splittedFileName"></param>
        /// <returns>String full path</returns>
        public static string JoinAbsFilePAth(string[] splittedFileName)
        {
            return string.Join("\\", splittedFileName);
            //return Path.Combine(splittedFileName);
        }

        /// <summary>
        /// Method takes the full path to file and returns a string with file content
        /// </summary>
        /// <param name="splittedFileName"></param>
        /// <returns>String file content</returns>
        public static string ReadErrorReport(string[] splittedFileName)
        {
            string TRXfileContent = "Error while reading the TRX-file.";
            try
            {
                string pathToFIle;
                if (!splittedFileName[0].Contains(":"))
                {
                    pathToFIle = "\\\\" + JoinAbsFilePAth(splittedFileName);
                }
                else
                {
                    pathToFIle = JoinAbsFilePAth(splittedFileName);
                }  
                TRXfileContent = File.ReadAllText(pathToFIle);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                Logger.Log.Error(ex.StackTrace);
            }
            return TRXfileContent;
        }
    }
}
