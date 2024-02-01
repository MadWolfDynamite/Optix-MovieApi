using Microsoft.EntityFrameworkCore;
using Optix.Domain.Models;
using Optix.Repository.Contexts;
using Optix.Repository.Interfaces;
using System.Linq.Expressions;

namespace Optix.API.Persistence.Repositories
{
    public class MovieRepository : IRepository<Movie>
    {
        protected readonly DbContext m_Context;

        public MovieRepository(MovieDbContext context)
        {
            m_Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> CountAsync()
        {
            return await m_Context.Set<Movie>().IgnoreAutoIncludes().CountAsync();
        }

        public async Task<IEnumerable<Movie>> FindAsync(Expression<Func<Movie, bool>> predicate, int limit)
        {
            return limit > 0
                ? await m_Context.Set<Movie>().Where(predicate).Take(limit).ToListAsync()
                : await m_Context.Set<Movie>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await m_Context.Set<Movie>().ToListAsync();
        }

        public async Task<Movie> GetAsync(long id)
        {
            return await m_Context.Set<Movie>().FindAsync(id);
        }
    }
}
