using EmployeeMangmentSystem.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    
    #region Usingusing EmployeeMangmentSystem.Resources;
    using System.Collections.Generic;
    using EmployeeMangmentSystem.Resources;
    using System.ComponentModel.DataAnnotations;
    #endregion


    /// <summary>
    /// ExternalLoginConfirmationViewModel
    /// </summary>
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    /// <summary>
    /// ExternalLoginListViewModel
    /// </summary>
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

  
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// VerifyCodeViewModel
    /// </summary>
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// ForgotViewModel
    /// </summary>
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    /// <summary>
    /// LoginViewModel
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// RegisterViewModel
    /// </summary>
    public class RegisterViewModel
    {

        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        [DataType(DataType.Password)]
        [Display(Name = "lblPassword", ResourceType = typeof(RegestrationResources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public Status UserStatus { get; set; }

        public bool IsSuperAdmin { get; set; }

        public string RoleId { get; set; }

        //[Display(Name = "lblIsActive", ResourceType = typeof(RegestrationResources))]
        //public bool IsActive { get; set; } 
    }

    /// <summary>
    /// Status
    /// </summary>
    public enum Status
    {
        Active = 0,
        InActive = 1,
        Suspended = 2,
        Block = 3
    }

    /// <summary>
    /// RoleViewModel
    /// </summary>
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="Role Name is Required")]
        [Display(Name ="Role Name")]
        public string Name { get; set; }

    }

    /// <summary>
    /// ResetPasswordViewModel
    /// </summary>
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    /// <summary>
    /// ForgotPasswordViewModel
    /// </summary>
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
