using EmployeeManager.Domain.Core.Models;
using EmployeeManager.Infrastructure.Data.EntityConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Interfaces.DatabaseContext
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmployeeEntityConfig());
            modelBuilder.ApplyConfiguration(new DepartmentEnitityConfig());
            modelBuilder.ApplyConfiguration(new RoleEnitityConfig());
            modelBuilder.ApplyConfiguration(new EmployeeRoleEntityConfig());
        }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<EmployeeRole> EmployeesRoles { get; set; } = null!;
    }
}
