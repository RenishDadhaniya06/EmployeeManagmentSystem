using EmployeeManagementSystem.Models;
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
        
        // GET: User
        public UserController()
        {

        }

        public ActionResult Index()
        {
            var currentUser = Guid.Parse(User.Identity.GetUserId());
            var UsersContext = new ApplicationDbContext();
            List<ApplicationUser> data = UsersContext.Users.Where(_=>_.IsSuperAdmin== false && _.ParentUserID == currentUser).ToList();

            return View(data.Where(_=>_.IsSuperAdmin == false));
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
                    //if (result.Succeeded)
                    //{
                    //    ViewBag.message = "Registered Successfully";
                    //    return RedirectToAction("Index");
                    //}
                    //else
                    //{
                    //    return HttpNotFound();
                    //}
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
        public ActionResult Edit(int id)
        {
            return View("Create");
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
