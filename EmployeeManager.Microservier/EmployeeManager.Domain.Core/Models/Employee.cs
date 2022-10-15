using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public IEnumerable<EmployeeRole>? EmployeeRole { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
    }
}
