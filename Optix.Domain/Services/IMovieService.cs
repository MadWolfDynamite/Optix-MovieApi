using Optix.Domain.Models;
using Optix.Domain.Services.Communication;

namespace Optix.Domain.Services
{
    public interface IMovieService
    {
        int Page { get; set; }
        int ItemLimit { get; set; }

        Task<SearchResponse<IEnumerable<Movie>>> ListAsync();
        Task<Movie> GetByIdAsync(long id);
        Task<SearchResponse<IEnumerable<Movie>>> GetByTitleAsync(string title);
        Task<SearchResponse<IEnumerable<Movie>>> GetByGenreAsync(string genre);
    }
}
