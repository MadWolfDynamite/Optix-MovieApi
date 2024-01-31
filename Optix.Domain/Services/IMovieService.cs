using Optix.Domain.Models;

namespace Optix.Domain.Services
{
    public interface IMovieService
    {
        int Page { get; set; }
        int ItemLimit { get; set; }

        Task<IEnumerable<Movie>> ListAsync();
        Task<Movie> GetByIdAsync(long id);
        Task<IEnumerable<Movie>> GetByTitleAsync(string title);
        Task<IEnumerable<Movie>> GetByGenreAsync(string genre);
    }
}
