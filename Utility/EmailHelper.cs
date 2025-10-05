using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace EmployeeDetailsWithTab.Utility
{
    public class EmailHelper
    {
        public static void SendEmail(string toEmail, string subject, string body)
        {

            string Host = "smtp.gmail.com";
            int Port = 587;
            string FromEmail = "nitinsahani093@gmail.com";
            string password = "wksi rhcg ahjl cdhs";
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(FromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(Host, Port))
                {
                    smtp.Credentials = new NetworkCredential(FromEmail, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
    }
}