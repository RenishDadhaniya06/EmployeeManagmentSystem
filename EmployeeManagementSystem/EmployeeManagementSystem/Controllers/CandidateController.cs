
namespace EmployeeManagementSystem.Controllers
{
    using EmployeeManagementSystem.Helper;
    #region Using
    using EmployeeManagementSystem.Models;
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using EmployeeMangmentSystem.Resources;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    #endregion

    /// <summary>
    /// CandidateController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [SessionTimeout]
    public class CandidateController : Controller
    {
        // GET: Candidate
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<DisplayCandidateViewModel>>("api/Candidate/GetCandidateList");
            ViewBag.Technology = await APIHelpers.GetAsync<List<Technologies>>("api/Technology/GetTechnologies");
            ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
            return View(data);
        }

        // GET: Candidate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        #region Print Method
        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<DisplayCandidateViewModel>>("api/Candidate/GetCandidateList");
                var builder = new PdfBuilder<List<DisplayCandidateViewModel>>(data, Server.MapPath("/Views/Candidate/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception ex)
            {
                return File("AccessDenied", "Error");
                //throw;
            }
        }
        #endregion

        #region Create Method
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
                        TempData["sucess"] = CandidateResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<CandidateViewModel>("api/Candidate/Put", model);
                        TempData["sucess"] = CandidateResources.update;
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        #endregion

        #region Edit Method
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
        #endregion

        #region Delete Method
        // POST: Candidate/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                await APIHelpers.DeleteAsync<bool>("api/Candidate/Delete/" + id);
                TempData["sucess"] = CandidateResources.delete;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        #endregion

        #region Filter Method
        [HttpPost]
        public async Task<ActionResult> GetFilter()
        {
            try
            {
                ViewBag.Technology = await APIHelpers.GetAsync<List<Technologies>>("api/Technology/GetTechnologies");
                ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
                var tech = Request["Technology"];
                var skills = Request["Skills"];
                var data = await APIHelpers.GetAsync<List<DisplayCandidateViewModel>>("api/Candidate/Filter?skill=" + skills + "&technology=" + tech);
                return View("Index", data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
