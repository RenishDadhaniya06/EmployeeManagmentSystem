﻿

namespace EmployeeManagementSystem.Controllers
{
    #region Using
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using EmployeeManagementSystem.Models;
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Resources;
    using System.Web;
    using Microsoft.AspNet.Identity.Owin;
    #endregion


    /// <summary>
    /// EmployeeController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class EmployeeController : Controller
    {

        private ApplicationUserManager _userManager;
        // GET: Employee
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        /// 
        public EmployeeController()
        {

        }

        public EmployeeController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<ActionResult> Index()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");
                if (data == null)
                {
                    data = new List<Employee>();
                }
                //ViewBag.Department = data;
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
            catch (Exception ex)
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
            catch (Exception ex)
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
                ViewBag.Department = await APIHelpers.GetAsync<List<Departments>>("api/Department/GetDepartments");
                ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
                var m = new Employee()
                {
                    BirthDate = DateTime.Today
                };
                return View(m);
            }
            catch (Exception ex)
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
                string dob = Request["BirthDate"];
                string email = Request["Email"];
                string password = Request["Password"];
                string skills = Request["Skills"];
                
                ModelState.Remove("BirthDate");
                ModelState.Remove("LeaveBalance");
                //var month = (13 - DateTime.Now.Month) * 1.5;
                if (ModelState.IsValid)
                {

                    collection.BirthDate = DateTime.ParseExact(dob, "MM/dd/yyyy", null);
                    collection.LeaveBalance = Convert.ToDecimal(15);
                    // TODO: Add insert logic here
                    if (collection.Id == Guid.Empty)
                    {
                        var user = new ApplicationUser { UserName = email, IsSuperAdmin = false, ParentUserID = Guid.Parse("06644856-45f6-4c78-9c19-60781abba7e3"), Email = email, FirstName = collection.FirstName, LastName = collection.LastName, UserStatus = 0 };
                        collection.UserId = Guid.Parse(user.Id);
                        var result = await UserManager.CreateAsync(user, password);
                        if (result.Succeeded)
                        {
                            await APIHelpers.PostAsync<Employee>("api/Employee/Post", collection);
                            TempData["sucess"] = EmployeeResources.create;
                        }
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
            catch(Exception ex)
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
                ViewBag.Department = await APIHelpers.GetAsync<List<Departments>>("api/Department/GetDepartments");
                ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
                return View("Create", await APIHelpers.GetAsync<Employee>("api/Employee/Get/" + id));
            }
            catch (Exception ex)
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
            catch(Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}
