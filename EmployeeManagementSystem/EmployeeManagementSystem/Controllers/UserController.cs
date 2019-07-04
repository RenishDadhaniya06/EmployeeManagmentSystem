using EmployeeManagementSystem.Models;
using EmployeeMangmentSystem.Resources;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
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

        public ActionResult Index()
        {
            //TempData["sucess"] = "qdq cqwd  qddwqed ";
            //TempData["error"] = " we wewqe   eqwqwe ";
            //TempData["info"] = "qwe qwe qwe e";
            //TempData["warning"] = "q ewq ";
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
        [Authorize(Roles = "Admin")]
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
                    var user = new ApplicationUser { UserName = model.Email, IsSuperAdmin = false, ParentUserID = Guid.Parse("06644856-45f6-4c78-9c19-60781abba7e3"), Email = model.Email, FirstName = model.FirstName, LastName = model.LastName,UserStatus = model.UserStatus,RoleId = model.RoleId };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        //_applicationDbContext.UserRoles.Add(new IdentityUserRole<string>()
                        //{
                        //    UserId = model.Id,
                        //RoleId = model.RoleId
                        //});
                        var data = _applicationDbContext.Roles.Where(m => m.Id == user.RoleId).SingleOrDefault();
                        await UserManager.AddToRoleAsync(user.Id, data.Name);

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
            catch(Exception ex)
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
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }

                var user = UserManager.Users.Where(m => m.Id == id).SingleOrDefault();
                RegisterViewModel model = new RegisterViewModel { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
                //return View("Create", user);
                return View(model);

            }
            catch (Exception)
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
            catch(Exception ex)
            {
                return View();
            }
        }

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
