
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
    /// OpeningController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [SessionTimeout]
    public class OpeningController : Controller
    {
        // GET: Opening
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<OpeningsViewModel>>("api/Openings/GetOpenings");
            if(data == null)
            {
                data = new List<OpeningsViewModel>();
            }
            return View(data);
        }

        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<OpeningsViewModel>>("api/Openings/GetOpenings");
                var builder = new PdfBuilder<List<OpeningsViewModel>>(data, Server.MapPath("/Views/Opening/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception ex)
            {
                return File("AccessDenied", "Error");
                //throw;
            }
        }

        // GET: Opening/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Opening/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Technology = await APIHelpers.GetAsync<List<Technologies>>("api/Technology/GetTechnologies");
            ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
            ViewBag.Department = await APIHelpers.GetAsync<List<Departments>>("api/Department/GetDepartments");
            return View();
        }

        
        // POST: Opening/Create
        [HttpPost]
        public async Task<ActionResult> Create(Openings model)
        {
            try
            {
                ModelState.Remove("Id");
                ModelState.Remove("CreatedDate");
                ModelState.Remove("Experience");
                if (ModelState.IsValid)
                {
                    var month = Convert.ToDecimal(Request["Month"]);
                    if(month < 10)
                    {
                        month = Convert.ToDecimal("0" + month);
                    }
                    else
                    {
                        month = Convert.ToDecimal(Request["Month"]);
                    }
                    var exp = Request["Year"] + "." + month;
                    model.Experience = Convert.ToDecimal(exp);
                    model.Skills = string.Join(",", Request["Skill"]);
                    if (model.Id == Guid.Empty)
                    {
                        model.CreatedDate = DateTime.Now.Date;
                        //model.Skills = string.Join(",", Request["Skill"]);
                        var data = await APIHelpers.PostAsync<Openings>("api/Openings/Post",model);
                        TempData["sucess"] = OpeningResources.create;
                    }
                    else
                    {
                        var data = await APIHelpers.PutAsync<Openings>("api/Openings/Put", model);
                        TempData["sucess"] = OpeningResources.update;
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Opening/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Technology = await APIHelpers.GetAsync<List<Technologies>>("api/Technology/GetTechnologies");
            ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
            ViewBag.Department = await APIHelpers.GetAsync<List<Departments>>("api/Department/GetDepartments");
            var data = await APIHelpers.GetAsync<Openings>("api/Openings/Get/" + id);
            string s = data.Experience.ToString("0.00", CultureInfo.InvariantCulture);
            string[] parts = s.Split('.');
            //int i1 = int.Parse(parts[0]);
            //int i2 = int.Parse(parts[1]);
            ViewBag.Year = parts[0];
            ViewBag.Month = parts[1];
            return View("create", data);
        }

       
        // POST: Opening/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                await APIHelpers.DeleteAsync<Openings>("api/Openings/Delete/" + id);
                TempData["sucess"] = OpeningResources.delete;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }
    }
}
