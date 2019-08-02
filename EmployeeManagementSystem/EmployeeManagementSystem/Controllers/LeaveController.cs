using EmployeeManagementSystem;
using EmployeeManagementSystem.Models;
using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Resources;
using Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LeaveManagementSystem.Controllers
{
    public class LeaveController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationDbContext appcontext;
        public LeaveController(ApplicationUserManager userManager,ApplicationDbContext context)
        {
            appcontext = context;
            _userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Leave
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Leave>>("api/Leave/GetLeavesByEmployee/"+ Guid.Parse(EmployeeManagementSystem.Helper.CommonHelper.GetUserId()));
                if (data == null)
                {
                    data = new List<Leave>();
                }
                ViewBag.Leave = data;
                return View(data.ToList());
            }
            catch (Exception ex)
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
                //var model = new Leave { From = DateTime.Now.Date };
                Leave model = new Leave();
                model.From = DateTime.Now.Date;
                model.To = DateTime.Now.Date;
                //return View(new Leave());
                return View(model);
            }
            catch (Exception ex)
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
        //[ValidateInput(false)]
        public async Task<ActionResult> Create(HttpPostedFileBase[] Attachment,Leave collection)
        {
            try
            {
                ModelState.Remove("Attachment");
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    collection.AssignTo = "";
                    collection.EmployeeId = Guid.Parse(EmployeeManagementSystem.Helper.CommonHelper.GetUserId());
                    collection.LeaveStatus = Enums.LeaveStatus.Pending;
                    collection.Attachment = "";
                    //if (Attachment.FileName.Contains(".pdf") || Attachment.FileName.Contains(".jpg") || Attachment.FileName.Contains(".JPEG"))
                    //{
                    //    Attachment.SaveAs(Server.MapPath("~/LeaveImage/" + Attachment.FileName));
                    //}
                    //else
                    //{

                    //    TempData["error"] = LeaveResources.FileError;
                    //    return View();
                    //}
                    foreach (HttpPostedFileBase file in Attachment)
                    {
                        if(file.FileName.Contains(".pdf") || file.FileName.Contains(".jpg") || file.FileName.Contains(".JPEG"))
                        {
                            if (file != null)
                            {
                                var filename = Path.GetFileName(file.FileName);
                                collection.Attachment =collection.Attachment + " " + filename;
                                var serverpath = Path.Combine(Server.MapPath("~/LeaveImage/") + filename);
                                file.SaveAs(serverpath);
                            }
                        }
                        else
                        {
                            TempData["error"] = LeaveResources.FileError;
                            return View();
                        }
                        
                    }
                    // TODO: Add insert logic here

                    //Server.MapPath("~/LeaveImage/" + collection.Attachment);
                    if (collection.Id == Guid.Empty)
                    {
                        //collection.Attachment = 
                        await APIHelpers.PostAsync<Leave>("api/Leave/Post", collection);
                        var id = Guid.Parse(User.Identity.GetUserId());
                        var role = appcontext.Roles.Where(m => m.Name == "HR").SingleOrDefault();
                        var users = UserManager.Users.Where(_ => _.RoleId == role.Id).SingleOrDefault();
                        string subject = "Request For Leave";
                        TempData["sucess"] = LeaveResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<Leave>("api/Leave/Put", collection);
                        TempData["sucess"] = LeaveResources.update;
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(collection);
                }

            }
            catch(Exception ex)
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
