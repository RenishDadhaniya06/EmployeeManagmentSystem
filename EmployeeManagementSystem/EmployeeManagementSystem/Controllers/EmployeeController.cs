using EmployeeManagementSystem.Models;
using EmployeeMangmentSystem.Repository.Models;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMangmentSystem.Resources;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class EmployeeController : Controller
    {
        // GET: Employee
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");
                if (data == null)
                {
                    data = new List<Employee>();
                }
                ViewBag.Employee = data;
                return View(data.ToList());
            }
            catch (Exception ex)
            {

                return RedirectToAction("AccessDenied", "Error");
            }

        }

        /// <summary>
        /// Prints this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");
                var builder = new PdfBuilder<List<Employee>>(data, Server.MapPath("/Views/Print/Pdf.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception)
            {

                return File("AccessDenied", "Error");
            }
        }

        // GET: Employee/Details/5
        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Details(Guid id)
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // GET: Employee/Create
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");
                ViewBag.Employee = data;
                return View(new Employee());
            }
            catch (Exception)
            {

                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // POST: Employee/Create
        /// <summary>
        /// Creates the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Employee collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    if (collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<Employee>("api/Employee/Post", collection);
                        TempData["sucess"] = EmployeeResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<Employee>("api/Employee/Put", collection);
                        TempData["sucess"] = EmployeeResources.update;
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
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // GET: Employee/Edit/5
        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");
                ViewBag.Employee = data;
                return View("Create", await APIHelpers.GetAsync<Employee>("api/Employee/Get/" + id));
            }
            catch (Exception)
            {

                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // POST: Employee/Delete/5
        /// <summary>
        /// Deletes the confirm.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<Employee>("api/Employee/Delete/" + id);
                TempData["sucess"] = EmployeeResources.delete;
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
