using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Optix.Domain.Models;
using Optix.Domain.Services;
using Optix.Domain.Services.Communication;

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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int page = 1, int itemsPerPage = 25)
        {
            SetPagingConfiguration(page, itemsPerPage);

            var searchResponse = await m_MovieService.ListAsync();

            if (!searchResponse.IsSuccess)
                return BadRequest(searchResponse.Message);

            var result = await m_GenreService.GetAllLinkedGenresAsync(searchResponse.Data);

            return result.IsSuccess
                ? Ok(new { searchResponse.Count, Items = result.Data })
                : BadRequest(result.Message);
        }

        [Route("Search/Title")]
        [HttpGet]
        public async Task<IActionResult> GetByTitle(string query, int page = 1, int itemsPerPage = 25)
        {
            SetPagingConfiguration(page, itemsPerPage);

            var searchResponse = await m_MovieService.GetByTitleAsync(query);

            if (!searchResponse.IsSuccess)
                return BadRequest(searchResponse.Message);

            var result = await m_GenreService.GetAllLinkedGenresAsync(searchResponse.Data);

            return result.IsSuccess
                ? Ok(new { searchResponse.Count, Items = result.Data })
                : BadRequest(result.Message);
        }

        private void SetPagingConfiguration(int page, int itemsPerPage)
        {
            m_MovieService.Page = page;
            m_MovieService.ItemLimit = itemsPerPage;
        }
    }
}
