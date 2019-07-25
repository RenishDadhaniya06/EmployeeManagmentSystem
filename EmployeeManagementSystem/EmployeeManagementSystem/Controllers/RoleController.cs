using EmployeeManagementSystem.Helper;
using EmployeeManagementSystem.Models;
using EmployeeMangmentSystem.Resources;
using Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    //[Route("[controller]/[action]")]
    [HandleError]
    [CheckAuthorization]
    public class RoleController : Controller
    {
        public ApplicationDbContext appcontext;
        public RoleController(ApplicationDbContext context)
        {
            appcontext = context;
        }
        // GET: Role
        [CheckAuthorization]
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
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    role.Id = Convert.ToString(Guid.NewGuid());
                    appcontext.Roles.Add(role);
                    await appcontext.SaveChangesAsync();
                    bool data = await APIHelpers.GetAsync<bool>("api/RolePermission/PostRoles/" + role.Id);
                    TempData["sucess"] = CommonResources.create;
                    return RedirectToAction("Index");
                                        
                }
                else
                {
                    TempData["error"] = CommonResources.error;
                    return View(role);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = CommonResources.error;
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
                RoleViewModel data = new RoleViewModel();
                data.Id = model.Id;
                data.Name = model.Name;
                return View(model);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id,[Bind] IdentityRole role)
        {
            try
            {
                var store = new RoleStore<IdentityRole>(appcontext);
                var manager = new RoleManager<IdentityRole>(store);
                var data = manager.Roles.Where(m => m.Id == role.Id).SingleOrDefault();
                data.Name = role.Name;
                await manager.UpdateAsync(data);
                await appcontext.SaveChangesAsync();
                TempData["sucess"] = CommonResources.update;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = CommonResources.error;
                throw;
            }
        }

        //[HttpGet]
        //public async Task<ActionResult> DeleteConfirm(string id)
        //{
        //    try
        //    {
        //        if (id == null)
        //            return HttpNotFound();
        //        var data = appcontext.Roles.Where(m => m.Id == id).SingleOrDefault();
        //        if(data == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        appcontext.Roles.Remove(data);
        //        await appcontext.SaveChangesAsync();
        //        TempData["sucess"] = CommonResources.delete;
        //        var temp = await APIHelpers.DeleteAsync<bool>("api/RolePermission/Delete/" + Guid.Parse(id));
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["error"] = CommonResources.error;
        //        throw;
        //    }
        //}
    }
}