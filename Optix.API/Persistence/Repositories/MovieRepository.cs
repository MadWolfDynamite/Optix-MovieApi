using Microsoft.EntityFrameworkCore;
using Optix.Domain.Models;
using Optix.Repository.Contexts;
using Optix.Repository.Interfaces;
using System.Data;
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

        public async Task<IEnumerable<Movie>> FindAsync(Expression<Func<Movie, bool>> predicate, int limit, string sortMember, string sortOrder)
        {
            var dbSet = m_Context.Set<Movie>().Where(predicate);

            dbSet = SortMovies(dbSet, sortMember, sortOrder);

            if (limit > 0)
                dbSet = dbSet.Take(limit);

            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(string sortMember, string sortOrder)
        {
            var dbSet = m_Context.Set<Movie>().AsQueryable();

            dbSet = SortMovies(dbSet, sortMember, sortOrder);

            return await dbSet.ToListAsync();
        }

        public async Task<Movie> GetAsync(long id)
        {
            return await m_Context.Set<Movie>().FindAsync(id);
        }

        private static IQueryable<Movie> SortMovies(IQueryable<Movie> dbSet, string sortMember, string sortOrder)
        {
            switch (sortMember.ToLower())
            {
                case "title":
                case "name":
                    dbSet = sortOrder.ToLower().Equals("desc")
                        ? dbSet.OrderByDescending(m => m.Title)
                        : dbSet.OrderBy(m => m.Title);
                    break;
                case "releasedate":
                case "release_date":
                case "date":
                    dbSet = sortOrder.ToLower().Equals("desc")
                        ? dbSet.OrderByDescending(m => m.ReleaseDate)
                        : dbSet.OrderBy(m => m.ReleaseDate);
                    break;
            }

            return dbSet;
        }
    }
}
