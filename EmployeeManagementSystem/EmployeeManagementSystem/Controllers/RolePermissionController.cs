using EmployeeManagementSystem.Models;
using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using EmployeeMangmentSystem.Repository.Repository.Interfaces;
using EmployeeMangmentSystem.Services.Services;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class RolePermissionController : Controller
    {
        private ApplicationDbContext _applicationDbContext = new ApplicationDbContext();
        private ICustomerService _iCustomerService;

        public RolePermissionController(ICustomerService iCustomerService, ApplicationDbContext context)
        {
            _applicationDbContext = context;
            _iCustomerService = iCustomerService;
        }
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
                ViewBag.Roles = await APIHelpers.GetAsync<List<RolesViewModel>>("api/RolePermission/GetRoles");
                var data = await APIHelpers.GetAsync<List<RolePermission>>("api/RolePermission/displayRoles/" + id);
                return View("Index", data);
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
                if(id == null)
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
                await APIHelpers.PutAsync<RolePermission>("api/RolePermission/Put",role);
                return RedirectToAction("Edit",role.Id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
