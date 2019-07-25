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

namespace LeaveManagementSystem.Controllers
{
    public class LeaveController : Controller
    {
        // GET: Leave
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Leave>>("api/Leave/GetLeaves");
                if (data == null)
                {
                    data = new List<Leave>();
                }
                ViewBag.Leave = data;
                return View(data.ToList());
            }
            catch (Exception)
            {

                return RedirectToAction("AccessDenied", "Error");
            }

        }

        /// <summary>
        /// Prints this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Leave>>("api/Leave/GetLeaves");
                var builder = new PdfBuilder<List<Leave>>(data, Server.MapPath("/Views/Print/Pdf.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception)
            {

                return File("AccessDenied", "Error");
            }
        }

        // GET: Leave/Details/5
        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
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

        // GET: Leave/Create
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Leave>>("api/Leave/GetLeaves");
                ViewBag.Leave = data;
                return View(new Leave());
            }
            catch (Exception)
            {

                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // POST: Leave/Create
        /// <summary>
        /// Creates the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Leave collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    collection.EmployeeId = Guid.Parse(EmployeeManagementSystem.Helper.CommonHelper.GetUserId());
                    // TODO: Add insert logic here
                    if (collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<Leave>("api/Leave/Post", collection);
                        TempData["sucess"] = CommonResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<Leave>("api/Leave/Put", collection);
                        TempData["sucess"] = CommonResources.update;
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

        // GET: Leave/Edit/5
        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>

        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Leave>>("api/Leave/GetLeaves");
                ViewBag.Leave = data;
                return View("Create", await APIHelpers.GetAsync<Leave>("api/Leave/Get/" + id));
            }
            catch (Exception)
            {

                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // POST: Leave/Delete/5
        /// <summary>
        /// Deletes the confirm.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<Leave>("api/Leave/Delete/" + id);
                TempData["sucess"] = CommonResources.delete;
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
