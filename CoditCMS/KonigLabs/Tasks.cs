using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace KonigLabs
{
    public class Tasks
    {
        public static void SendEmailToAdmin(string text)
        {

            var message = new MailMessage();

            using (var db = DB.DAL.ApplicationDbContext.Create())
            {
                var adminEmails = db.SiteSettings.Where(s => s.Name == "AdminEmails").FirstOrDefault();
                var emailList = "kobyb.palatkin@yandex.ru";
                if (adminEmails != null)
                {
                    emailList = adminEmails.Value;
                }
                foreach (var email in emailList.Split(','))
                {
                    message.To.Add(new MailAddress(email.Trim()));
                }
            }

            
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Subject = "New comment";
            message.Body = text;

            var client = new SmtpClient();
            if (client.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
            {
                client.EnableSsl = true;
            }
                client.Send(message);
            client.SendCompleted += (s, e) =>
            {
                message.Dispose();
                client.Dispose();
            };
        }
    }
}