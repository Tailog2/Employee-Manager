using EmployeeManager.Domain.Core.Models;
using EmployeeManager.Domain.Interfaces.Common;
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
    public class RoleRepository :RepositoryReading<Role>, IRoleRepository
    {
        public RoleRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
