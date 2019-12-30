using EmployeeManagementSystem.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using EmployeeMangmentSystem.Repository.Repository.Classes;
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
            RepositoryContext repositoryContext = new RepositoryContext();
            var data = repositoryContext.Database.SqlQuery<FreeDevloperViewModel>(@"exec [dbo].[FreeEmployees]").ToList();
            var email = repositoryContext.Database.SqlQuery<string>(@"exec [dbo].[GetSalesAdminEmail]").ToArray();
            var systemSetting = repositoryContext.SettingView.FirstOrDefault();
            
            string html = "";

            html += "<html><head>" + "<meta name = 'viewport' content = 'width=device-width' /><style type = 'text/css'>";
            html += ".tftable {width: 100 %;border-width: 1px;border-collapse: collapse;}.tftable th {font-size: 15px;font-weight: bold;background-color: #acc8cc;border-width: 1px;padding: 8px; border-style: solid;text-align: left;}</style>";
            html += "</head><body>";
            html += "<table class='tftable' border='1' style='width: 100 %; border-style:solid; border-width:1px;cellpadding='4' cellspacing='4'><tbody> ";
            html += "<tr><td>Name</td><td>Email</td><td>Skills</td><td>Total Experiance</td></tr>";


            foreach (var d in data)
            {
                if (Convert.ToInt32(d.WorkingCount) == 0)
                {
                    html += "<tr><td>" + d.FullName + "</td> <td>" + d.Email + "</td> <td>" + d.Skills + "</td><td>" + d.Experience + "</td> </tr>";
                }
            }
            html += " </tbody></table>";

            Helpers.CommonHelper.SendMail(string.Join(",", email), "List of Available Resource", html, systemSetting.Email, systemSetting.Password, systemSetting.Host, systemSetting.Port);
        }
    }

}