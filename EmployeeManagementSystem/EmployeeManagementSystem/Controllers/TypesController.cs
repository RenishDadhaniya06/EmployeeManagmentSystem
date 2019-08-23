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
    public class TypesController : Controller
    {
        // GET: TemplateType
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<TemplatesType>>("api/TemplateType/GetTemplateTypes");
            if (data == null)
            {
                data = new List<TemplatesType>();
            }
            return View(data.ToList());
        }

        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<TemplatesType>>("api/TemplateType/GetTemplateTypes");
                var builder = new PdfBuilder<List<TemplatesType>>(data, Server.MapPath("/Views/Types/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception ex)
            {
                return File("AccessDenied", "Error");
            }
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
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    if(collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<TemplatesType>("api/TemplateType/Post", collection);
                        TempData["sucess"] = TemplateTypeResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<TemplatesType>("api/TemplateType/Put", collection);
                        TempData["sucess"] = TemplateResources.update;
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
        public async Task<ActionResult> Edit(Guid id)
        {
            return View("Create", await APIHelpers.GetAsync<TemplatesType>("api/TemplateType/Get/" + id));
        }

        // POST: TemplateType/Edit/5
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

        // GET: TemplateType/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: TemplateType/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<TemplatesType>("api/TemplateType/Delete/" + id);
                TempData["sucess"] = TemplateTypeResources.delete;
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
