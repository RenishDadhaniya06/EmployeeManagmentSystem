

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
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    #endregion


    /// <summary>
    /// EmployeeController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class EmployeeController : Controller
    {

        private ApplicationUserManager _userManager;
        private ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

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
                //var data = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");
                var builder = new PdfBuilder<List<Employee>>(data, Server.MapPath("/Views/Employee/Print.cshtml"));
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
                ViewBag.Roles = _applicationDbContext.Roles.ToList();
                var m = new EmployeeUserViewModel()
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
        public async Task<ActionResult> Create(EmployeeUserViewModel collection)
        {
            try
            {
                ViewBag.Department = await APIHelpers.GetAsync<List<Departments>>("api/Department/GetDepartments");
                ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
                ViewBag.Roles = _applicationDbContext.Roles.ToList();
                string skills = string.Join(",", Request["Skill"]);
                ModelState.Remove("BirthDate");
                ModelState.Remove("LeaveBalance");
                ModelState.Remove("OtherContact");
                ModelState.Remove("Skills");
                //var month = (13 - DateTime.Now.Month) * 1.5;
                if(collection.Id == Guid.Empty)
                {
                    if (ModelState.IsValid)
                    {
                        collection.Skills = skills;
                        string dob = Request["BirthDate"];
                        collection.BirthDate = DateTime.ParseExact(dob, "MM/dd/yyyy", null);
                        var user = new ApplicationUser { RoleId = collection.RoleId, UserName = collection.Email, IsSuperAdmin = false, ParentUserID = Guid.Parse("06644856-45f6-4c78-9c19-60781abba7e3"), Email = collection.Email, FirstName = "", LastName = "", UserStatus = 0 };
                        collection.UserId = Guid.Parse(user.Id);
                        var result = await UserManager.CreateAsync(user, collection.Password);
                        if (result.Succeeded)
                        {
                            await APIHelpers.PostAsync<Employee>("api/Employee/Post", collection);
                            TempData["sucess"] = EmployeeResources.create;
                        }
                    }
                    else
                    {
                        return View(collection);
                    }

                }
                else
                {
                    ModelState.Remove("Password");
                    ModelState.Remove("Email");
                    if (ModelState.IsValid)
                    {
                        string dob = Request["BirthDate"];
                        collection.BirthDate = DateTime.ParseExact(dob, "MM/dd/yyyy", null);
                        collection.Skills = skills;
                        await APIHelpers.PutAsync<Employee>("api/Employee/Put", collection);
                        TempData["sucess"] = EmployeeResources.update;
                    }
                    else
                    {
                        return View(collection);
                    }
                }
                return RedirectToAction("Index");
                //if (ModelState.IsValid)
                //{
                //    collection.Skills = skills;
                //    // TODO: Add insert logic here
                //    if (collection.Id == Guid.Empty)
                //    {
                //        string dob = Request["BirthDate"];
                //        collection.BirthDate = DateTime.ParseExact(dob, "MM/dd/yyyy", null);
                //        var user = new ApplicationUser { RoleId = collection.RoleId, UserName = collection.Email, IsSuperAdmin = false, ParentUserID = Guid.Parse("06644856-45f6-4c78-9c19-60781abba7e3"), Email = collection.Email, FirstName = "", LastName = "", UserStatus = 0 };
                //        collection.UserId = Guid.Parse(user.Id);
                //        var result = await UserManager.CreateAsync(user, collection.Password);
                //        if (result.Succeeded)
                //        {
                //            await APIHelpers.PostAsync<Employee>("api/Employee/Post", collection);
                //            TempData["sucess"] = EmployeeResources.create;
                //        }
                //    }
                //    else
                //    {
                //        await APIHelpers.PutAsync<Employee>("api/Employee/Put", collection);
                //        TempData["sucess"] = EmployeeResources.update;
                //    }
                //    return RedirectToAction("Index");
                //}
                //else
                //{
                //    return View(collection);
                //}

            }
            catch (Exception ex)
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
                ViewBag.Roles = _applicationDbContext.Roles.ToList();
                var data = await APIHelpers.GetAsync<EmployeeUserViewModel>("api/Employee/GetEmployeeUser/" + id);
                EmployeeUserViewModel emp = new EmployeeUserViewModel{
                    Id = data.Id,
                    FirstName = data.FirstName,
                    MiddleName = data.MiddleName,
                    LastName = data.LastName,
                    Address = data.Address,
                    BirthDate = data.BirthDate,
                    Phone = data.Phone,
                    OtherContact = data.OtherContact,
                    CurrentSalary = data.CurrentSalary,
                    LeaveBalance = data.LeaveBalance,
                    IsEmailVerified = data.IsEmailVerified,
                    UserId = data.UserId,
                    Department = data.Department,
                    Skills = data.Skills
                };
                var temp = await APIHelpers.GetAsync<List<ProjectViewModel>>("api/Employee/Projects/" + data.UserId);
                emp.Projects = temp;
                return View("Create", emp);
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
                var data = await APIHelpers.GetAsync<Employee>("api/Employee/Get/" + id);
                await UserManager.DeleteAsync(UserManager.Users.Where(_ => _.Id == data.UserId.ToString()).SingleOrDefault());
                await APIHelpers.DeleteAsync<bool>("api/Employee/Delete/" + id);

                TempData["sucess"] = EmployeeResources.delete;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        public async Task<JsonResult> ChangeStatus(string teamid)
        {
            try
            {
                if(teamid != null)
                {
                    await APIHelpers.GetAsync<string>("api/Project/ChangeWorkingStatus/" + teamid);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
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
