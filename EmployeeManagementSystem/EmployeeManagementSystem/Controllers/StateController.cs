
namespace EmployeeManagementSystem.Controllers
{
    #region Using
    using EmployeeManagementSystem.Models;
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Resources;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    #endregion


    /// <summary>
    /// StateController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class StateController : Controller
    {
        // GET: State
        public async Task<ActionResult> Index()
        {
            ViewBag.Country = await APIHelpers.GetAsync<List<Countries>>("api/Country/GetCountries");
            var data = await APIHelpers.GetAsync<List<States>>("api/State/Getstates");
            return View(data.ToList());
        }

        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<States>>("api/State/Getstates");
                var builder = new PdfBuilder<List<States>>(data, Server.MapPath("/Views/State/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception ex)
            {
                return File("AccessDenied", "Error");
            }
        }

        // GET: State/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: State/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Country = await APIHelpers.GetAsync<List<Countries>>("api/Country/GetCountries");
            return View();
        }

        // POST: State/Create
        [HttpPost]
        public async Task<ActionResult> Create(States collection)
        {
            try
            {
                // TODO: Add insert logic here
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    if (collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<States>("api/State/Post", collection);
                        TempData["sucess"] = StateResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<States>("api/State/Put", collection);
                        TempData["sucess"] = StateResources.update;
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: State/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Country = await APIHelpers.GetAsync<List<Countries>>("api/Country/GetCountries");
            return View("Create", await APIHelpers.GetAsync<States>("api/State/Get/" + id));
        }

        
        // POST: State/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<States>("api/State/Delete/" + id);
                TempData["sucess"] = StateResources.delete;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }
    }
}
