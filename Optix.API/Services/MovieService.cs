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
        private string m_Sort, m_SortOrder;

        private int Offset { get => (m_Page - 1) * m_Limit; }

        public int Page { get => m_Page; set => m_Page = value; }
        public int ItemLimit { get => m_Limit; set => m_Limit = value; }
        public string SortBy { get => m_Sort; set => m_Sort = value; }
        public string SortOrder { get => m_SortOrder; set => m_SortOrder = value; }

        public MovieService(IRepository<Movie> movieRepository)
        {
            m_MovieRepository = movieRepository;

            m_Page = 1;
            m_Limit = 0;

            m_Sort = string.Empty;
            m_SortOrder = "asc";
        }

        public async Task<SearchResponse<IEnumerable<Movie>>> GetByGenreAsync(string genre)
        {
            var query = genre.ToLower();

            try
            {
                var movies = await m_MovieRepository.FindAsync(m => m.Genres.Any(g => g.Name.ToLower().StartsWith(query)), 0, m_Sort, m_SortOrder);
                var count = movies.Count();

                movies = movies.Skip(Offset);

                if (m_Limit > 0)
                    movies = movies.Take(m_Limit);

                return new SearchResponse<IEnumerable<Movie>>(count, movies);
            }
            catch (Exception ex)
            {
                return new SearchResponse<IEnumerable<Movie>>(ex.Message);
            }
        }

        public async Task<Movie> GetByIdAsync(long id)
        {
            return await m_MovieRepository.GetAsync(id);
        }

        public async Task<SearchResponse<IEnumerable<Movie>>> GetByTitleAsync(string title)
        {
            var query = title.ToLower();

            try
            {
                var movies = await m_MovieRepository.FindAsync(m => m.Title.ToLower().StartsWith(query), 0, m_Sort, m_SortOrder);
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
            var movies = await m_MovieRepository.FindAsync(m => m.Id > Offset, m_Limit, m_Sort, m_SortOrder);
            var count = await m_MovieRepository.CountAsync();

            return new SearchResponse<IEnumerable<Movie>>(count, movies);
        }
    }
}
