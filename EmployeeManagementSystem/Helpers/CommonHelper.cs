
namespace Helpers
{
    #region Using
    using System.Net;
    using System.Net.Mail;
    #endregion


    /// <summary>
    /// CommonHelper
    /// </summary>
    public static class CommonHelper
    {

        public static string ApiURL = "http://localhost:56853/";


        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="emailId">The email identifier.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public static void SendMail(string emailId, string subject,string body,string senderEmail,string senderPassword)
        {

            var message = new MailMessage();
            //var fromMail = new MailAddress(Email, subject);
            message.From = new MailAddress(senderEmail);
            //var toMail = new MailAddress(emailId);
            string[] multi = emailId.Split(',');
            foreach(string multiid in multi)
            {
                message.To.Add(multiid);
            }
            

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                //Credentials = new NetworkCredential(fromMail.Address, PWD)
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };
            //using (var message = new MailMessage(fromMail, toMail)
            //{
            //    Subject = subject,
            //    Body = body,
            //    IsBodyHtml = true
            //})
            message.Subject = subject;

            message.Body = body;

            message.IsBodyHtml = true;
            smtp.Send(message);
            
                
        }
    }
}
