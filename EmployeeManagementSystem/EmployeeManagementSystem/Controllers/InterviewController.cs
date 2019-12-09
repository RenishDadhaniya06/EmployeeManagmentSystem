
namespace EmployeeManagementSystem.Controllers
{
    using EmployeeManagementSystem.Helper;
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using EmployeeMangmentSystem.Resources;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    #endregion


    /// <summary>
    /// InterviewController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class InterviewController : Controller
    {
        // GET: Interview
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<DisplayInterviewModel>>("api/Interview/GetInterviewsList");
            if(data == null)
            {
                data = new List<DisplayInterviewModel>();
            }
            return View(data.ToList());
        }

        // GET: Interview/Details/5
        [HttpPost]
        public async Task<JsonResult> EmployeeDetails(Guid id)
        {
            try
            {
                var data = await APIHelpers.GetAsync<Employee>("api/Employee/Get/" + id);
                return Json(data,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: Interview/Create
        public async Task<ActionResult> Create()
        {
            //var data = await APIHelpers.GetAsync<List<Candidates>>("api/Candidate/GetCandidates");
            //ViewBag.Candidate = new SelectList(data, "Id", "Name", "Email");
            ViewBag.Employee = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");
            ViewBag.emp = new Employee();
            ViewBag.sdate = DateTime.Now.ToString("MM/dd/yyyy");
            ViewBag.stime = DateTime.Now.ToString("HH:mm");
            return View();
        }

        // POST: Interview/Create
        [HttpPost]
        public async Task<ActionResult> Create(Interviews model)
        {
            try
            {
                //var c = Request["CandidateId"];
                var stime = Request["Stime"];
                var sdate = Request["Sdate"];
                DateTime time = Convert.ToDateTime(DateTime.ParseExact(sdate, "MM/dd/yyyy", null) + DateTime.Parse(stime).TimeOfDay);
                ModelState.Remove("Id");
                ModelState.Remove("ScheduleTime");
                if (ModelState.IsValid)
                {
                    model.ScheduleTime = time;
                    if(model.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<Interviews>("api/Interview/Post", model);
                        TempData["sucess"] = InterviewResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<Interviews>("api/Interview/Put", model);
                        TempData["sucess"] = InterviewResources.update;
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Interview/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var data = await APIHelpers.GetAsync<Interviews>("api/Interview/Get/" + id);
            var Candidate = await APIHelpers.GetAsync<Candidates>("api/Candidate/Get/" + data.CandidateId);
            ViewBag.Candidate = Candidate.Name;
            ViewBag.Employee = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");
            ViewBag.emp = await APIHelpers.GetAsync<Employee>("api/Employee/Get/" + data.EmployeeId);
            ViewBag.stime = data.ScheduleTime.ToString("HH:mm");
            ViewBag.sdate = data.ScheduleTime.ToString("MM/dd/yyyy");
            return View("create",data);
        }

        // GET: Interview/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                await APIHelpers.DeleteAsync<Interviews>("api/Interview/Delete/" + id);
                TempData["sucess"] = InterviewResources.delete;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetCandidateDetail(string name)
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Candidates>>("api/Interview/GetCandidateDetail?name=" + name);
                return Json(data,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
