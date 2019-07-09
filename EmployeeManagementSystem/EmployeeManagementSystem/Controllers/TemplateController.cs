using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Resources;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

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
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Templates collection)
        {
            try
            {
                //ModelState.Remove("TemplateContent");
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    if(collection.Id == Guid.Empty)
                    {
                        var data = collection.TemplateContent;
                        await APIHelpers.PostAsync<Templates>("api/Templates/Post", collection);
                        TempData["sucess"] = TemplateResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<Templates>("api/Templates/Put", collection);
                        TempData["sucess"] = TemplateResources.update;
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
        public async Task<ActionResult> Edit(Guid id)
        {
            return View("Create",await APIHelpers.GetAsync<Templates>("api/Templates/Get/" + id));
        }

        // POST: Template/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Template/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Template/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<Designation>("api/Templates/Delete/" + id);
                TempData["sucess"] = TemplateResources.delete;
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
