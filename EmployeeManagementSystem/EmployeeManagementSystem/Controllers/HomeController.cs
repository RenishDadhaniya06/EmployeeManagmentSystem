
namespace EmployeeManagementSystem.Controllers
{

    #region Using
    using EmployeeManagementSystem.Helper;
    using EmployeeManagementSystem.Models;
    using EmployeeManagementSystem.Models.Time;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    #endregion


    /// <summary>
    /// HomeController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        public async Task<ActionResult> Index()
        {
            //return View();
            ViewBag.Message = "Your application description page.";
            var data = await GetTimeTrackingSystems();

            return View(data);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}



        public async Task<TimeTrackingSystems> GetTimeTrackingSystems()
        {
            try
            {
                var user = CommonHelper.GetUser();
                var date = DateTime.Now.ToShortDateString();
                var userId = Guid.Parse(user.Id);
                List<TimeTrackingSystems> timeTrackingSystems = new List<TimeTrackingSystems>();
                timeTrackingSystems = applicationDbContext.TimeTrackingSystems.Where(_ => _.UserId == userId && _.Date != date).ToList();
                if (timeTrackingSystems.Count > 0)
                {
                    applicationDbContext.TimeTrackingSystems.RemoveRange(timeTrackingSystems);
                    await applicationDbContext.SaveChangesAsync();
                }

                var status = applicationDbContext.TimeTrackingSystems.Count(_ => _.UserId == userId && _.Date == date);
                if (status > 0)
                {
                    var model = applicationDbContext.TimeTrackingSystems.Where(_ => _.UserId == userId && _.Date == date);
                    return model.FirstOrDefault();
                }
                else
                {
                    var model = new TimeTrackingSystems()
                    {
                        Date = date,
                        UserId = userId,
                        id = Guid.NewGuid(),
                        IsBreakIn = false,
                        IsClockIn = false,
                    };
                    applicationDbContext.TimeTrackingSystems.Add(model);
                    await applicationDbContext.SaveChangesAsync();
                    return model;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";
            var data = await GetTimeTrackingSystems();

            return View(data);
        }
        public async System.Threading.Tasks.Task<ActionResult> ViewReport(string id, string date)
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
                return View(data);
            }
            catch (Exception ex)
            {

                throw;
            }
           

        }
        public async Task<ActionResult> Contact()
        {
            try
            {
                List<Report> Report = new List<Report>();
                var user = CommonHelper.GetUser();
                ViewBag.User = this.applicationDbContext.Users.Select(x => new UserViewModel() { Name = x.FirstName + " " + x.LastName, Id = x.Id }).ToList();
                ReportFilter model = new ReportFilter();
                model.IsSuperAdmin = user.IsSuperAdmin;
                model.Report = new List<Report>();
                if (user != null && user.IsSuperAdmin)
                {
                    return View(model);
                }
                else
                {
                    var id = Guid.Parse(user.Id);
                    var data = this.applicationDbContext.TimeTrackings.Where(_ => _.UserId == id).ToList();
                    model = NewMethod(model, data);
                    return View(model);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            

        }

        //private async System.Threading.Tasks.Task<ActionResult> NewMethod()
        //{
        //    var user = CommonHelper.GetUser();
        //    if (user != null && user.IsSuperAdmin)
        //    {
        //        var data = await applicationDbContext.Database.SqlQuery<Report>(@"exec [dbo].[RolePermissionById] @p0", roleId).ToListAsync();
        //        return View();
        //    }
        //    else
        //    {
        //        var data = await Database.SqlQuery<Report>(@"exec [dbo].[RolePermissionById] @p0", roleId).ToListAsync();
        //        return View();
        //    }
        //}

        public async System.Threading.Tasks.Task<ActionResult> BrakeOut()
        {
            try
            {
                ViewBag.Message = "Your contact page.";
                var user = CommonHelper.GetUser();
                var date = DateTime.Now.ToShortDateString();
                var userId = Guid.Parse(user.Id);

                TimeTrackingSystems model = applicationDbContext.TimeTrackingSystems.Where(_ => _.UserId == userId && _.Date == date).FirstOrDefault();

                model.IsBreakIn = false;
                applicationDbContext.TimeTrackingSystems.Attach(model);
                applicationDbContext.Entry(model).State = EntityState.Modified;
                await applicationDbContext.SaveChangesAsync();

                applicationDbContext.TimeTrackings.Add(new TimeTrackings()
                {
                    Id = Guid.NewGuid(),
                    Date = date,
                    CreatedDate = DateTime.Now.ToString(),
                    In = DateTime.Now.ToLongTimeString(),
                    Out = string.Empty,
                    UserId = userId
                });
                await applicationDbContext.SaveChangesAsync();
                //return RedirectToAction("About");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async System.Threading.Tasks.Task<ActionResult> BrakeIn()
        {
            try
            {
                var user = CommonHelper.GetUser();
                var date = DateTime.Now.ToShortDateString();
                var userId = Guid.Parse(user.Id);

                TimeTrackingSystems model = applicationDbContext.TimeTrackingSystems.Where(_ => _.UserId == userId && _.Date == date).FirstOrDefault();
                model.IsBreakIn = true;
                applicationDbContext.TimeTrackingSystems.Attach(model);
                applicationDbContext.Entry(model).State = EntityState.Modified;
                await applicationDbContext.SaveChangesAsync();

                TimeTrackings timeTracking = applicationDbContext.TimeTrackings.Where(_ => _.UserId == userId && _.Date == date && _.Out == string.Empty).FirstOrDefault();
                timeTracking.Out = DateTime.Now.ToLongTimeString();
                applicationDbContext.TimeTrackings.Attach(timeTracking);
                applicationDbContext.Entry(timeTracking).State = EntityState.Modified;
                await applicationDbContext.SaveChangesAsync();

                //return RedirectToAction("About");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async System.Threading.Tasks.Task<ActionResult> ClockOut()
        {
            try
            {
                var user = CommonHelper.GetUser();
                var date = DateTime.Now.ToShortDateString();
                var userId = Guid.Parse(user.Id);

                TimeTrackingSystems model = applicationDbContext.TimeTrackingSystems.Where(_ => _.UserId == userId && _.Date == date).FirstOrDefault();
                model.IsClockIn = false;
                applicationDbContext.TimeTrackingSystems.Attach(model);
                applicationDbContext.Entry(model).State = EntityState.Modified;
                await applicationDbContext.SaveChangesAsync();

                TimeTrackings timeTracking = applicationDbContext.TimeTrackings.Where(_ => _.UserId == userId && _.Date == date && _.Out == string.Empty).FirstOrDefault();
                timeTracking.Out = DateTime.Now.ToLongTimeString();
                applicationDbContext.TimeTrackings.Attach(timeTracking);
                applicationDbContext.Entry(timeTracking).State = EntityState.Modified;

                await applicationDbContext.SaveChangesAsync();
                //return RedirectToAction("About");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async System.Threading.Tasks.Task<ActionResult> ClockIn()
        {
            try
            {
                var user = CommonHelper.GetUser();
                var date = DateTime.Now.ToShortDateString();
                var userId = Guid.Parse(user.Id);

                TimeTrackingSystems model = applicationDbContext.TimeTrackingSystems.Where(_ => _.UserId == userId && _.Date == date).FirstOrDefault();
                model.IsClockIn = true;
                applicationDbContext.TimeTrackingSystems.Attach(model);
                applicationDbContext.Entry(model).State = EntityState.Modified;
                await applicationDbContext.SaveChangesAsync();
                applicationDbContext.TimeTrackings.Add(new TimeTrackings()
                {
                    Id = Guid.NewGuid(),
                    Date = date,
                    CreatedDate = DateTime.Now.ToString(),
                    In = DateTime.Now.ToLongTimeString(),
                    Out = string.Empty,
                    UserId = userId
                });
                await applicationDbContext.SaveChangesAsync();
                //return RedirectToAction("About");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async System.Threading.Tasks.Task<ActionResult> GetReports(ReportFilter model)
        {
            try
            {
                var user = CommonHelper.GetUser();
                ViewBag.User = this.applicationDbContext.Users.Select(x => new UserViewModel() { Name = x.FirstName + " " + x.LastName, Id = x.Id }).ToList();
                model.Report = new List<Report>();
                if (user.IsSuperAdmin)
                {
                    if (model.UserId != Guid.Empty.ToString())
                    {
                        model.IsSuperAdmin = user.IsSuperAdmin;
                        var id = Guid.Parse(model.UserId);
                        var data = this.applicationDbContext.TimeTrackings.Where(_ => _.UserId == id).ToList();
                        model = NewMethod(model, data);
                    }
                }
                else
                {
                    var id = Guid.Parse(user.Id);
                    var data = this.applicationDbContext.TimeTrackings.Where(_ => _.UserId == id).ToList();
                    model = NewMethod(model, data);
                }
                return View("Contact", model);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

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
    }

    public class Report
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public TimeSpan TotalAge { get; set; }
    }

    public class ReportFilter
    {
        public string UserId { get; set; }

        public bool IsSuperAdmin { get; set; }
        //public WeekFilter WeekFilter { get; set; }
        public List<Report> Report { get; set; }
    }
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

}