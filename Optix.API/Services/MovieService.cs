using Optix.Domain.Models;
using Optix.Domain.Services;
using Optix.Repository.Interfaces;

namespace Optix.API.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> m_MovieRepository;

        private int m_Page, m_Limit;

        private int Offset { get => (m_Page - 1) * m_Limit; }

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

        public async Task<IEnumerable<Movie>> GetByTitleAsync(string title)
        {
            var movies = await m_MovieRepository.FindAsync(m => m.Title.StartsWith(title), 0);
            movies = movies.Skip(Offset);

            return m_Limit > 0
                ? movies.Take(m_Limit) 
                : movies;
        }

        public async Task<IEnumerable<Movie>> ListAsync()
        {
            return await m_MovieRepository.FindAsync(m => m.Id > Offset, m_Limit);
        }
    }
}
