using EmployeeManager.Domain.Interfaces.Common;
using EmployeeManager.Domain.Interfaces.DatabaseContext;
using EmployeeManager.Domain.Interfaces.IRepositories;
using EmployeeManager.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlDbContext _context;

        public UnitOfWork(SqlDbContext context)
        {
            _context = context;
            Departments = new DepartmentRepository(_context);
            Employees = new EmployeeRepository(_context);
            Roles = new RoleRepository(_context);
        }

        public IDepartmentRepository Departments { get; init; } = null!;
        public IEmployeeRepository Employees { get; init; } = null!;
        public IRoleRepository Roles { get; init; } = null!;

        public async ValueTask<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
