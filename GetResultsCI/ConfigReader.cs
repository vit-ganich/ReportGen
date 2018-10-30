using System;
using System.Configuration;

namespace GetResultsCI
{
    class ConfigReader
    {
        #region Test results folder settings
        public static string GetTestResultsDir()
        {
            return ConfigurationManager.AppSettings["TestResultsDir"];
        }
        public static string GetFilesExtension()
        {
            return ConfigurationManager.AppSettings["FilesExtension"];
        }
        #endregion
        
        #region Report file settongs
        public static string GetReportFolder()
        {
            return ConfigurationManager.AppSettings["ReportFolder"];
        }

        public static string GetReportFileExtension()
        {
            return ConfigurationManager.AppSettings["ReportFileExtension"];
        }
        public static int GetErrorsCount()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["CountOfErrorsToInclude"]);
        }
        #endregion

        #region Email settings
        public static string[] GetSmtpServerPort()
        {
            return ConfigurationManager.AppSettings["SmtpServerPort"].Split(',');
        }
        public static string[] GetSmtpServerCredentials()
        {
            return ConfigurationManager.AppSettings["SmtpServerCredentials"].Split(',');
        }
        public static bool GetSslEnabled()
        {
            return Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpServerEnableSsl"]);
        }
        public static string GetSmtpMailFrom()
        {
            return ConfigurationManager.AppSettings["SmtpMailFrom"];
        }
        public static string GetSmtpMailTo()
        {
            return ConfigurationManager.AppSettings["SmtpMailTo"];
        }

        public static string GetSmtpMailBody()
        {
            return ConfigurationManager.AppSettings["SmtpMailBody"];
        }
        #endregion

        #region Date and time settings
        public static string GetDateTimeFormat()
        {
            return ConfigurationManager.AppSettings["DateTimeFormat"];
        }
        #endregion
    }
}
