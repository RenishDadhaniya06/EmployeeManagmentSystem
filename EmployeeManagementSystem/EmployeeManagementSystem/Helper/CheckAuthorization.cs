using System.Web.Mvc;
using System.Web.Routing;

namespace EmployeeManagementSystem.Helper
{

    public class CheckAuthorization : AuthorizeAttribute
    {


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            CheckIfUserIsAuthenticated(filterContext);
        }

        private void CheckIfUserIsAuthenticated(AuthorizationContext filterContext)
        {
            // If Result is null, we're OK: the user is authenticated and authorized. 
            // If here, you're getting an HTTP 401 status code. In particular,
            // filterContext.Result is of HttpUnauthorizedResult type. Check Ajax here. 
            if (filterContext.HttpContext.User.Identity.IsAuthenticated || CommonHelper.IsSuperAdmin())
            {
                return;
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
               RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
            }

        }
    }
}