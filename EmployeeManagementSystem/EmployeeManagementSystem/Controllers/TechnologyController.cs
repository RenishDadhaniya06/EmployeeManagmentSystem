
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
    /// TechnologyController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [SessionTimeout]
    public class TechnologyController : Controller
    {
        // GET: Department
        public async Task<ActionResult> Index()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Technologies>>("api/Technology/GetTechnologies");
                if (data == null)
                {
                    data = new List<Technologies>();
                }
                return View(data.ToList());
            }
            catch (Exception)
            {
                return RedirectToAction("AccessDenied", "Error");
            }

        }

        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Technologies>>("api/Technology/GetTechnologies");
                var builder = new PdfBuilder<List<Technologies>>(data, Server.MapPath("/Views/Technology/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception ex)
            {
                return File("AccessDenied", "Error");
            }
        }

        // GET: Designation/Details/5
        public ActionResult Details(Guid id)
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // GET: Designation/Create
        public ActionResult Create()
        {
            try
            {
                return View(new Technologies());
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // POST: Designation/Create
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Technologies collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    if (collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<Technologies>("api/Technology/Post", collection);
                        TempData["sucess"] = TechnologiesResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<Technologies>("api/Technology/Put", collection);
                        TempData["sucess"] = TechnologiesResources.update;
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
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // GET: Designation/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                return View("Create", await APIHelpers.GetAsync<Technologies>("api/Technology/Get/" + id));
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // POST: Designation/Edit/5


        // POST: Designation/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<Technologies>("api/Technology/Delete/" + id);
                TempData["sucess"] = TechnologiesResources.delete;
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