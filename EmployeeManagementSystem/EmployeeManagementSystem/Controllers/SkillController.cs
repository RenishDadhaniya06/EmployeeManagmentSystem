using EmployeeManagementSystem.Models;
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
    public class SkillController : Controller
    {
        // GET: Department
        public async Task<ActionResult> Index()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
                if (data == null)
                {
                    data = new List<Skills>();
                }
                return View(data.ToList());
            }
            catch (Exception)
            {

                return RedirectToAction("AccessDenied", "Error");
            }

        }

        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
                var builder = new PdfBuilder<List<Skills>>(data, Server.MapPath("/Views/Skill/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception)
            {

                return File("AccessDenied", "Error");
            }
        }

        // GET: Designation/Details/5
        public ActionResult Details(Guid id)
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // GET: Designation/Create
        public ActionResult Create()
        {
            try
            {
                return View(new Skills());
            }
            catch (Exception)
            {

                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // POST: Designation/Create
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Skills collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    if (collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<Skills>("api/Skill/Post", collection);
                        TempData["sucess"] = SkillsResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<Skills>("api/Skill/Put", collection);
                        TempData["sucess"] = SkillsResources.update;
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(collection);
                }

            }
            catch
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // GET: Designation/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                return View("Create", await APIHelpers.GetAsync<Skills>("api/Skill/Get/" + id));
            }
            catch (Exception)
            {

                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // POST: Designation/Edit/5


        // POST: Designation/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<Skills>("api/Skill/Delete/" + id);
                TempData["sucess"] = SkillsResources.delete;
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }
    }
}