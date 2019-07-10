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
    public class TypesController : Controller
    {
        // GET: TemplateType
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<TemplatesType>>("api/TemplateType/GetTemplateTypes");
            return View(data.ToList());
        }

        // GET: TemplateType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TemplateType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TemplateType/Create
        [HttpPost]
        public async Task<ActionResult> Create(TemplatesType collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<TemplatesType>("", collection);
                        TempData["sucess"] = TemplateTypeResources.create;
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: TemplateType/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TemplateType/Edit/5
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

        // GET: TemplateType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TemplateType/Delete/5
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
