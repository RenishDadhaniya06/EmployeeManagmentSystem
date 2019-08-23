using EmployeeManagementSystem.Models;
using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Resources;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public async Task<ActionResult> Index()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Notifications>>("api/Notification/GetNotifications");
                if (data == null)
                {
                    data = new List<Notifications>();
                }
                return View(data.ToList());
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }

        }

        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Notifications>>("api/Notification/GetNotifications");
                var builder = new PdfBuilder<List<Notifications>>(data, Server.MapPath("/Views/Notification/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception ex)
            {
                return File("AccessDenied", "Error");
            }
        }

        // GET: Notification/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Notification/Create
        public ActionResult Create()
        {
            try
            {
                Notifications model = new Notifications();
                model.From = DateTime.Now.Date;
                model.To = DateTime.Now.Date;
                //return View(new Notifications());
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // POST: Notification/Create
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Notifications collection)
        {
            try
            {
                ModelState.Remove("From");
                ModelState.Remove("To");
                if (ModelState.IsValid)
                {
                    collection.From = DateTime.Now.Date;
                    var todate = Convert.ToDouble(Request["Duration"]);
                    if (todate == 0)
                    {
                        TempData["error"] = NotificationResources.duration;
                        return View(collection);
                    }
                    else
                    {
                        collection.To = DateTime.Now.Date.AddDays(todate - 1);
                    }
                    //collection.To = DateTime.Now.Date.AddDays(Convert.ToDouble(Request["Duration"]));
                    // TODO: Add insert logic here
                    if (collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<Notifications>("api/Notification/Post", collection);
                        TempData["sucess"] = NotificationResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<Notifications>("api/Notification/Put", collection);
                        TempData["sucess"] = NotificationResources.update;
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(collection);
                }
            }
            catch(Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // GET: Notification/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                return View("Create", await APIHelpers.GetAsync<Notifications>("api/Notification/Get/" + id));
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

       
        // GET: Notification/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<Notifications>("api/Notification/Delete/" + id);
                TempData["sucess"] = NotificationResources.delete;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }

       // [Route("Notification/GetMessage")]
       //public async Task<Notifications> GetMessages()
       // {
       //     try
       //     {
       //         Notifications temp = new Notifications();
       //         var data = await APIHelpers.GetAsync<List<Notifications>>("api/Notification/GetNotifications");
       //         foreach(var item in data)
       //         {
       //             if(DateTime.Now.Date <= item.From && DateTime.Now.Date >= item.To)
       //             {
       //                 temp = item;
       //             }
       //         }
       //         return temp;
       //     }
       //     catch (Exception ex)
       //     {

       //         throw;
       //     }
       // }

        [HttpGet]
        public async Task<JsonResult> GetMessages()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Notifications>>("api/Notification/GetMessage");
                //return Json(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
