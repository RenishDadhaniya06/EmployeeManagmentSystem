using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [OutputCache(Duration = 86400, VaryByParam = "None")]
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TempData["sucess"] = "qdq cqwd  qddwqed ";
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