using System.Linq.Expressions;

namespace Optix.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, int limit);
        Task<int> CountAsync();
    }
}
