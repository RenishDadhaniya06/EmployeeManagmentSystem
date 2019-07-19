using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class RolePermissionController : Controller
    {
    
        // GET: RolePermission
        public async Task<ActionResult> Index()
        {
            //ViewBag.Roles = _applicationDbContext.Roles.Where(m => m.Name != "Admin").ToList();
            ViewBag.Roles = await APIHelpers.GetAsync<List<RolesViewModel>>("api/RolePermission/GetRoles");
            return View();
        }

        //public async Task<JsonResult> DisplayRoles(Guid id)
        //{
        //    try
        //    {
        //        var data = await _iCustomerService.GetRolesById(id);
        //        return Json(data,JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
        public async Task<ActionResult> DisplayRoles(Guid id)
        {
            try
            {
                DisplayRoleModel model = new DisplayRoleModel();
                ViewBag.Roles = await APIHelpers.GetAsync<List<RolesViewModel>>("api/RolePermission/GetRoles");
                var data = await APIHelpers.GetAsync<List<RolePermission>>("api/RolePermission/displayRoles/" + id);
                model.RolePermissions = data;
                model.Role = id;
                return View("Index", model);
                //return RedirectToAction("Index", model);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    var data = await APIHelpers.GetAsync<RolePermission>("api/RolePermission/Get/" + id);
                    return View(data);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RolePermission role)
        {
            try
            {
                await APIHelpers.PutAsync<RolePermission>("api/RolePermission/Put", role);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
