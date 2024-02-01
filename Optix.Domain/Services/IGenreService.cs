using Optix.Domain.Models;
using Optix.Domain.Services.Communication;

namespace Optix.Domain.Services
{
    public interface IGenreService
    {
        Task<ServiceResponse<IEnumerable<Movie>>> GetAllLinkedGenresAsync(IEnumerable<Movie> movies);
        Task<IEnumerable<Genre>> GetAllGenresForMovieAsync(long movieId);
    }
}
