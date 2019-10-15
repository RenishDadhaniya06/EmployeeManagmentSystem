using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using EmployeeManagementSystem.Models;
using EmployeeMangmentSystem.Repository.Repository.Classes;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Material;

            scheduler.Config.first_hour = 6;
            scheduler.Config.last_hour = 20;

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ContentResult Data()
        {
            var apps = dbContext.Appointments.ToList();
            return new SchedulerAjaxData(apps);
        }

        public ActionResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = DHXEventsHelper.Bind<Appointment>(actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        dbContext.Appointments.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        dbContext.Entry(changedEvent).State = EntityState.Deleted;
                        break;
                    default:// "update"  
                        dbContext.Entry(changedEvent).State = EntityState.Modified;
                        break;
                }
                dbContext.SaveChanges();
                action.TargetId = changedEvent.Id;
            }
            catch (Exception a)
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}