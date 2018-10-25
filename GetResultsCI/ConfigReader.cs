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
        public static string GetTestResultsDir()
        {
            return ConfigurationManager.AppSettings["TestResultsDir"];
        }
        public static string GetFilesExtension()
        {
            return ConfigurationManager.AppSettings["FilesExtension"];
        }

        public static string GetDateTimeFormat()
        {
            return ConfigurationManager.AppSettings["DateTimeFormat"];
        }

        public static string GetReportFolder()
        {
            return ConfigurationManager.AppSettings["ReportFolder"];
        }

        public static string GetReportFileExtension()
        {
            return ConfigurationManager.AppSettings["ReportFileExtension"];
        }

        public static string GetTableHeader()
        {
            return ConfigurationManager.AppSettings["TableHeader"];
        }

        public static int GetErrorsCount()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["CountOfErrorsToInclude"]);
        }
    }
}
