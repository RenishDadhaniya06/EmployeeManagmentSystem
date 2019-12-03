
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
    /// EmployeeController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class EmployeeController : ApiController
    {
        #region Properties
        private IRepository<Employee> _repository;
        private IRepository<CandidateSkills> _skillrepository;
        private IRepository<ProjectTeams> _projectteamrepository;
        private ICustomerService _iCustomerService;
        #endregion

        #region Constructor
        public EmployeeController(IRepository<Employee> repository,ICustomerService customerService,IRepository<CandidateSkills> skillrepository,IRepository<ProjectTeams> projectrepository)
        {
            _repository = repository;
            _iCustomerService = customerService;
            _skillrepository = skillrepository;
            _projectteamrepository = projectrepository;
        }
        #endregion

        #region Get All Method
        [Route("api/Employee/GetEmployees")]
        public IEnumerable<Employee> GetEmployees()
        {
            try
            {
                var data = _repository.GetAll();
                return data;

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion

        #region GetbyId Method
        [Route("api/Employee/Get/{id}")]
        [HttpGet]
        public Employee Get(Guid id)
        {
            var data1 = _repository.GetById(id);
            return data1;
        }
        #endregion

        #region Post Method
        [Route("api/Employee/Post")]
        [HttpPost]
        public Employee Post(EmployeeUserViewModel model)
        {
            try
            {
                List<CandidateSkills> skills = new List<CandidateSkills>();
                Employee emp = new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    BirthDate = model.BirthDate,
                    Address = model.Address,
                    OtherContact = model.OtherContact,
                    Department = model.Department,
                    LeaveBalance = model.LeaveBalance,
                    CurrentSalary = model.CurrentSalary,
                    IsEmailVerified = model.IsEmailVerified,
                    UserId = model.UserId,
                };
                var data2 = _repository.Insert(emp);
                if(model.Skills != "")
                {
                    foreach (var item in model.Skills.Split(','))
                    {
                        skills.Add(new CandidateSkills()
                        {
                            Id = Guid.NewGuid(),
                            CandidateId = data2.Id,
                            SkillId = Guid.Parse(item)
                        });
                    }
                    _skillrepository.InsertRange(skills);
                }
                return data2;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
        #endregion

        #region Put Method
        [Route("api/Employee/Put")]
        [HttpPut]
        public Employee Put(EmployeeUserViewModel model)
        {
            //return _repository.Update(model);
            List<CandidateSkills> skills = new List<CandidateSkills>();
            Employee emp = new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Phone = model.Phone,
                BirthDate = model.BirthDate,
                Address = model.Address,
                OtherContact = model.OtherContact,
                Department = model.Department,
                LeaveBalance = model.LeaveBalance,
                CurrentSalary = model.CurrentSalary,
                IsEmailVerified = model.IsEmailVerified,
                UserId = model.UserId,
            };
            var data2 = _repository.Update(emp);
            _skillrepository.DeleteWhere(_=> _.CandidateId == emp.Id);
            if(model.Skills != "")
            {
                foreach (var item in model.Skills.Split(','))
                {
                    skills.Add(new CandidateSkills()
                    {
                        Id = Guid.NewGuid(),
                        CandidateId = data2.Id,
                        SkillId = Guid.Parse(item)
                    });
                }
                _skillrepository.InsertRange(skills);
            }
            
            return data2;
        }
        #endregion

        #region Delete Method
        [Route("api/Employee/Delete/{id}")]
        [HttpDelete]
        public bool Delete(Guid id)
        {
            var data = Get(id);
            _repository.Delete(id);
            _skillrepository.DeleteWhere(_ => _.CandidateId == id);
            _projectteamrepository.DeleteWhere(_ => _.UserId == data.UserId);
            return true;
        }
        #endregion

        #region Get Employee by email using SP Method
        [Route("api/Employee/GetEmployee")]
        [HttpGet]
        public async Task<Employee> GetEmployee(string email)
        {
            var data = await _iCustomerService.GetEmployee(email);
            return data;
        }
        #endregion

        #region Get Employee by Id using SP Method
        [Route("api/Employee/GetEmployeeUser/{id}")]
        [HttpGet]
        public async Task<EmployeeUserViewModel> GetEmployeeUserViewModel(Guid id)
        {
            try
            {
                var data = await _iCustomerService.GetEmployeeUserViewModel(id);
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        [Route("api/Employee/Projects/{id}")]
        [HttpGet]
        public async Task<List<ProjectViewModel>> GetProjects(Guid id)
        {
            try
            {
                return await _iCustomerService.GetProjectsbyUserId(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
