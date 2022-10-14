using EmployeeManager.Domain.Core.Models;
using EmployeeManager.Domain.Interfaces.DatabaseContext;
using EmployeeManager.Domain.Interfaces.IRepositories;
using EmployeeManager.Infrastructure.Data.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : Repository<Employee>,  IEmployeeRepository
    {
        public EmployeeRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Employee>?> FindAsync(Expression<Func<Employee, bool>> predicate)
        {
            return await Context.Set<Employee>()
                .Include(e => e.Department)
                .Include(e => e.EmployeeRole)
                .Where(predicate)
                .ToListAsync();
        }

        public override async Task<Employee?> GetAsync(int id)
        {
            return await Context.Set<Employee>()
                .Include(e => e.Department)
                .Include(e => e.EmployeeRole)
                .SingleOrDefaultAsync(e => e.Id == id);
        }

        public override async Task<IEnumerable<Employee>?> GetAllAsync()
        {
            return await Context.Set<Employee>()
                .Include(e => e.Department)
                .Include(e => e.EmployeeRole)
                .ToListAsync();
        }

        public override void Update(Employee entity)
        {
            UpdateEmployeeRole(entity);
            Context.Set<Employee>().Update(entity);
        }
        private void UpdateEmployeeRole(Employee employee)
        {
            var previousEmployeeRole = Context.Set<EmployeeRole>()
               .Where(er => er.EmployeeId == employee.Id)
               .ToList();

            if (previousEmployeeRole != null && previousEmployeeRole.Any())
                Context.Set<EmployeeRole>().RemoveRange(previousEmployeeRole);

            if (employee.EmployeeRole != null && employee.EmployeeRole.Any())
                Context.Set<EmployeeRole>().AddRange(employee.EmployeeRole);
        }
    }
}
