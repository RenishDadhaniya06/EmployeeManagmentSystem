using EmployeeMangmentSystem.Repository.Models;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EmployeeMangmentSystem.Resources;

namespace EmployeeManagementSystem.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Template
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<Templates>>("api/Template/GetTemplates");
            return View(data.ToList());
        }

        // GET: Template/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Template/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Template/Create
        [HttpPost]
        public async Task<ActionResult> Create(Templates collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    if(collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<Templates>("api/Templates/Post", collection);
                        TempData["sucess"] = TemplateResources
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(collection);
                }
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Template/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Template/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Template/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Template/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
