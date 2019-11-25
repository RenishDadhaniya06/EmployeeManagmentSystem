using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeManagementSystem.Helper
{
    public class Jobclass : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (var message = new MailMessage("leo.smith000001@gmail.com", "umesh.albiorix@gmail.com"))
            {
                message.Subject = "Message Subject test";
                message.Body = "Message body test at " + DateTime.Now;
                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = true,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential("leo.smith000001@gmail.com", "test@123456")
                })
                {
                    client.Send(message);
                }
            }
        }
    }
   
}