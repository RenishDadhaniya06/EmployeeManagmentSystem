
namespace EmployeeManagementSystem.Controllers
{
    using EmployeeManagementSystem.Helper;
    #region Using
    using EmployeeMangmentSystem.Repository.Models;
    using Helpers;
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    #endregion


    /// <summary>
    /// SettingController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [SessionTimeout]
    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }

        // GET: Setting/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Setting/Create
        public async Task<ActionResult> Create()
        {
            var data = await APIHelpers.GetAsync<SettingView>("api/Setting/GetSettings");
            return View(data);
        }

        // POST: Setting/Create
        [HttpPost]
        public async Task<ActionResult> Create(HttpPostedFileBase Logo, SettingView collection)
        {
            try
            {
                //HttpPostedFileBase file;
                if (Logo != null && !string.IsNullOrEmpty(Logo.FileName)) { 
                Logo.SaveAs(Server.MapPath("~/Images/" + Logo.FileName));
                collection.Logo = Logo.FileName;
                }
                // TODO: Add insert logic here
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    await APIHelpers.PostAsync<SettingView>("api/Setting/Post",collection);
                    TempData["sucess"] = "Setting Updated Successfully";
                }
                return RedirectToAction("Create");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

    }
}
