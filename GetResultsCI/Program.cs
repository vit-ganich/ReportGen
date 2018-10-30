using System;

namespace GetResultsCI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Parser.Parse();

                Postman.EmailSend();
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex);
            }
        }
    }
}
