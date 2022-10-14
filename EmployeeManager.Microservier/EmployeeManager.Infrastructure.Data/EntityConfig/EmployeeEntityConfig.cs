using EmployeeManager.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Data.EntityConfig
{
    internal class EmployeeEntityConfig : IEntityTypeConfiguration<Employee>
    {
        //DeleteHourlyPaymentColumnInEmployeeTable
        //SetPrecisionTo10And10ForHourlyRateColumnInRoleTable
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.LastName).HasMaxLength(50);
            builder
                .HasOne(e => e.Department)
                .WithMany(s => s.Employees)
                .HasForeignKey(g => g.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
