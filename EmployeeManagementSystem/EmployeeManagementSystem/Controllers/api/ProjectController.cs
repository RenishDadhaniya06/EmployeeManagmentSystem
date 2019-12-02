
namespace EmployeeManagementSystem.Controllers.api
{
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using EmployeeMangmentSystem.Repository.Repository.Interfaces;
    using EmployeeMangmentSystem.Services.Services;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    #endregion

    /// <summary>
    /// ProjectController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ProjectController : ApiController
    {
        #region Properties
        private IRepository<Projects> _repository;
        private IRepository<ProjectTeams> _teamrepository;
        private ICustomerService _customerService;
        #endregion

        #region Constructor
        public ProjectController(IRepository<Projects> repository, IRepository<ProjectTeams> teamrepository,ICustomerService customerService)
        {
            _repository = repository;
            _teamrepository = teamrepository;
            _customerService = customerService;
        }
        #endregion

        #region Get All Method
        [Route("api/Project/GetProjects")]
        public async System.Threading.Tasks.Task<IEnumerable<ProjectTeamViewModel>> GetProjects()
        {
            try
            {
                var data = await _customerService.GetProjects();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetbyId Method
        [Route("api/Project/Get/{id}")]
        public Projects Get(Guid id)
        {
            try
            {
                var data = _repository.GetById(id);
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetbyId Method
        [Route("api/Project/GetTeam/{id}")]
        public async System.Threading.Tasks.Task<ProjectTeamViewModel> GetTeam(Guid id)
        {
            try
            {

                 List<ProjectTeamViewModel> projectList = await _customerService.GetProjects();
                var single = projectList.Find(_=>_.Id == id);
             
                return single;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Route("api/Project/GetTeamById/{id}")]
        public async System.Threading.Tasks.Task<List<TeamViewModel>> GetTeamById(Guid id)
        {
            try
            {

                List<TeamViewModel> projectList = await _customerService.TeamByProjectIdGet(id);

                return projectList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Post Method
        [Route("api/Project/Post")]
        public Projects Post(ProjectTeamViewModel model)
        {
            try
            {
                Projects pro = new Projects
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Description = model.Description,
                    ProjectStatus = model.ProjectStatus,
                    ProjectType = model.ProjectType,
                    Documents = model.Documents
                };
                var data = _repository.Insert(pro);
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Put Method
        [Route("api/Project/Put")]
        public Projects Put(ProjectTeamViewModel model)
        {
            try
            {
                Projects pro = new Projects
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    ProjectStatus = model.ProjectStatus,
                    ProjectType = model.ProjectType,
                    Documents = model.Documents
                };
                var updatedata = _repository.Update(pro);
                _teamrepository.DeleteWhere(_=>_.ProjectId == pro.Id);
                List<ProjectTeams> teams = new List<ProjectTeams>();
                foreach(var data in model.EmployeeId.Split(','))
                {
                    teams.Add(new ProjectTeams()
                    {
                        Id = Guid.NewGuid(),
                        ProjectId = model.Id,
                        CurrentlyWorking = true,
                        UserId = Guid.Parse(data),
                    });
                }
                _teamrepository.InsertRange(teams);
                return updatedata;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Delete Method
        [Route("api/Project/Delete/{id}")]
        public bool Delete(Guid id)
        {
            try
            {
                _repository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion


        //#region Post Method
        //[Route("api/Project/Post")]
        //public Projects Post(Projects model)
        //{
        //    try
        //    {
        //        model.Id = Guid.NewGuid();
        //        var data = _repository.Insert(model);
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        //#endregion
    }
}
