using System;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace GetResultsCI
{
    class Postman
    {
        public static void EmailSend()
        {
            Logger.Log.Debug("SMTP email settings - checking data format.");

            #region Email settings
            string server = ConfigReader.GetSmtpServerPort()[0];
            int port = Convert.ToInt32(ConfigReader.GetSmtpServerPort()[1]);
            bool isSslEnabled = ConfigReader.GetSslEnabled();
            string userMail = ConfigReader.GetSmtpServerCredentials()[0];
            string userPass = ConfigReader.GetSmtpServerCredentials()[1];
            string mailTo = ConfigReader.GetSmtpMailTo();
            string mailBody = ConfigReader.GetSmtpMailBody();
            #endregion

            Logger.Log.Debug("SMTP email settings have a correct data format.");

            SmtpClient SmtpServer = new SmtpClient(server);
            SmtpServer.Port = port;
            SmtpServer.EnableSsl = isSslEnabled;
            SmtpServer.Credentials = new NetworkCredential(userMail, userPass);

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(userMail);
            mail.To.Add(mailTo);
            mail.Subject = $"CI summary {ReportWriter.ReportName}";
            mail.Body = mailBody;

            var pathToAttachFile = Path.Combine(ReportWriter.ReportFolder, ReportWriter.ReportName + "." + ConfigReader.GetReportFileExtension());
            var attachment = new Attachment(pathToAttachFile);
            mail.Attachments.Add(attachment);
            Logger.Log.Info("SMTP email attachment was created successfully.");

            SmtpServer.Send(mail);
            Logger.Log.Info("Email was sent successfully.");
        }
    }
}
