using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using EmployeeMangmentSystem.Repository.Repository.Interfaces;
using EmployeeMangmentSystem.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmployeeManagementSystem.Controllers.api
{
    public class CandidateController : ApiController
    {
        private IRepository<Candidates> _repository;
        private IRepository<CandidateSkills> _srepository;
        private IRepository<CandidateTechnologies> _trepository;
        private ICustomerService _customerService;

        public CandidateController(IRepository<Candidates> repository,IRepository<CandidateSkills> srepository,IRepository<CandidateTechnologies> trepository,ICustomerService customerService)
        {
            _repository = repository;
            _srepository = srepository;
            _trepository = trepository;
            _customerService = customerService;
        }

        [Route("api/Candidate/GetCandidates")]
        public IEnumerable<Candidates> GetCandidates()
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

        [Route("api/Candidate/GetCandidateList")]
        public async Task<List<DisplayCandidateViewModel>> GetCandidateList()
        {
            try
            {
                var data = await _customerService.GetCandidates();
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("api/Candidate/Get/{id}")]
        public CandidateViewModel Get(Guid id)
        {
            try
            {
                CandidateViewModel model = new CandidateViewModel();
                var data = _repository.GetById(id);
                model.Id = data.Id;
                model.Name = data.Name;
                model.Email = data.Email;
                model.MobileNo = data.MobileNo;
                model.CurrentCity = data.CurrentCity;
                model.Experience = data.Experience;
                model.CurrentSalary = data.CurrentSalary;
                model.ExpectedSalary = data.ExpectedSalary;
                model.NoticePeriod = data.NoticePeriod;
                model.BirthDate = data.BirthDate;
                model.Designation = data.Designation;
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("api/Candidate/Post")]
        public Candidates Post(CandidateViewModel model)
        {
            try
            {
                List<CandidateSkills> skills = new List<CandidateSkills>();
                List<CandidateTechnologies> tech = new List<CandidateTechnologies>();
                Candidates user = new Candidates();
                user.Id = Guid.NewGuid();
                user.Name = model.Name;
                user.Email = model.Email;
                user.MobileNo = model.MobileNo;
                user.CurrentCity = model.CurrentCity;
                user.Experience = model.Experience;
                user.CurrentSalary = model.CurrentSalary;
                user.ExpectedSalary = model.ExpectedSalary;
                user.NoticePeriod = model.NoticePeriod;
                user.BirthDate = model.BirthDate;
                user.Designation = model.Designation;
                var data = _repository.Insert(user);
                foreach(var item in model.Skills.Split(','))
                {
                    skills.Add(new CandidateSkills()
                    {
                        Id = Guid.NewGuid(),
                        CandidateId = data.Id,
                        SkillId = Guid.Parse(item)
                    });
                }
                _srepository.InsertRange(skills);
                foreach(var item in model.Technology.Split(','))
                {
                    tech.Add(new CandidateTechnologies()
                    {
                        Id = Guid.NewGuid(),
                        CandidateId = data.Id,
                        TechnologyId = Guid.Parse(item)
                    });
                }
                _trepository.InsertRange(tech);
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("api/Candidate/Put")]
        public Candidates Put(CandidateViewModel model)
        {
            try
            {
                _customerService.DeleteCandidateSkill(model.Id);
                _customerService.DeleteCandidateTechnology(model.Id);
                List<CandidateSkills> skills = new List<CandidateSkills>();
                List<CandidateTechnologies> tech = new List<CandidateTechnologies>();
                Candidates user = new Candidates();
                user.Id = model.Id;
                user.Name = model.Name;
                user.Email = model.Email;
                user.MobileNo = model.MobileNo;
                user.CurrentCity = model.CurrentCity;
                user.Experience = model.Experience;
                user.CurrentSalary = model.CurrentSalary;
                user.ExpectedSalary = model.ExpectedSalary;
                user.NoticePeriod = model.NoticePeriod;
                user.BirthDate = model.BirthDate;
                user.Designation = model.Designation;


                var data = _repository.Update(user);
                foreach (var item in model.Skills.Split(','))
                {
                    skills.Add(new CandidateSkills()
                    {
                        Id = Guid.NewGuid(),
                        CandidateId = data.Id,
                        SkillId = Guid.Parse(item)
                    });
                }
                 _srepository.InsertRange(skills);
                foreach (var item in model.Technology.Split(','))
                {
                    tech.Add(new CandidateTechnologies()
                    {
                        Id = Guid.NewGuid(),
                        CandidateId = data.Id,
                        TechnologyId = Guid.Parse(item)
                    });
                }
                _trepository.InsertRange(tech);
                return data;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("api/Candidate/Delete/{id}")]
        public bool Delete(Guid id)
        {
            try
            {
                _customerService.DeleteCandidateSkill(id);
                _customerService.DeleteCandidateTechnology(id);
                _repository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("api/Candidate/Filter")]
        [HttpGet]
        public async Task<List<DisplayCandidateViewModel>> GetFilter(string skill, string technology)
        {
            try
            {
                var data = await _customerService.GetFilterCandidate(skill, technology);
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}