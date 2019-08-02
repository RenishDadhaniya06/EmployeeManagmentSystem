using System.Net;
using System.Net.Mail;

namespace Helpers
{
    public static class CommonHelper
    {

        public static string ApiURL = "http://localhost:56853/";

        public static string Email = "Dhaval.Albiorix@gmail.com";

        public static string PWD = "Dhaval@$Albiorix";

        public static void SendMail(string emailId, string subject,string body)
        {
           
            var fromMail = new MailAddress(Email, subject);
            var toMail = new MailAddress(emailId);
           

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(fromMail.Address, PWD)
            };
            using (var message = new MailMessage(fromMail, toMail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
    }
}
