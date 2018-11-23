using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace GetResultsCI
{
    class TRXreader
    {
        public static string GetErrorMessages(string[] splittedFileName)
        {
            XmlDocument xDoc = ReadErrorReport(splittedFileName);
            StringBuilder errorString = new StringBuilder();
            int count = 0; // counter for limitation of the errors number

            try
            {
                // Awful nested foreach's, but I can't come up with elegant solution
                foreach (XmlNode xnode in xDoc.DocumentElement)
                {
                    foreach (XmlNode firstChild in xnode.ChildNodes)
                    {
                        XmlAttribute testName = firstChild.Attributes["testName"];

                        foreach (XmlNode secondChild in firstChild.ChildNodes)
                        {
                            foreach (XmlNode thirdChild in secondChild.ChildNodes)
                            {
                                if (thirdChild.Name.Equals("ErrorInfo"))
                                {
                                    // To avoid the report encumbering, the number of errors must be limited
                                    if (count == ConfigReader.ErrorsCount) { break; }

                                    foreach (XmlNode forthChild in thirdChild.ChildNodes)
                                    {
                                        if (forthChild.Name.Equals("Message"))
                                        {
                                            // Skip the Inconclusive steps
                                            if (!forthChild.InnerText.StartsWith("Assert.Inconclusive failed."))
                                            {
                                                string testNumber = ExtractTestNumber(testName.InnerText);

                                                string errorMessage = ExtractErrorMessage(forthChild.InnerText);

                                                errorString.Append(string.Format("{0} - {1}\t", testNumber, errorMessage));
                                                count++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Logger.Log.Debug("Errors extracting was finished successfully.");
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                Logger.Log.Error(ex.StackTrace);
            }
            return errorString.ToString();
        }

        public static string ExtractTestNumber(string testName)
        {
            Regex rx = new Regex(@"(_\d\d*_\d\d*_\d\d*_\d\d*)|(_\d\d*_\d\d*_\d\d*)");
            Match match = rx.Match(testName);
            return match.ToString();
        }

        public static string ExtractErrorMessage(string rawString)
        {
            string errorMessage = rawString.Replace("Assert.IsTrue failed. ", "");
            int lengthLimit = 90;
            // Long error message usually indicates, that the method trew exception
            if (errorMessage.Length >= lengthLimit)
            {
                Regex ex = new Regex(@"System.*");
                Match match = ex.Match(errorMessage);

                if (match.Length != 0) { errorMessage = match.ToString(); }

                else { errorMessage = errorMessage.Substring(0, lengthLimit); }
            }
            return errorMessage.Trim();
        }

        public static XmlDocument ReadErrorReport(string[] splittedFileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                // For network folder
                string pathToFIle = string.Join("\\", splittedFileName);

                // For local folder on a disk
                if (!splittedFileName[0].Contains(":")) { pathToFIle = "\\\\" + pathToFIle; }

                xmlDocument.Load(pathToFIle);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                Logger.Log.Error(ex.StackTrace);
            }
            return xmlDocument;
        }
    }
}