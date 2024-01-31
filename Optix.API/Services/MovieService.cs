using Optix.Domain.Models;
using Optix.Domain.Services;
using Optix.Repository.Interfaces;

namespace Optix.API.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> m_MovieRepository;

        private int m_Page, m_Limit;

        public int Page { get => m_Page; set => m_Page = value; }
        public int ItemLimit { get => m_Limit; set => m_Limit = value; }

        public MovieService(IRepository<Movie> movieRepository)
        {
            m_MovieRepository = movieRepository;

            m_Page = 1;
            m_Limit = 0;
        }

        public Task<IEnumerable<Movie>> GetByGenreAsync(string genre)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> GetByIdAsync(long id)
        {
            return await m_MovieRepository.GetAsync(id);
        }

        public Task<IEnumerable<Movie>> GetByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> ListAsync()
        {
            var offset = (m_Page - 1) * m_Limit;
            return await m_MovieRepository.FindAsync(m => m.Id > offset, m_Limit);
        }
    }
}
