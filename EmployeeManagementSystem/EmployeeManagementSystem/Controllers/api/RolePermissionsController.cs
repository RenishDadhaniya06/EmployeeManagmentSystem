
namespace EmployeeManagementSystem.Controllers.api
{
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using EmployeeMangmentSystem.Repository.Repository.Interfaces;
    using EmployeeMangmentSystem.Services.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    #endregion


    /// <summary>
    /// RolePermissionsController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class RolePermissionsController : ApiController
    {
        #region Properties
        private IRepository<RolePermission> rolerepository;
        private IRepository<PermissionModules> modulerepository;
        private ICustomerService _customerService;
        #endregion

        public RolePermissionsController(IRepository<RolePermission> rrepository, IRepository<PermissionModules> prepository,ICustomerService customerService)
        {
            rolerepository = rrepository;
            modulerepository = prepository;
            _customerService = customerService;
        }

        [Route("api/RolePermission/GetPermissions")]
        public IEnumerable<RolePermission> GetPermissions()
        {
            try
            {
                var data = rolerepository.GetAll();
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        

        [Route("api/PermissionModule/GetModules")]
        public IEnumerable<PermissionModules> GetModules()
        {
            try
            {
                var data = modulerepository.GetAll();
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("api/RolePermission/PostRoles/{id}")]
        public bool PostRoles(Guid id)
        {
            try
            {
                IEnumerable<PermissionModules> permissionModules = modulerepository.GetAll();
                List<RolePermission> role = new List<RolePermission>();
                foreach (var item in permissionModules)
                {
                    role.Add(new RolePermission()
                    {
                        ModuleName = item.ModuleName,
                        Create = false,
                        Delete = false,
                        Edit = false,
                        Read = false,
                        Id = Guid.NewGuid(),
                        RoleId = id
                    });
                }
                rolerepository.InsertRange(role);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("api/RolePermission/GetRoles")]
        [HttpGet]
        public async Task<List<RolesViewModel>> GetRoles()
        {
            try
            {
                var data = await _customerService.GetRoles();
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("api/RolePermission/displayRoles/{id}")]
        public async Task<List<RolePermission>> displayRoles(Guid id)
        {
            try
            {
                var data = await _customerService.GetRolesById(id);
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [Route("api/RolePermission/Put")]
        public RolePermission Put(RolePermission role)
        {
            try
            {
                return rolerepository.Update(role);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("api/RolePermission/Get/{id}")]
        public RolePermission Get(Guid id)
        {
            try
            {
                var data = rolerepository.GetById(id);
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("api/RolePermission/Delete/{id}")]
        public bool  Delete(Guid id)
        {
            try
            {
                //rolerepository.Delete(id);
                var data = _customerService.DeletebyRoleId(id);
                //if(data == true)
                //{
                //    return true
                //}
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}