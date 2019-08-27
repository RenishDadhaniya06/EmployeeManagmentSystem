using EmployeeManagementSystem.Models;
using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using EmployeeMangmentSystem.Resources;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class InterviewerController : Controller
    {
        // GET: Interviews
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<DisplayInterviewModel>>("api/Interviewer/GetInterviewerList");
            if (data == null)
            {
                data = new List<DisplayInterviewModel>();
            }
            return View(data.ToList());
        }

        // GET: Interviews/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<DisplayInterviewModel>>("api/Interviewer/GetInterviewList");
                var builder = new PdfBuilder<List<DisplayInterviewModel>>(data, Server.MapPath("/Views/Interviewer/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception ex)
            {
                return File("AccessDenied", "Error");
            }
        }

        // GET: Interviews/Create
        public async Task<ActionResult> Create()
        {
            var data = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");
            ViewBag.Employee = data;
            ViewBag.Technology = await APIHelpers.GetAsync<List<Technologies>>("api/Technology/GetTechnologies");
            ViewBag.Designation = await APIHelpers.GetAsync<List<Designation>>("api/Designation/GetDesignations");
            ViewBag.fromtime = DateTime.Now.ToString("HH:mm");
            ViewBag.totime = DateTime.Now.AddHours(1).ToString("HH:mm");
            return View();
        }

        // POST: Interviews/Create
        [HttpPost]
        public async Task<ActionResult> Create(Interviewers model)
        {
            try
            {
                var fromtime = Request["FromTime"];
                var totime = Request["ToTime"];
                //var ftime = TimeSpan.Parse(fromtime, CultureInfo.InvariantCulture);
                ModelState.Remove("FromTime");
                ModelState.Remove("ToTime");
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    model.FromTime = DateTime.Parse(fromtime).TimeOfDay;
                    model.ToTime = DateTime.Parse(totime).TimeOfDay;
                    if (model.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<Interviewers>("api/Interviewer/Post", model);
                        TempData["sucess"] = InterviewerResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<Interviewers>("api/Interviewer/Put", model);
                        TempData["sucess"] = InterviewerResources.update;
                    }

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Interviews/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var data = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");
            ViewBag.Employee = data;
            ViewBag.Technology = await APIHelpers.GetAsync<List<Technologies>>("api/Technology/GetTechnologies");
            ViewBag.Designation = await APIHelpers.GetAsync<List<Designation>>("api/Designation/GetDesignations");
            var data1 = await APIHelpers.GetAsync<Interviewers>("api/Interviewer/Get/" + id);
            ViewBag.fromtime = DateTime.Today.Add(data1.FromTime).ToString("HH:mm");
            ViewBag.totime = DateTime.Today.Add(data1.ToTime).ToString("HH:mm");
            return View("create", data1);
        }


        // POST: Interviews/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                await APIHelpers.DeleteAsync<Interviewers>("api/Interviewer/Delete/" + id);
                TempData["sucess"] = InterviewerResources.delete;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }
    }
}
