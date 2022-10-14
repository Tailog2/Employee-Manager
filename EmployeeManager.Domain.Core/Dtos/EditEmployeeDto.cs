using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Core.Dtos
{
    public class EditEmployeeDto
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string FirstName { get; set; } = null!;
        [MinLength(3)]
        public string LastName { get; set; } = null!;
        public IEnumerable<int>? RolesIds { get; set; }
        public int DepartmentId { get; set; }
    }
}
