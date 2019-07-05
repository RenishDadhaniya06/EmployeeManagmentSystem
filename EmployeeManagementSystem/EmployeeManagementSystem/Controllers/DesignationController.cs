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
    public class DesignationController : Controller
    {
        // GET: Designation
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<Designation>>("api/Designation/GetDesignations");
            return View(data.ToList());
        }

        // GET: Designation/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: Designation/Create
        public ActionResult Create()
        {
            return View(new Designation());
        }

        // POST: Designation/Create
        [HttpPost]
        public async Task<ActionResult> Create(Designation collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (collection.Id == Guid.Empty)
                {
                    await APIHelpers.PostAsync<Designation>("api/Designation/Post", collection);
                    TempData["sucess"] = DesignationResources.create;
                }
                else
                {
                    await APIHelpers.PutAsync<Designation>("api/Designation/Put", collection);
                    TempData["sucess"] = DesignationResources.update;
                }
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // GET: Designation/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {

            return View("Create", await APIHelpers.GetAsync<Designation>("api/Designation/Get/" + id));
        }

        // POST: Designation/Edit/5


        // POST: Designation/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<Designation>("api/Designation/Delete/" + id);
                TempData["sucess"] = DesignationResources.delete;
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }
    }
}
