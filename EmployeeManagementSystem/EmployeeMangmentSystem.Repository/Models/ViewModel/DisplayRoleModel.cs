using System;
using System.Collections.Generic;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class DisplayRoleModel
    {
        public IEnumerable<RolePermission> RolePermissions { get; set; }
        public Guid Role { get; set; }
    }
}
