using EmployeeMangmentSystem.Repository.Models;
using Helpers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
           ////var data = await APIHelpers.GetAsync<Customer>("api/Home/GetCustomers");
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