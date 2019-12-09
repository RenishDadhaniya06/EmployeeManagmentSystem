
namespace EmployeeManagementSystem.Controllers
{

    #region Using
    using EmployeeManagementSystem.Helper;
    using EmployeeManagementSystem.Models;
    using EmployeeManagementSystem.Models.Time;
    using EmployeeMangmentSystem.Repository.Models;
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

        public ActionResult Lockout()
        {
            return View();
        }

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
    }

    //public class Report
    //{
    //    public string UserId { get; set; }
    //    public string Name { get; set; }
    //    public string Date { get; set; }
    //    public TimeSpan TotalAge { get; set; }
    //}

    //public class ReportFilter
    //{
    //    public string UserId { get; set; }

    //    public bool IsSuperAdmin { get; set; }
    //    //public WeekFilter WeekFilter { get; set; }
    //    public List<Report> Report { get; set; }
    //}
    //public class UserViewModel
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //}

}