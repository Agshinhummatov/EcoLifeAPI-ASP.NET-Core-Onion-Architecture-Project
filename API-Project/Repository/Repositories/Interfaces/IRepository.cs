using Domain.Common;
using System.Linq.Expressions;

namespace Repository.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int? id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SoftDeleteAsync(int? id);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T,bool>> expression = null);

        Task<bool> IsExsist(Expression<Func<T, bool>> expression);
    }
}
