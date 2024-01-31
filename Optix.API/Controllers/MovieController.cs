using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Optix.Domain.Models;
using Optix.Domain.Services;

namespace Optix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService m_MovieService;
        private readonly IGenreService m_GenreService;

        public MovieController(IMovieService movieService, IGenreService genreService)
        {
            m_MovieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            m_GenreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(int page = 1, int itemsPerPage = 25)
        {
            m_MovieService.Page = page;
            m_MovieService.ItemLimit = itemsPerPage;

            var movies = await m_MovieService.ListAsync();

            return await m_GenreService.GetAllLinkedGenresAsync(movies);
        }
    }
}
