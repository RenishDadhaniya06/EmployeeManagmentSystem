
namespace EmployeeManagementSystem.Controllers.api
{
    using EmployeeManagementSystem.Helper;
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using EmployeeMangmentSystem.Repository.Repository.Interfaces;
    using EmployeeMangmentSystem.Services.Services;
    using System;
    using System.Collections.Generic;
    using System.Web;
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
                return await _customerService.GetProjects(); 
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Route("api/Project/GetProjectsByUserId/{id}")]
        public async System.Threading.Tasks.Task<IEnumerable<ProjectTeamViewModel>> GetProjectsByUserId(string id)
        {
            try
            {
                return await _customerService.GetProjectsByUserId(id);
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
                return updatedata;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        [Route("api/Project/PostTeam")]
        [HttpGet]
        public void PostTeam(string id,string pid)
        {
            try
            {
                var proid = Guid.Parse(pid);
                _teamrepository.DeleteWhere(_ => _.ProjectId == proid);
                List<ProjectTeams> teams = new List<ProjectTeams>();
                foreach (var data in id.Split(','))
                {
                    teams.Add(new ProjectTeams()
                    {
                        Id = Guid.NewGuid(),
                        ProjectId = proid,
                        CurrentlyWorking = true,
                        UserId = Guid.Parse(data),
                    });
                }
                _teamrepository.InsertRange(teams);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Delete Method
        [Route("api/Project/Delete/{id}")]
        public bool Delete(Guid id)
        {
            try
            {
                _teamrepository.DeleteWhere(_ => _.ProjectId == id);
                _repository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        [Route("api/Project/UpdateWorkingStatus")]
        [HttpGet]
        public void UpdateWorkingStatus(string id,string proid)
        {
            try
            {
                //ProjectTeams data = new ProjectTeams();
                var userid = Guid.Parse(id);
                var projectid = Guid.Parse(proid);
                var data = _teamrepository.GetFirstOrDefault(_ => _.ProjectId == projectid && _.UserId == userid);
                if(data.CurrentlyWorking == true)
                {
                    data.CurrentlyWorking = false;
                    _teamrepository.Update(data);
                }
                else
                {
                    data.CurrentlyWorking = true;
                    _teamrepository.Update(data);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("api/Project/ChangeWorkingStatus/{id}")]
        [HttpGet]
        public void ChangeWorkingStatus(string id)
        {
            try
            {
                var teamId = Guid.Parse(id);
                var data = _teamrepository.GetById(teamId);
                if(data.CurrentlyWorking == true)
                {
                    data.CurrentlyWorking = false;
                    _teamrepository.Update(data);
                }
                else
                {
                    data.CurrentlyWorking = true;
                    _teamrepository.Update(data);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("api/Project/DeleteMember")]
        [HttpGet]
        public bool DeleteMember(string id,string proid)
        {
            try
            {
                var userid = Guid.Parse(id);
                var projectid = Guid.Parse(proid);
                var data = _teamrepository.GetFirstOrDefault(_ => _.ProjectId == projectid && _.UserId == userid);
                _teamrepository.Delete(data.Id);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


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
