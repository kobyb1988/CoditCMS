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
            var toEmail = "aganzha@yandex.ru";

            message.To.Add(new MailAddress(toEmail));
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Subject = "New comment";



            message.Body = text;

            var client = new SmtpClient();
            if (client.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
            {
                client.EnableSsl = false;
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