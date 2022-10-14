﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Core.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal HourlyRate { get; set; }
        public ICollection<EmployeeRole>? EmployeeRole { get; set; }
    }
}
