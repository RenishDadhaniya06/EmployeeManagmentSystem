using System.Collections.Generic;

namespace EmployeeMangmentSystem.Repository.Models
{
    public class DisplayRoleModel
    {
        public IEnumerable<RolePermission> RolePermissions { get; set; }
        public RolePermission Role { get; set; }
    }
}
