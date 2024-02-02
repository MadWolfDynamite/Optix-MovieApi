using Microsoft.AspNetCore.Mvc;
using Optix.API.DTOs;
using Optix.Domain.Models;
using Optix.Domain.Services;

namespace Optix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService m_MovieService;

        public MovieController(IMovieService movieService)
        {
            m_MovieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PagingSortingParameters parameters)
        {
            SetPagingConfiguration(parameters.Page, parameters.ItemsPerPage);
            SetSortingConfiguration(parameters.Sort, parameters.Order);

            var searchResponse = await m_MovieService.ListAsync();

            return searchResponse.IsSuccess
                ? Ok(new MovieSearchResult<IEnumerable<Movie>>(searchResponse.Count, searchResponse.Data))
                : BadRequest(searchResponse.Message);
        }

        [Route("Search/Title")]
        [HttpGet]
        public async Task<IActionResult> GetByTitle(string query, [FromQuery] PagingSortingParameters parameters)
        {
            SetPagingConfiguration(parameters.Page, parameters.ItemsPerPage);
            SetSortingConfiguration(parameters.Sort, parameters.Order);

            var searchResponse = await m_MovieService.GetByTitleAsync(query);

            return searchResponse.IsSuccess
                ? Ok(new MovieSearchResult<IEnumerable<Movie>>(searchResponse.Count, searchResponse.Data))
                : BadRequest(searchResponse.Message);
        }

        [Route("Search/Genre")]
        [HttpGet]
        public async Task<IActionResult> GetByGenre(string query, [FromQuery] PagingSortingParameters parameters)
        {
            SetPagingConfiguration(parameters.Page, parameters.ItemsPerPage);
            SetSortingConfiguration(parameters.Sort, parameters.Order);

            var searchResponse = await m_MovieService.GetByGenreAsync(query);

            return searchResponse.IsSuccess
                ? Ok(new MovieSearchResult<IEnumerable<Movie>>(searchResponse.Count, searchResponse.Data))
                : BadRequest(searchResponse.Message);
        }

        private void SetPagingConfiguration(int page, int itemsPerPage)
        {
            m_MovieService.Page = page;
            m_MovieService.ItemLimit = itemsPerPage;
        }

        private void SetSortingConfiguration(string sort, string order)
        {
            m_MovieService.SortBy = sort ?? string.Empty;
            m_MovieService.SortOrder = order;
        }
    }
}
