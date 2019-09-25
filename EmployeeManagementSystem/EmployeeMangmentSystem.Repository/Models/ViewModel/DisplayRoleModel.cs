
namespace EmployeeMangmentSystem.Repository.Models
{
    #region Using
    using System;
    using System.Collections.Generic;
    #endregion


    /// <summary>
    /// DisplayRoleModel
    /// </summary>
    public class DisplayRoleModel
    {
        public IEnumerable<RolePermission> RolePermissions { get; set; }
        public Guid Role { get; set; }
    }
}
