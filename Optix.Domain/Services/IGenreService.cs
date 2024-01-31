using Optix.Domain.Models;

namespace Optix.Domain.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Movie>> GetAllLinkedGenresAsync(IEnumerable<Movie> movies);
        Task<IEnumerable<Genre>> GetAllGenresForMovieAsync(long movieId);
    }
}
