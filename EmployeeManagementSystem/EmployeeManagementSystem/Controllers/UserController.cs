
namespace EmployeeManagementSystem.Controllers
{
    #region Using
    using EmployeeManagementSystem.Helper;
    using EmployeeManagementSystem.Models;
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using EmployeeMangmentSystem.Repository.Repository.Classes;
    using EmployeeMangmentSystem.Resources;
    using Helpers;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using CommonHelper = Helper.CommonHelper;

    #endregion

    /// <summary>
    /// UserController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class UserController : Controller
    {
        #region Properties
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _applicationDbContext = new ApplicationDbContext();
        private RepositoryContext _repositoryContext = new RepositoryContext();
        #endregion

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationDbContext applicationDbContext, RepositoryContext repositoryContext)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _repositoryContext = repositoryContext;
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

        [CheckAuthorization]
        public ActionResult Index()
        {
            return View(GetUsers());
        }

        private List<ApplicationUser> GetUsers()
        {
            var id = Guid.Parse(User.Identity.GetUserId());
            return UserManager.Users.Where(_ => _.IsSuperAdmin == false && _.ParentUserID == id).ToList();
        }

        public ActionResult CreateSucess()
        {
            TempData["sucess"] = CommonResources.create;
            return View("Index", GetUsers());
        }
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        [CheckAuthorization]
        public ActionResult Create()
        {
            ViewBag.Roles = _applicationDbContext.Roles.Where(m => m.Name != "Admin").ToList();
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            try
            {

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, IsSuperAdmin = false, ParentUserID = Guid.Parse("06644856-45f6-4c78-9c19-60781abba7e3"), Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, UserStatus = model.UserStatus, RoleId = model.RoleId };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        //_applicationDbContext.UserRoles.Add(new IdentityUserRole<string>()
                        //{
                        //    UserId = model.Id,
                        //RoleId = model.RoleId
                        //});
                        //var month = DateTime.Now.Month;
                        //var cal = 13 - month;
                        //Employee emp = new Employee();
                        //emp.Name = model.FirstName + model.LastName;
                        //emp.Email = model.Email;
                        //emp.AvailableLeaves = Convert.ToDecimal(cal * 1.5);
                        //await APIHelpers.PostAsync<Employee>("api/Employee/Post", emp);
                        //_repositoryContext.Employees.Add(emp);
                        TempData["sucess"] = CommonResources.create;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorModelState(result);
                        return View(model);
                    }
                }
                else
                {
                    TempData["error"] = CommonResources.error;
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return View();
            }
        }

        private string AddErrors(IdentityResult result)
        {
            string err = string.Empty;
            foreach (var error in result.Errors)
            {
                err += err;
            }
            return err;
        }

        private void AddErrorModelState(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: User/Edit/5
        [HttpGet]
        [CheckAuthorization]
        public ActionResult Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Roles = _applicationDbContext.Roles.Where(m => m.Name != "Admin").ToList();
                var user = UserManager.Users.Where(m => m.Id == id).SingleOrDefault();
                RegisterViewModel model = new RegisterViewModel { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
                //return View("Create", user);
                return View(model);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RegisterViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
                ModelState.Remove("IsSuperAdmin");
                if (ModelState.IsValid)
                {

                    var currentuser = UserManager.FindById(model.Id);
                    currentuser.FirstName = model.FirstName;
                    currentuser.LastName = model.LastName;
                    currentuser.Email = model.Email;
                    currentuser.UserStatus = model.UserStatus;
                    currentuser.RoleId = model.RoleId;
                    //var updaterole = await UserManager.GetRolesAsync(currentuser.Id);
                    var updaterole = _applicationDbContext.Roles.Where(m => m.Id == currentuser.RoleId).SingleOrDefault();
                    var role = UserManager.GetRoles(currentuser.Id);
                    string temp = Convert.ToString(role[0]);
                    if (await UserManager.IsInRoleAsync(currentuser.Id, temp))
                    {
                        await UserManager.RemoveFromRolesAsync(currentuser.Id, temp);
                        await UserManager.AddToRolesAsync(currentuser.Id, updaterole.Name);
                    }
                    //await UserManager.IsInRoleAsync()

                    var data = await UserManager.UpdateAsync(currentuser);
                    //await _applicationDbContext.SaveChangesAsync();
                    if (data.Succeeded)
                    {
                        TempData["sucess"] = CommonResources.update;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorModelState(data);
                        return View(model);
                    }
                }
                else
                {
                    TempData["error"] = CommonResources.error;
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            //return View();
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                var user = UserManager.FindById(id);
                RegisterViewModel model = new RegisterViewModel { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
                return View(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: User/Delete/5
        //[HttpPost,ActionName("Delete")]
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            try
            {
                // TODO: Add delete logic here
                var user = UserManager.FindById(id);
                await UserManager.DeleteAsync(user);
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Profile()
        {
            try
            {
                var empId = CommonHelper.CurrentEmployeeId();
                ViewBag.Department = await APIHelpers.GetAsync<List<Departments>>("api/Department/GetDepartments");                ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");                ViewBag.Roles = _applicationDbContext.Roles.ToList();
                var temp = await APIHelpers.GetAsync<EmployeeUserViewModel>("api/Employee/GetEmployeeUser/" + empId);             return View(temp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // POST: Employee/Create
        /// <summary>        /// Creates the specified collection.        /// </summary>        /// <param name="collection">The collection.</param>        /// <returns></returns>        [HttpPost]        [ValidateInput(false)]        public async Task<ActionResult> Profile(EmployeeUserViewModel collection)        {            try            {
                ViewBag.Department = await APIHelpers.GetAsync<List<Departments>>("api/Department/GetDepartments");                ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");                string skills = string.Join(",", Request["Skill"]);                collection.Skills = skills;                await APIHelpers.PutAsync<Employee>("api/Employee/Put", collection);
                TempData["sucess"] = EmployeeResources.update;
                //string dob = Request["BirthDate"];
                //string skills = string.Join(",", Request["Skill"]);

                //ModelState.Remove("BirthDate");
                //ModelState.Remove("LeaveBalance");
                ////var month = (13 - DateTime.Now.Month) * 1.5;
                //if (ModelState.IsValid)
                //{

                //    collection.BirthDate = DateTime.ParseExact(dob, "dd/MMM/yyyy", null);
                //    collection.LeaveBalance = Convert.ToDecimal(15);
                //    collection.Skills = skills;
                //    // TODO: Add insert logic here
                //    if (collection.Id == Guid.Empty)
                //    {
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
                return View("Profile",collection);                //}                //else                //{                //    return View(collection);                //}            }            catch (Exception ex)            {                TempData["error"] = CommonResources.error;                return RedirectToAction("AccessDenied", "Error");            }        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
