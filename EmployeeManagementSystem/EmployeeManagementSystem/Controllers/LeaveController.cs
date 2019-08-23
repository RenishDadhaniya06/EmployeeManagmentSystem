using EmployeeManagementSystem.Models;
using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Models.ViewModel;
using EmployeeMangmentSystem.Resources;
using EmployeeMangmentSystem.Services.Services;
using Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LeaveManagementSystem.Controllers
{
    public class LeaveController : Controller
    {
        //private ApplicationUserManager _userManager;

        private ICustomerService _customerService;

        public LeaveController()
        {

        }
        public LeaveController(ICustomerService customerService)
        {
            //_userManager = userManager;
            _customerService = customerService;
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
            catch (Exception ex)
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
            catch (Exception ex)
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
        [ValidateInput(true)]
        public async Task<ActionResult> Create(HttpPostedFileBase[] Attachment,Leave collection)
        {
            try
            {
                string from = Request["From"];
                string to = Request["To"];
                ModelState.Remove("Attachment");
                ModelState.Remove("Id");
                ModelState.Remove("EmployeeId");
                ModelState.Remove("From");
                ModelState.Remove("To");
                if (ModelState.IsValid)
                {
                    collection.From = DateTime.ParseExact(from, "MM/dd/yyyy", null);
                    collection.To = DateTime.ParseExact(to, "MM/dd/yyyy", null);
                    collection.AssignTo = "";
                    collection.EmployeeId = Guid.Parse(EmployeeManagementSystem.Helper.CommonHelper.GetUserId());
                    collection.LeaveStatus = Enums.LeaveStatus.Pending;
                    collection.Attachment = "";
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
                    if (collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<Leave>("api/Leave/Post", collection);
                        var role = await _customerService.GetHR();
                        string temp = String.Join(",", role);
                        string subject = "Request For Leave";
                        var body = await _customerService.GetLeaveTemplate();
                        var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
                        var userManager = new UserManager<ApplicationUser>(store);
                        ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
                        var content = body.TemplateContent;
                        content.Replace("###CurrentUser###","Dhaval");
                        content = Regex.Replace(content, "###CurrentUser###", user.FirstName + " " + user.LastName);
                        content = Regex.Replace(content, "###FromDate###", collection.From.ToString());
                        content = Regex.Replace(content, "###ToDate###", collection.To.ToString());
                        content = Regex.Replace(content, "###LeaveReason###", collection.Message);
                        CommonHelper.SendMail(temp, subject, content);
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
            catch (Exception ex)
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
            catch(Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetPendingLeave()
        {
            try
            {
                FilterViewModel model = new FilterViewModel();
                var data = await APIHelpers.GetAsync<List<LeaveViewModel>>("api/Leave/GetPendingLeave");
                model.Leaves = data.ToList();
                return View(model);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> ApproveLeave(Guid id,string email)
        {
            try
            {
                var data = await APIHelpers.GetAsync<bool>("api/Leave/ApproveLeaves/" + id);
                if(data == true)
                {
                    var emp = await APIHelpers.GetAsync<Employee>("api/Employee/GetEmployee?Email=" + email);
                    var leave = await APIHelpers.GetAsync<Leave>("api/Leave/Get/" + id);
                    emp.AvailableLeaves = emp.AvailableLeaves - (leave.To.Day - leave.From.Day);
                    await APIHelpers.PutAsync<Employee>("api/Employee/Put", emp);
                    var subject = "Leave";
                    var body = "Congratulations!! Your Leave has been Approved.";
                    CommonHelper.SendMail(email, subject, body);
                    return RedirectToAction("GetPendingLeave","Leave");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> RejectedLeave(Guid id,string email)
        {
            try
            {
                var data = await APIHelpers.GetAsync<bool>("api/Leave/RejectedLeaves/" + id);
                if (data == true)
                {
                    var subject = "Leave";
                    var body = "Sorry, Your Leave has been Rejected. If You have any query Please Contact Administrator or HR";
                    CommonHelper.SendMail(email, subject, body);
                    return RedirectToAction("GetPendingLeave", "Leave");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> GetFilter(FilterViewModel model)
        {
            try
            {
                if (DateTime.Compare(model.LeaveFilters.Fromdate,Convert.ToDateTime("01-01-0001")) == 0 || DateTime.Compare(model.LeaveFilters.Todate,Convert.ToDateTime("01-01-2001")) == 0)
                {
                    return RedirectToAction("GetPendingLeave");
                }
                var data = await APIHelpers.GetAsync<List<LeaveViewModel>>("api/Leave/Filter?name=" + model.LeaveFilters.Name + "&fromdate=" + model.LeaveFilters.Fromdate + "&todate=" + model.LeaveFilters.Todate + "&LeaveType=" + Convert.ToInt32(model.LeaveFilters.leavetype) + "&LeaveStatus=" + Convert.ToInt32(model.LeaveFilters.leavestatus));
                model.Leaves = data;
                return View("GetPendingLeave", model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
