
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
    public class CountryController : Controller
    {
        #region Index Method
        // GET: Country
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<Countries>>("api/Country/GetCountries");
            return View(data.ToList());
        }
        #endregion

        #region Print Method
        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Countries>>("api/Country/GetCountries");
                var builder = new PdfBuilder<List<Countries>>(data, Server.MapPath("/Views/Country/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception ex)
            {
                return File("AccessDenied", "Error");
            }
        }
        #endregion

        #region Details Method
        // GET: Country/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        #endregion

        #region Create Method
        // GET: Country/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Country/Create
        [HttpPost]
        public async Task<ActionResult> Create(Countries collection)
        {
            try
            {
                // TODO: Add insert logic here
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    if (collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<Countries>("api/Country/Post", collection);
                        TempData["sucess"] = CountryResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<Countries>("api/Country/Put", collection);
                        TempData["sucess"] = CountryResources.update;
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

        // GET: Country/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            return View("Create",await APIHelpers.GetAsync<Countries>("api/Country/Get/" + id));
        }
        #endregion

        #region Delete Method
        // POST: Country/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<Countries>("api/Country/Delete/" + id);
                TempData["sucess"] = CountryResources.delete;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                //return View();
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }
        #endregion
    }
}
