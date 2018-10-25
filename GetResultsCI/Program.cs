using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace GetResultsCI
{
    class Program
    {
        static void Main(string[] args)
        {
           Parser.Parse();
           //string path = "\\\\w10x64-29\\Test Results\\CI\\10_23_2018\\CEP CM_1_CLTQACLIENT433\\90.22-MPCM Reminders_01_Oracle_Chrome_B7.7.18295.1-81-52-5-6.trx";
        }
    }
}
