using System;
using System.Configuration;
using static System.Configuration.ConfigurationManager;

namespace GetResultsCI
{
    class ConfigReader
    {
        #region Test results folder settings
        public static string TestResultsDir => AppSettings["TestResultsDir"];

        public static string FilesExtension => AppSettings["FilesExtension"];
        #endregion
        
        #region Report file settings
        public static string ReportFolder => AppSettings["ReportFolder"];

        public static string ReportFileExtension => AppSettings["ReportFileExtension"].ToLower();

        public static int ErrorsCount => Convert.ToInt32(AppSettings["CountOfErrorsToInclude"]);

        public static string ExcelFormula => AppSettings["ExcelFormula"];
        #endregion

        #region Email settings
        public static string[] SmtpServerPort => AppSettings["SmtpServerPort"].Split(',');
 
        public static string[] SmtpServerCredentials => AppSettings["SmtpServerCredentials"].Split(',');

        public static bool IsSslEnabled => Convert.ToBoolean(AppSettings["SmtpServerEnableSsl"]);

        public static string SmtpMailTo => AppSettings["SmtpMailTo"];

        public static string SmtpMailBody => AppSettings["SmtpMailBody"];
        #endregion

        #region Date and time settings
        public static string DateTimeFormat => AppSettings["DateTimeFormat"];
        #endregion
    }
}
