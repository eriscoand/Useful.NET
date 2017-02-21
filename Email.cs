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
    
     public static void SimpleEmail(string subject, string message, string receiver)
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
