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
                string server = ConfigReader.GetSmtpServerPort()[0];
                int port = Convert.ToInt32(ConfigReader.GetSmtpServerPort()[1]);
                bool isSslEnabled = ConfigReader.GetSslEnabled();
                string userMail = ConfigReader.GetSmtpServerCredentials()[0];
                string userPass = ConfigReader.GetSmtpServerCredentials()[1];
                string mailFrom = ConfigReader.GetSmtpMailFrom();
                string mailTo = ConfigReader.GetSmtpMailTo();
                string mailBody = ConfigReader.GetSmtpMailBody();

                SmtpClient SmtpServer = new SmtpClient(server);
                SmtpServer.Port = port;
                SmtpServer.EnableSsl = isSslEnabled;
                SmtpServer.Credentials = new NetworkCredential(userMail, userPass);
                Logger.Log.Info("SMTP server settings were applied successfully.");

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(mailFrom);
                mail.To.Add(mailTo);
                mail.Subject = $"CI summary {ReportWriter.ReportName}";
                mail.Body = mailBody;
                Logger.Log.Info("SMTP email message was created successfully.");

                var pathToAttachFile = Path.Combine(ReportWriter.ReportFolder, ReportWriter.ReportName + "." + ConfigReader.GetReportFileExtension());
                var attachment = new Attachment(pathToAttachFile);
                mail.Attachments.Add(attachment);
                Logger.Log.Info("SMTP email attachment was created successfully.");

                SmtpServer.Send(mail);
                Logger.Log.Info("Email was sent successfully.");
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                Logger.Log.Error(ex.StackTrace);
                throw;
            }
        }
    }
}
