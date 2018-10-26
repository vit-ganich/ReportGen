using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace GetResultsCI
{
    class PostOffice
    {
        public static void EmailSend()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.yandex.ru");
                mail.From = new MailAddress("******@yandex.ru");
                mail.To.Add("*************");
                mail.Subject = $"CI summary {ReportWriter.ReportName}";
                mail.Body = "This is email with auto-generated Ci summary.";

                var pathToAttachFile = Path.Combine(ReportWriter.ReportFolder, ReportWriter.ReportName + "." + ConfigReader.GetReportFileExtension());
                var attachment = new Attachment(pathToAttachFile);
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new NetworkCredential("*********@yandex.ru", "************");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                Console.WriteLine("Email was sent successfully.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error while email sending.");
                throw;
            }
        }
    }
}
