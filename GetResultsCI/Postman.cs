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
            string server = ConfigReader.SmtpServerPort[0];
            int port = Convert.ToInt32(ConfigReader.SmtpServerPort[1]);
            bool isSslEnabled = ConfigReader.IsSslEnabled;
            string userMail = ConfigReader.SmtpServerCredentials[0];
            string userPass = ConfigReader.SmtpServerCredentials[1];
            string mailTo = ConfigReader.SmtpMailTo;
            string mailBody = ConfigReader.SmtpMailBody;
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

            var pathToAttachFile = Path.Combine(ReportWriter.ReportFolder, ReportWriter.ReportName + "." + ConfigReader.ReportFileExtension);
            var attachment = new Attachment(pathToAttachFile);
            mail.Attachments.Add(attachment);
            Logger.Log.Info("SMTP email attachment was created successfully.");

            Logger.Log.Info("Email sending, please, wait...");
            SmtpServer.Send(mail);
            Logger.Log.Info("Email was successfully sent to all recipients.");
        }
    }
}
