using System.Linq.Expressions;

namespace EmployeeManager.Domain.Interfaces.Common
{
    public interface IRepository<TEntity> : IRepositoryReading<TEntity>, IRepositoryWriting<TEntity> where TEntity : class
    {
    }
}
