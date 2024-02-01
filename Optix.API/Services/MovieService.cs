using Optix.Domain.Models;
using Optix.Domain.Services;
using Optix.Domain.Services.Communication;
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

        public Task<SearchResponse<IEnumerable<Movie>>> GetByGenreAsync(string genre)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> GetByIdAsync(long id)
        {
            return await m_MovieRepository.GetAsync(id);
        }

        public async Task<SearchResponse<IEnumerable<Movie>>> GetByTitleAsync(string title)
        {
            try
            {
                var query = title.ToLower();
                var movies = await m_MovieRepository.FindAsync(m => m.Title.ToLower().StartsWith(query), 0);
                var count = movies.Count();

                movies = movies.Skip(Offset);

                if (m_Limit > 0)
                    movies = movies.Take(m_Limit);

                return new SearchResponse<IEnumerable<Movie>>(count, movies);
            }
            catch (Exception ex)
            {
                return new SearchResponse<IEnumerable<Movie>>(ex);
            }
        }

        public async Task<SearchResponse<IEnumerable<Movie>>> ListAsync()
        {
            var movies = await m_MovieRepository.FindAsync(m => m.Id > Offset, m_Limit);
            var count = await m_MovieRepository.CountAsync();

            return new SearchResponse<IEnumerable<Movie>>(count, movies);
        }
    }
}
