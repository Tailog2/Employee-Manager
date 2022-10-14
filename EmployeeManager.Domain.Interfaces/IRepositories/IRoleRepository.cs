using EmployeeManager.Domain.Core.Models;
using EmployeeManager.Domain.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Interfaces.IRepositories
{
    public interface IRoleRepository : IRepositoryReading<Role>
    {
    }
}
