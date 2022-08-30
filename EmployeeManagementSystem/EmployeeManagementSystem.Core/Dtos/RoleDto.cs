using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Core.Dtos
{
    public class RoleDto
    {
        public string RoleName { get; set; } = null!;
        public int CreatedBy { get; set; }
    }
}
