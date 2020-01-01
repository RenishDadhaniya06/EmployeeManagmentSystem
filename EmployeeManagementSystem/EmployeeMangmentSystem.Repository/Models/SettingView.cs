
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    using System.ComponentModel.DataAnnotations;
    #endregion


    /// <summary>
    /// SettingView
    /// </summary>
    public class SettingView
    {
        public Guid Id { get; set; }

        [Display(Name ="Header")]
        public string Header { get; set; }

        [Display(Name ="SubHeader")]
        public string SubHeader { get; set; }

        [Display(Name ="Logo")]
        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [Display(Name ="Show Date")]
        public bool ShowDate { get; set; }

        public string Host { get; set; }

        public Int32 Port { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Send Daily Report?")]
        public bool DailySalesReport { get; set; }
    }
}
