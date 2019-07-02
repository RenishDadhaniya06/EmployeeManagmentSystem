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
    //[Route("[controller]/[action]")]
    public class RoleController : Controller
    {
        public ApplicationDbContext appcontext;
        public RoleController(ApplicationDbContext context)
        {
            appcontext = context;
        }
        // GET: Role
        public async Task<ActionResult> Index()
        {
            //var roleStore = new RoleStore<IdentityRole>(appcontext);
            //var roleMngr = new RoleManager<IdentityRole>(roleStore);
            //var data = roleMngr.Roles.ToList();
            var data = appcontext.Roles.ToList();
            //ViewBag.Role = appcontext.Roles.ToList();
            
            return View(data);
        }

        [HttpGet]
        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Post(IdentityRole role)
        {
            try
            {
                ////ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    if(role.Id == null)
                    {
                        role.Id = Convert.ToString(Guid.NewGuid());
                        appcontext.Roles.Add(role);
                        await appcontext.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var store = new RoleStore<IdentityRole>(appcontext);
                        var manager = new RoleManager<IdentityRole>(store);
                        var data = manager.Roles.Where(m => m.Id == role.Id).SingleOrDefault();
                        data.Name = role.Name;
                        await manager.UpdateAsync(data);
                        await appcontext.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                if (id == null)
                    return HttpNotFound();
                var model = appcontext.Roles.Where(m => m.Id == id).SingleOrDefault();
                if(model  == null)
                {
                    return HttpNotFound();
                }
                return View("Post", model);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}