using EmployeeMangmentSystem.Repository.Models;
using Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Setting/Create
        [HttpPost]
        public async Task<ActionResult> Create(SettingView collection,HttpPostedFileBase Logo)
        {
            try
            {
                //HttpPostedFileBase file;
                Logo.SaveAs(Server.MapPath("~/Images/" + Logo.FileName));
                collection.Logo = Logo.FileName;
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
