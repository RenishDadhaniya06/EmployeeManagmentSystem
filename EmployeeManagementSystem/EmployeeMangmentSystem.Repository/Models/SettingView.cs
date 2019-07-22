using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class SettingView
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name ="Header")]
        public string Header { get; set; }

        [Required]
        [Display(Name ="SubHeader")]
        public string SubHeader { get; set; }

        [Display(Name ="Logo")]
        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [Display(Name ="Show Date")]
        [DataType(DataType.Date)]
        public bool ShowDate { get; set; }

        public string Host { get; set; }

        public Int32 Port { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
