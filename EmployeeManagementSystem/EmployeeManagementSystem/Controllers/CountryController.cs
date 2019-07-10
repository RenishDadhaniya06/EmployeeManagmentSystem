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
    public class CountryController : Controller
    {
        // GET: Country
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<Countries>>("api/Country/GetCountries");
            return View(data.ToList());
        }

        // GET: Country/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

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
            catch
            {
                return View();
            }
        }

        // GET: Country/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            return View("Create",await APIHelpers.GetAsync<Countries>("api/Country/Get/" + id));
        }


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
            catch
            {
                //return View();
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }
    }
}
