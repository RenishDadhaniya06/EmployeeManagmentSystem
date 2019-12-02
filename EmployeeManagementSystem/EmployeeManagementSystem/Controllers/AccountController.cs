
namespace EmployeeManagementSystem.Controllers
{
    #region Using
    using EmployeeManagementSystem.Models;
    using Helpers;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    #endregion

    /// <summary>
    /// AccountController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

        #region Constructor
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,ApplicationDbContext context)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _applicationDbContext = context;
        }
        #endregion

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        #region Login Method
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                var user = await SignInManager.UserManager.FindAsync(model.Email, model.Password);

                if (user != null && user.UserStatus == Status.Active)
                {
                    if(user.UserStatus == Status.InActive)
                    {
                        TempData["error"] = "This Account is inactive";
                        return View();
                    }
                    else if(user.UserStatus == Status.Block)
                    {
                        TempData["error"] = "This Account is blocked";
                        return View();
                    }
                    else if(user.UserStatus == Status.Suspended)
                    {
                        TempData["error"] = "This Account is suspended";
                        return View();
                    }
                    else
                    {
                        var ctx = Request.GetOwinContext();
                        var identity = await UserManager.CreateIdentityAsync(
                        user, DefaultAuthenticationTypes.ApplicationCookie);
                        var authManager = ctx.Authentication;
                        authManager.SignIn(identity);
                        TempData["sucess"] = "Sucessfully Login.";
                        return RedirectToAction("Index", "Home");
                    }
                   
                }
                else
                {
                    TempData["error"] = "UserName or Password is wrong! Try Again.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "An Error Occured While Processing Please Try Again.";
                return View();
            }

        }
        #endregion


        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }


        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }

        #region Register Method
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = _applicationDbContext.Roles.Where(m => m.Name == "Guest").SingleOrDefault();
                model.RoleId = data.Id;
                IdentityResult result = await RegiaterUser(model);
                
                if (result.Succeeded)
                {
                    ViewBag.message = "Registered Sucessfully Please Try To Login Now.";

                    return View("Login");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var data = _applicationDbContext.Roles.Where(m => m.Name == "Guest").SingleOrDefault();
                //model.RoleId = data.Id;
                IdentityResult result = await RegiaterUser(model);
                if (result.Succeeded)
                {
                    TempData["sucess"] = "Registered Sucessfully Please Try To Login Now.";
                    return RedirectToAction("CreateSucess", "User");
                }
                else
                {
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
            //return RedirectToAction("Index", "User");
        }

        private async Task<IdentityResult> RegiaterUser(RegisterViewModel model)
        {

            var user = new ApplicationUser { UserName = model.Email, IsSuperAdmin = false, ParentUserID = Guid.Parse("06644856-45f6-4c78-9c19-60781abba7e3"), Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, UserStatus = (Status)Status.InActive, RoleId = model.RoleId };
            var result = await UserManager.CreateAsync(user, model.Password);
           
            if (result.Succeeded)
            {
                string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                SendVerificationLinkEmail(model.Email, callbackUrl);
            }
            return result;
        }


        #region Forgot Password Method
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            //return View(result.Succeeded ? "ConfirmEmail" : "Error");
            if (result.Succeeded)
            {
                var data = await UserManager.FindByIdAsync(userId);
                var guest = _applicationDbContext.Roles.Where(m => m.Name == "Guest").SingleOrDefault();
                //data.UserStatus = Status.Active;
                data.UserStatus = (Status)Status.Active;
                await UserManager.AddToRoleAsync(userId, guest.Name);
                var tempresult = await UserManager.UpdateAsync(data);
                if (tempresult.Succeeded)
                {
                    ApplicationDbContext context = new ApplicationDbContext();
                    await context.SaveChangesAsync();
                    return View("ConfirmEmail");
                }
                
            }
            return View();
            

        }

        [AllowAnonymous]
        public static void SendVerificationLinkEmail(string emailId, string activationcode)
        {
            string subject = "Your account is successfull created";
            string body = "<br/><br/>We are excited to tell you that your account is" +
        " successfully created. Please click on the below link to verify your account" +
        " <br/><br/><a href='" + activationcode + "'>" + activationcode + "</a> ";
            //CommonHelper.SendMail(emailId, subject, body);
        }

        //
        // POST: /Account/LogOff
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}