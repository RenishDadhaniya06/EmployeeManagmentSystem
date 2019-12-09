
namespace EmployeeManagementSystem.Controllers
{
    using EmployeeManagementSystem.Helper;
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
    /// CitiesController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class CitiesController : Controller
    {
        #region Index Method
        // GET: Cities
        public async Task<ActionResult> Index()
        {
            //var data2 = await APIHelpers.GetAsync<List<Notifications>>("api/Notification/GetNotifications");
            var data2 = await APIHelpers.GetAsync<List<Notifications>>("api/Notification/GetMessage");
            ViewBag.States = await APIHelpers.GetAsync<List<States>>("api/State/GetStates");
            var data = await APIHelpers.GetAsync<List<City>>("api/City/GetCities");
            return View(data.ToList());
        }
        #endregion

        #region Print Method
        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<City>>("api/City/GetCities");
                var builder = new PdfBuilder<List<City>>(data, Server.MapPath("/Views/Cities/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception ex)
            {
                return File("AccessDenied", "Error");
            }
        }
        #endregion

        #region Detaila Method
        // GET: Cities/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }
        #endregion

        #region Create Method
        // GET: Cities/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.States = await APIHelpers.GetAsync<List<States>>("api/State/GetStates");
            return View();
        }

        // POST: Cities/Create
        [HttpPost]
        public async Task<ActionResult> Create(City collection)
        {
            try
            {
                // TODO: Add insert logic here
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    if (collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<City>("api/City/Post", collection);
                        TempData["sucess"] = CityResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<City>("api/City/Put", collection);
                        TempData["sucess"] = CityResources.update;
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        #endregion

        #region Edit Method
        // GET: Cities/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.States = await APIHelpers.GetAsync<List<States>>("api/State/GetStates");
            return View("Create", await APIHelpers.GetAsync<City>("api/City/Get/" + id));
        }
        #endregion

        #region Delete Method
        // POST: State/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<bool>("api/City/Delete/" + id);
                TempData["sucess"] = CityResources.delete;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }
        #endregion
    }
}
