
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    #endregion


    /// <summary>
    /// RolePermission
    /// </summary>
    public class RolePermission
    {
        public Guid Id { get; set; }

        public string ModuleName { get; set; }

        public bool Create { get; set; }

        public bool Read { get; set; }

        public bool Edit { get; set; }

        public bool Delete { get; set; }

        public Guid RoleId { get; set; }
    }
}
