using System;

namespace EmployeeMangmentSystem.Repository.Models
{
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
