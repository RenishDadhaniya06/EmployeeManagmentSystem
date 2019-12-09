
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
    /// TemplateController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class TemplateController : Controller
    {
        // GET: Template
        public async Task<ActionResult> Index()
        {
            //ViewBag.Types = await APIHelpers.GetAsync<List<TemplatesType>>("api/TemplateType/GetTemplateTypes");
            
            var data = await APIHelpers.GetAsync<List<Templates>>("api/Template/GetTemplates");
            if (data == null)
            {
                data = new List<Templates>();
            }
            return View(data.ToList());
        }

        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Templates>>("api/Template/GetTemplates");
                var builder = new PdfBuilder<List<Templates>>(data, Server.MapPath("/Views/Template/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception ex)
            {
                return File("AccessDenied", "Error");
            }
        }

        // GET: Template/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Template/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Types = await APIHelpers.GetAsync<List<TemplatesType>>("api/TemplateType/GetTemplateTypes");
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
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Template/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Types = await APIHelpers.GetAsync<List<TemplatesType>>("api/TemplateType/GetTemplateTypes");
            return View("Create",await APIHelpers.GetAsync<Templates>("api/Templates/Get/" + id));
        }

        

        // POST: Template/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<Templates>("api/Templates/Delete/" + id);
                TempData["sucess"] = TemplateResources.delete;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                //return View();
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }
    }
}
