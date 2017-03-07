using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

static class Email
{

    public static void Send(string subject, string message, string receiver, string copyTo = "", string replyTo = "", string attachment_path = "")
     {
            message = "<html><body>" + message + "</body></html>";

            string email_from = WebConfigurationManager.AppSettings["email_from"];
            string email_name = WebConfigurationManager.AppSettings["email_name"];

            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            MailAddress mailAddress = new MailAddress(email_from, email_name);

            mail.To.Add(receiver);
            mail.From = mailAddress;
            mail.Subject = subject;
            mail.Body = message;

            if (!String.IsNullOrWhiteSpace(copyTo))
            {
                mail.CC.Add(copyTo);
            }

            if (!String.IsNullOrWhiteSpace(replyTo))
            {
                mail.ReplyToList.Add(replyTo);
            }

            if (!String.IsNullOrWhiteSpace(attachment_path))
            {
                // Create  the file attachment for this e-mail message.
                Attachment data = new Attachment(attachment_path, MediaTypeNames.Application.Octet);
                // Add time stamp information for the file.
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(attachment_path);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(attachment_path);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(attachment_path);
                disposition.DispositionType = DispositionTypeNames.Attachment;
                // Add the file attachment to this e-mail message.
                mail.Attachments.Add(data);
            }

            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            smtp.Host = WebConfigurationManager.AppSettings["smtp_host"];

            NetworkCredential basicCredential = new NetworkCredential(WebConfigurationManager.AppSettings["smtp_user"], WebConfigurationManager.AppSettings["smtp_password"]);

            smtp.Credentials = basicCredential;

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Log.SimpleLog(ex.Message.ToString());
            }

        }



}
