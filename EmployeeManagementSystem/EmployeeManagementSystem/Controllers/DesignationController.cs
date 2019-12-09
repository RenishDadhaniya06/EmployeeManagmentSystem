
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
    /// DesignationController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [SessionTimeout]
    public class DesignationController : Controller
    {
        #region Index Method
        // GET: Designation
        public async Task<ActionResult> Index()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Designation>>("api/Designation/GetDesignations");
                if (data == null)
                {
                    data = new List<Designation>();
                }
                return View(data.ToList());
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }

        }
        #endregion

        #region Print Method
        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Designation>>("api/Designation/GetDesignations");
                var builder = new PdfBuilder<List<Designation>>(data, Server.MapPath("/Views/Designation/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception ex)
            {
                return File("AccessDenied", "Error");
            }
        }
        #endregion

        #region Details Method
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
        #endregion

        #region Create Method
        // GET: Designation/Create
        public ActionResult Create()
        {
            try
            {
                return View(new Designation());
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // POST: Designation/Create
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Designation collection)
        {
            try
            {
                if (ModelState.IsValid)
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
        #endregion

        #region Edit Method
        // GET: Designation/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                return View("Create", await APIHelpers.GetAsync<Designation>("api/Designation/Get/" + id));
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }
        #endregion

        #region Delete Method
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
            catch(Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }
        #endregion
    }
}
