using Optix.Domain.Models;
using Optix.Domain.Services;
using Optix.Domain.Services.Communication;
using Optix.Repository.Interfaces;

namespace Optix.API.Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> m_GenreRepository;

        public GenreService(IRepository<Genre> genreRepository)
        {
            m_GenreRepository = genreRepository;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresForMovieAsync(long movieId)
        {
            return await m_GenreRepository.FindAsync(genre => genre.MovieId == movieId, 0);
        }

        public async Task<ServiceResponse<IEnumerable<Movie>>> GetAllLinkedGenresAsync(IEnumerable<Movie> movies)
        {
            foreach (var movie in movies)
            {
                var genres = await GetAllGenresForMovieAsync(movie.Id);
                movie.Genres = genres.ToList();
            }

            return new ServiceResponse<IEnumerable<Movie>>(movies);
        }
    }
}
