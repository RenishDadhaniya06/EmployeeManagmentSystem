using EmployeeMangmentSystem.Repository.Models;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [OutputCache(Duration = 86400, VaryByParam = "None")]
    [Authorize]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            //var data = await APIHelpers.GetAsync<Customer>("api/Home/GetCustomers");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}