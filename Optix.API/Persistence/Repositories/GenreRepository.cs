using Microsoft.EntityFrameworkCore;
using Optix.Domain.Models;
using Optix.Repository.Contexts;
using Optix.Repository.Interfaces;
using System.Linq;
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

        public async Task<IEnumerable<Genre>> FindAsync(Expression<Func<Genre, bool>> predicate, int limit)
        {
            return limit > 0
                ? await m_Context.Set<Genre>().Where(predicate).Take(limit).ToListAsync()
                : await m_Context.Set<Genre>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await m_Context.Set<Genre>().ToListAsync();
        }

        public async Task<Genre> GetAsync(long id)
        {
            return await m_Context.Set<Genre>().FindAsync(id);
        }
    }
}
