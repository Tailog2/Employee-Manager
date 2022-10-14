using EmployeeManager.Domain.Core.Models;
using EmployeeManager.Domain.Interfaces.Common;
using EmployeeManager.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Data.Repositories.Common
{
    public abstract class RepositoryReading<TEntity> : IRepositoryReading<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public RepositoryReading(DbContext dbContext)
        {
            Context = dbContext;
        }

        public virtual async Task<TEntity?> GetAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>?> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().AnyAsync(predicate);
        }
    }
}
