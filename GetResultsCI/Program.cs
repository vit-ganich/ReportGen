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
            try
            {
                Parser.Parse();
                PostOffice.EmailSend();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
