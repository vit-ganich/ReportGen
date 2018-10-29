using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static string GetDateTimeFormat()
        {
            return ConfigurationManager.AppSettings["DateTimeFormat"];
        }
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
    }
}
