


namespace EmployeeManagementSystem.Controllers
{
    #region Using
    using EmployeeManagementSystem.Helper;
    using EmployeeManagementSystem.Models;
    using EmployeeManagementSystem.Models.Time;
    using EmployeeMangmentSystem.Repository.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    #endregion


    /// <summary>
    /// ReportsController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [SessionTimeout]
    public class ReportsController : Controller
    {
        #region Properties
        ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        #endregion


        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Contact()
        {
            try
            {
                var temp = CommonHelper.GetUser();
                ViewBag.User = this.applicationDbContext.Users.Select(x => new UserViewModel() { Name = x.FirstName + " " + x.LastName, Id = x.Id }).ToList();
                //List<Report> Report = new List<Report>();
                //var user = CommonHelper.GetUser();
                //ViewBag.User = this.applicationDbContext.Users.Select(x => new UserViewModel() { Name = x.FirstName + " " + x.LastName, Id = x.Id }).ToList();
                //ReportFilter model = new ReportFilter();
                //model.IsSuperAdmin = user.IsSuperAdmin;
                //model.Report = new List<Report>();
                //if (user != null && user.IsSuperAdmin)
                //{
                //    return View(model);
                //}
                //else
                //{
                //    var id = Guid.Parse(user.Id);
                //    var data = this.applicationDbContext.TimeTrackings.Where(_ => _.UserId == id).ToList();
                //    model = NewMethod(model, data);
                //    return View(model);
                //}
                var data = await APIHelpers.GetAsync<ReportFilter>("api/Reports/Contect?admin=" + temp.IsSuperAdmin + "&id1=" + temp.Id);
                return View(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //private ReportFilter NewMethod(ReportFilter model, List<TimeTrackings> data)
        //{
        //    try
        //    {
        //        foreach (var item in data)
        //        {
        //            if (model.Report.Where(_ => _.Date == item.Date && _.UserId == item.UserId.ToString()).Count() > 0)
        //            {
        //                continue;
        //            }
        //            var date = data.Where(x => x.Date == item.Date).ToList();
        //            var temid = item.UserId.ToString();

        //            var cuurUser = this.applicationDbContext.Users.Where(_ => _.Id == temid).FirstOrDefault();

        //            var report = new Report()
        //            {
        //                Date = item.Date,
        //                Name = cuurUser.FirstName + " " + cuurUser.LastName,
        //                UserId = cuurUser.Id
        //            };
        //            TimeSpan totalTime = new TimeSpan();
        //            foreach (var i in date)
        //            {
        //                if (i.Out == string.Empty)
        //                {
        //                    TimeSpan temptotalTime = Convert.ToDateTime(DateTime.Now.ToLongTimeString()) - Convert.ToDateTime(i.In);
        //                    totalTime += temptotalTime;
        //                }
        //                else
        //                {
        //                    TimeSpan temptotalTime = Convert.ToDateTime(i.Out) - Convert.ToDateTime(i.In);
        //                    totalTime += temptotalTime;
        //                }
        //            }
        //            report.TotalAge = totalTime;
        //            model.Report.Add(report);
        //        }

        //        model.Report = model.Report.OrderByDescending(_ => DateTime.Parse(_.Date)).ToList();
        //        return model;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public async Task<ActionResult> ViewReport(string id, string date)
        {
            try
            {
                //var uid = Guid.Parse(id);
                //var cuurUser = this.applicationDbContext.Users.Where(_ => _.Id == id).FirstOrDefault();

                //var data = this.applicationDbContext.TimeTrackings.Where(_ => _.UserId == uid && _.Date == date).ToList();
                //foreach (var item in data)
                //{
                //    item.CreatedDate = cuurUser.FirstName + " " + cuurUser.LastName;
                //}
                //return View(data);
                var data = await APIHelpers.GetAsync<List<TimeTrackings>>("api/Reports/ViewReport?id=" + id + "&date=" + date);
                return View(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ActionResult> GetReports(ReportFilter model)
        {
            try
            {
                var temp = CommonHelper.GetUser();
                var emp = Request["Employee"];
                var from = Request["from"];
                var to = Request["to"];
                ViewBag.User = this.applicationDbContext.Users.Select(x => new UserViewModel() { Name = x.FirstName + " " + x.LastName, Id = x.Id }).ToList();
                //var user = CommonHelper.GetUser();
                //ViewBag.User = this.applicationDbContext.Users.Select(x => new UserViewModel() { Name = x.FirstName + " " + x.LastName, Id = x.Id }).ToList();
                //model.Report = new List<Report>();
                //if (user.IsSuperAdmin)
                //{
                //    if (model.UserId != Guid.Empty.ToString())
                //    {
                //        model.IsSuperAdmin = user.IsSuperAdmin;
                //        var id = Guid.Parse(model.UserId);
                //        var data = this.applicationDbContext.TimeTrackings.Where(_ => _.UserId == id).ToList();
                //        model = NewMethod(model, data);
                //    }
                //}
                //else
                //{
                //    var id = Guid.Parse(user.Id);
                //    var data = this.applicationDbContext.TimeTrackings.Where(_ => _.UserId == id).ToList();
                //    model = NewMethod(model, data);
                //}
                //var data = await Helpers.APIHelpers.PostAsync<ReportFilter>("api/Reports/GetReports",model);
                var data = await APIHelpers.GetAsync<ReportFilter>("api/Reports/GetReports?userId=" + emp + "&admin=" + temp.IsSuperAdmin + "&id=" + temp.Id + "&from=" + from + "&to=" + to);
                return View("Contact", data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}