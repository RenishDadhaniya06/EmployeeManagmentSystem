
namespace EmployeeManagementSystem.Helper
{
    #region Using
    using System.Web.Mvc;
    using System.Web.Routing;
    #endregion

    /// <summary>
    /// CheckAuthorization
    /// </summary>
    /// <seealso cref="System.Web.Mvc.AuthorizeAttribute" />
    public class CheckAuthorization : AuthorizeAttribute
    {
        /// <summary>
        /// Called when a process requests authorization.
        /// </summary>
        /// <param name="filterContext">The filter context, which encapsulates information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />.</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            CheckIfUserIsAuthenticated(filterContext);
        }

        /// <summary>
        /// Checks if user is authenticated.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
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