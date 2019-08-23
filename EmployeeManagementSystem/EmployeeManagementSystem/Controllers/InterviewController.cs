using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Resources;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class InterviewController : Controller
    {
        // GET: Interviews
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<Interviews>>("api/Interview/GetInterviews");
            if (data == null)
            {
                data = new List<Interviews>();
            }
            return View(data.ToList());
        }

        // GET: Interviews/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Interviews/Create
        public async Task<ActionResult> Create()
        {
            var data = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");
            ViewBag.Employee = data;
            ViewBag.Technology = await APIHelpers.GetAsync<List<Technologies>>("api/Technology/GetTechnologies");
            ViewBag.Designation = await APIHelpers.GetAsync<List<Designation>>("api/Designation/GetDesignations");
            return View();
        }

        // POST: Interviews/Create
        [HttpPost]
        public async Task<ActionResult> Create(Interviews model)
        {
            try
            {
                var fromtime = Request["FromTime"];
                var totime = Request["ToTime"];
                var ftime = TimeSpan.Parse(fromtime);
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    await APIHelpers.PostAsync<Interviews>("api/Interviews/Post", model);
                    TempData["sucess"] = InterviewResources.create;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Interviews/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Interviews/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Interviews/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Interviews/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}
