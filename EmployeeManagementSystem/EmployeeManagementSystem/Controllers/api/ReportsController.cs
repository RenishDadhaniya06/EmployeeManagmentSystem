

namespace EmployeeManagementSystem.Controllers.api
{
    #region Using
    using EmployeeManagementSystem.Models;
    using EmployeeManagementSystem.Models.Time;
    using EmployeeMangmentSystem.Repository.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    #endregion


    /// <summary>
    /// ReportsController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ReportsController : ApiController
    {
        #region Properties
        ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        #endregion

        [Route("api/Reports/Contect")]
        [HttpGet]
        public ReportFilter Contact(bool admin,Guid id1)
        {
            try
            {
                List<Report> Report = new List<Report>();
                //ViewBag.User = this.applicationDbContext.Users.Select(x => new UserViewModel() { Name = x.FirstName + " " + x.LastName, Id = x.Id }).ToList();
                ReportFilter model = new ReportFilter();
                //model.IsSuperAdmin = user.IsSuperAdmin;
                model.IsSuperAdmin = admin;
                model.Report = new List<Report>();
                if (admin)
                {
                    return model;
                }
                else
                {
                    var id = id1;
                    var data = this.applicationDbContext.TimeTrackings.Where(_ => _.UserId == id).ToList();
                    model = NewMethod(model, data);
                    return model;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("api/Reports/NewMethod")]
        private ReportFilter NewMethod(ReportFilter model, List<TimeTrackings> data)
        {
            try
            {
                foreach (var item in data)
                {
                    if (model.Report.Where(_ => _.Date == item.Date && _.UserId == item.UserId.ToString()).Count() > 0)
                    {
                        continue;
                    }
                    var date = data.Where(x => x.Date == item.Date).ToList();
                    var temid = item.UserId.ToString();

                    var cuurUser = this.applicationDbContext.Users.Where(_ => _.Id == temid).FirstOrDefault();

                    var report = new Report()
                    {
                        Date = item.Date,
                        Name = cuurUser.FirstName + " " + cuurUser.LastName,
                        UserId = cuurUser.Id
                    };
                    TimeSpan totalTime = new TimeSpan();
                    foreach (var i in date)
                    {
                        if (i.Out == string.Empty)
                        {
                            TimeSpan temptotalTime = Convert.ToDateTime(DateTime.Now.ToLongTimeString()) - Convert.ToDateTime(i.In);
                            totalTime += temptotalTime;
                        }
                        else
                        {
                            TimeSpan temptotalTime = Convert.ToDateTime(i.Out) - Convert.ToDateTime(i.In);
                            totalTime += temptotalTime;
                        }
                    }
                    report.TotalAge = totalTime;
                    model.Report.Add(report);
                }

                model.Report = model.Report.OrderByDescending(_ => DateTime.Parse(_.Date)).ToList();
                return model;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("api/Reports/ViewReport")]
        [HttpGet]
        public List<TimeTrackings> ViewReport(string id, string date)
        {
            try
            {
                var uid = Guid.Parse(id);
                var cuurUser = this.applicationDbContext.Users.Where(_ => _.Id == id).FirstOrDefault();

                var data = this.applicationDbContext.TimeTrackings.Where(_ => _.UserId == uid && _.Date == date).ToList();
                foreach (var item in data)
                {
                    item.CreatedDate = cuurUser.FirstName + " " + cuurUser.LastName;
                }
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("api/Reports/GetReports")]
        [HttpGet]
        public ReportFilter GetReports(string userId,bool admin,Guid id)
        {
            try
            {
                //var user = EmployeeManagementSystem.Helper.CommonHelper.GetUser();
                ReportFilter model = new ReportFilter();
                model.UserId = userId;
                //ViewBag.User = this.applicationDbContext.Users.Select(x => new UserViewModel() { Name = x.FirstName + " " + x.LastName, Id = x.Id }).ToList();
                model.Report = new List<Report>();
                if (admin)
                {
                    if (model.UserId != Guid.Empty.ToString())
                    {
                        model.IsSuperAdmin = admin;
                        var id1 = Guid.Parse(model.UserId);
                        var data = this.applicationDbContext.TimeTrackings.Where(_ => _.UserId == id1).ToList();
                        model = NewMethod(model, data);
                    }
                }
                else
                {
                    var id1 = id;
                    var data = this.applicationDbContext.TimeTrackings.Where(_ => _.UserId == id1).ToList();
                    model = NewMethod(model, data);
                }
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
