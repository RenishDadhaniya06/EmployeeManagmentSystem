
namespace EmployeeManagementSystem.Controllers
{
    #region Using
    using System.Web.Mvc;
    #endregion


    /// <summary>
    /// ErrorController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}