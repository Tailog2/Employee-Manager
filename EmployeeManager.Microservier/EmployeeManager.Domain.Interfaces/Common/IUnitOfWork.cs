using EmployeeManager.Domain.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Interfaces.Common
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees{ get; }
        IRoleRepository Roles { get; }
        ValueTask<int> CompleteAsync();
    }
}
