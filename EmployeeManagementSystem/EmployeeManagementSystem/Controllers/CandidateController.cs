using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using EmployeeMangmentSystem.Resources;
using Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class CandidateController : Controller
    {
        // GET: Candidate
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<DisplayCandidateViewModel>>("api/Candidate/GetCandidateList");
            return View(data);
        }

        // GET: Candidate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Candidate/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Designation = await APIHelpers.GetAsync<List<Designation>>("api/Designation/GetDesignations");
            ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
            ViewBag.Technology = await APIHelpers.GetAsync<List<Technologies>>("api/Technology/GetTechnologies");
            return View();
        }

        // POST: Candidate/Create
        [HttpPost]
        public async Task<ActionResult> Create(CandidateViewModel model)
        {
            try
            {
                string dt = Request["Birthdate"];
                ModelState.Remove("Id");
                ModelState.Remove("Experience");
                ModelState.Remove("BirthDate");
                model.Skills = string.Join(",", Request["Skill"]);
                model.Technology = string.Join(",", Request["Technology"]);
                var d = decimal.Parse("20.03");
                var d2 = Convert.ToDecimal("20.03");
                if (ModelState.IsValid)
                {
                    model.BirthDate = DateTime.ParseExact(dt,"MM/dd/yyyy",null);
                    var month = Convert.ToDecimal(Request["Month"]);
                    if (month < 10)
                    {
                        model.Experience = Convert.ToDecimal(Request["Year"] + "." + "0" + Request["Month"]);
                    }
                    else
                    {
                        model.Experience = Convert.ToDecimal(Request["Year"] + "." + Request["Month"]);
                    }
                    if(model.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<CandidateViewModel>("api/Candidate/Post", model);
                    }
                    else
                    {
                        await APIHelpers.PutAsync<CandidateViewModel>("api/Candidate/Put", model);
                    }
                    TempData["sucess"] = CityResources.create;
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Candidate/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Designation = await APIHelpers.GetAsync<List<Designation>>("api/Designation/GetDesignations");
            ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
            ViewBag.Technology = await APIHelpers.GetAsync<List<Technologies>>("api/Technology/GetTechnologies");
            var data = await APIHelpers.GetAsync<CandidateViewModel>("api/Candidate/Get/" + id);
            string s = data.Experience.ToString("0.00", CultureInfo.InvariantCulture);
            string[] parts = s.Split('.');
            ViewBag.Year = parts[0];
            ViewBag.Month = parts[1];
            ViewBag.birthdate = data.BirthDate;
            return View("create", data);
        }


        // POST: Candidate/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                await APIHelpers.DeleteAsync<bool>("api/Candidate/Delete/" + id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}
