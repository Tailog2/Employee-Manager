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
    internal class EmployeeRoleEntityConfig : IEntityTypeConfiguration<EmployeeRole>
    {
        public void Configure(EntityTypeBuilder<EmployeeRole> builder)
        {
            builder
                .HasKey(er => new { er.EmployeeId, er.RoleId });
            builder
                .HasOne(er => er.Employee)
                .WithMany(e => e.EmployeeRole)
                .HasForeignKey(er => er.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(er => er.Role)
                .WithMany(r => r.EmployeeRole)
                .HasForeignKey(er => er.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
