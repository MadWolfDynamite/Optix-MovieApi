using Microsoft.EntityFrameworkCore;
using Optix.Domain.Models;
using Optix.Repository.Contexts;
using Optix.Repository.Interfaces;
using System.Linq.Expressions;

namespace Optix.API.Persistence.Repositories
{
    public class GenreRepository : IRepository<Genre>
    {
        protected readonly DbContext m_Context;

        public GenreRepository(MovieDbContext context)
        {
            m_Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<int> CountAsync()
        {
            return m_Context.Set<Genre>().CountAsync();
        }

        public async Task<IEnumerable<Genre>> FindAsync(Expression<Func<Genre, bool>> predicate, int limit, string sortMember, string sortOrder)
        {
            var dbSet = m_Context.Set<Genre>().Where(predicate);

            if (limit > 0)
                dbSet = dbSet.Take(limit);

            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Genre>> GetAllAsync(string sortMember, string sortOrder)
        {
            return await m_Context.Set<Genre>().ToListAsync();
        }

        public async Task<Genre> GetAsync(long id)
        {
            return await m_Context.Set<Genre>().FindAsync(id);
        }
    }
}
