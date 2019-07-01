using EmployeeManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class UserController : Controller
    {
        
        // GET: User
        public UserController()
        {

        }

        public ActionResult Index()
        {
            return View(GetUsers());
        }

        private List<ApplicationUser> GetUsers()
        {
            var currentUser = Guid.Parse(User.Identity.GetUserId());
            var UsersContext = new ApplicationDbContext();
            List<ApplicationUser> data = UsersContext.Users.Where(_ => _.IsSuperAdmin == false && _.ParentUserID == currentUser).ToList();

            return data;
        }

        public ActionResult CreateSucess()
        {
            TempData["sucess"] = "Regestered Sucessfully Please Try To Login Now.";

            return View("Index",GetUsers());
        }
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
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
                    var user = new ApplicationUser { UserName = model.Email, IsActive = true, IsSuperAdmin = false, ParentUserID = Guid.Parse("06644856-45f6-4c78-9c19-60781abba7e3"), Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                    var UserContext = new ApplicationDbContext();
                    UserContext.Users.Add(user);
                    await UserContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View();
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
                var UserContext = new ApplicationDbContext();
                var user = UserContext.Users.Where(m => m.Id == id).SingleOrDefault();
                RegisterViewModel model = new RegisterViewModel { Id = user.Id,FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, IsActive = user.IsActive };
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
                    var UserContext = new ApplicationDbContext();
                    //var user = new ApplicationUser { UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName, IsActive = model.IsActive, IsSuperAdmin = model.IsSuperAdmin };
                    var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
                    var manager = new UserManager<ApplicationUser>(store);
                    var currentuser = manager.FindById(model.Id);
                    currentuser.FirstName = model.FirstName;
                    currentuser.LastName = model.LastName;
                    currentuser.Email = model.Email;
                    currentuser.IsActive = model.IsActive;
                    await manager.UpdateAsync(currentuser);
                    await store.Context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch(Exception ex)
            {
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
                var UserContext = new ApplicationDbContext();
                var user = UserContext.Users.Where(m => m.Id == id).SingleOrDefault();
                RegisterViewModel model = new RegisterViewModel { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, IsActive = user.IsActive };
                return View(model);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // POST: User/Delete/5
        [HttpPost,ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            try
            {
                // TODO: Add delete logic here
                var UserContext = new ApplicationDbContext();
                var user = UserContext.Users.Where(m => m.Id == id).SingleOrDefault();
                UserContext.Users.Remove(user);
                await UserContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
