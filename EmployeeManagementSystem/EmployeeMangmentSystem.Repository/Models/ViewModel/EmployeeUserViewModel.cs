
namespace EmployeeMangmentSystem.Repository.Models.ViewModel
{
    #region Using
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    #endregion


    /// <summary>
    /// EmployeeUserViewModel
    /// </summary>
    /// <seealso cref="EmployeeMangmentSystem.Repository.Models.Employee" />
    [NotMapped]
    public class EmployeeUserViewModel : Employee
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Skills { get; set; }
    }
}
