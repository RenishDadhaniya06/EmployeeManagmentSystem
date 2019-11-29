
namespace EmployeeManagementSystem.Controllers
{
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using EmployeeMangmentSystem.Resources;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    #endregion


    /// <summary>
    /// ProjectController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ProjectController : Controller
    {
        // GET: Project
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<Projects>>("api/Project/GetProjects");
            if (data == null)
            {
                data = new List<Projects>();
            }
            return View(data.ToList());
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Project/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                ViewBag.Employees = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");
                return View(new ProjectTeamViewModel());
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(HttpPostedFileBase[] Documents, ProjectTeamViewModel collection)
        {
            try
            {
                string employees = string.Join(",", Request["Employees"]);
                ModelState.Remove("Id");
                ModelState.Remove("Documents");
                ModelState.Remove("EmployeeId");
                if (ModelState.IsValid)
                {
                    collection.Documents = "";
                    foreach (HttpPostedFileBase file in Documents)
                    {

                        if (file != null)
                        {
                            var filename = Path.GetFileName(file.FileName);
                            collection.Documents = collection.Documents + " " + filename;
                            var serverpath = Path.Combine(Server.MapPath("~/ProjectDocuments/") + filename);
                            file.SaveAs(serverpath);
                        }

                    }
                    //ViewBag.Employees = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");  For when redirect to same view this statement is ndeeded for Employee Dropdown
                    if (collection.Id == Guid.Empty)
                    {
                        //var data = await APIHelpers.PostAsync<Projects>("api/Project/Post", collection);
                        //collection.Id = data.Id;
                        await APIHelpers.PostAsync<Projects>("api/Project/Post", collection);
                        TempData["sucess"] = ProjectResources.create;
                    }
                    else
                    {
                        collection.EmployeeId = employees;
                        await APIHelpers.PutAsync<Projects>("api/Project/Put", collection);
                        TempData["sucess"] = ProjectResources.update;
                    }
                    return RedirectToAction("Index");
                    //return View("Create", collection);  //For redirect to same view
                }
                else
                {
                    return View(collection);
                }
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Project/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                ViewBag.Employees = await APIHelpers.GetAsync<List<Employee>>("api/Employee/GetEmployees");
                return View("Create", await APIHelpers.GetAsync<ProjectTeamViewModel>("api/Project/GetTeam/" + id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<bool>("api/Project/Delete/" + id);
                TempData["sucess"] = ProjectResources.delete;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public async Task<ActionResult> Create(HttpPostedFileBase[] Documents, Projects collection)
        //{
        //    try
        //    {
        //        ModelState.Remove("Id");
        //        ModelState.Remove("Documents");
        //        if (ModelState.IsValid)
        //        {
        //            collection.Documents = "";
        //            foreach (HttpPostedFileBase file in Documents)
        //            {

        //                if (file != null)
        //                {
        //                    var filename = Path.GetFileName(file.FileName);
        //                    collection.Documents = collection.Documents + " " + filename;
        //                    var serverpath = Path.Combine(Server.MapPath("~/ProjectDocuments/") + filename);
        //                    file.SaveAs(serverpath);
        //                }

        //            }
        //            if (collection.Id == Guid.Empty)
        //            {
        //                await APIHelpers.PostAsync<Projects>("api/Project/Post", collection);
        //                TempData["sucess"] = ProjectResources.create;
        //            }
        //            else
        //            {
        //                await APIHelpers.PutAsync<Projects>("api/Project/Put", collection);
        //                TempData["sucess"] = ProjectResources.update;
        //            }
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            return View(collection);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }
        //}

    }
}
