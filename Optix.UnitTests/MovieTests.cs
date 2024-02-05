using Microsoft.EntityFrameworkCore;
using Moq;
using Optix.API.Persistence.Repositories;
using Optix.API.Services;
using Optix.Domain.Models;
using Optix.Repository.Contexts;

namespace Optix.UnitTests
{
    public class MovieTests
    {
        private MovieRepository m_Repository;

        private string m_DbName;
        private MovieDbContext m_DbContext;

        [SetUp]
        public void Setup()
        {
            var genreData = new List<Genre>
            { 
                new Genre { Id = 1, Name = "Action", MovieId = 1 },
                new Genre { Id = 2, Name = "Adventure", MovieId = 1 },
                new Genre { Id = 3, Name = "Family", MovieId = 1 },

                new Genre { Id = 4, Name = "Crime", MovieId = 2 },

                new Genre { Id = 5, Name = "Thriller", MovieId = 3 },
                new Genre { Id = 6, Name = "Family", MovieId = 3 },
            }.AsQueryable();

            var movieData = new List<Movie> 
            {
                new Movie
                {
                    Id = 1,
                    Title = "Movie1",
                    Overview = $"{Guid.NewGuid()}",
                    ReleaseDate = new DateTime (2002, 2, 13),
                    Popularity = 707.78M,
                    VoteCount = 9584,
                    VoteAverage = 7.8M,
                    OriginalLanguage = "en",
                    PosterUrl = "https://image.tmdb.org/t/p/original/muIaHotSaSUQr0KZCIJOYQEe7y2.jpg",
                    Genres = new List<Genre> 
                    {
                        genreData.ElementAt(0),
                        genreData.ElementAt(1),
                        genreData.ElementAt(2),
                    }
                },
                new Movie
                {
                    Id = 2,
                    Title = "Movie2",
                    Overview = $"{Guid.NewGuid()}",
                    ReleaseDate = new DateTime (2022, 5, 21),
                    Popularity = 5096.21M,
                    VoteCount = 775,
                    VoteAverage = 4.9M,
                    OriginalLanguage = "en",
                    PosterUrl = "https://image.tmdb.org/t/p/original/muIaHotSaSUQr0KZCIJOYQEe7y2.jpg",
                    Genres = new List<Genre> 
                    {
                       genreData.ElementAt(3),
                    }
                },
                new Movie
                {
                    Id = 3,
                    Title = "Movie3",
                    Overview = $"{Guid.NewGuid()}",
                    ReleaseDate = new DateTime (1999, 12, 24),
                    Popularity = 1027.96M,
                    VoteCount = 6752,
                    VoteAverage = 9.1M,
                    OriginalLanguage = "ja",
                    PosterUrl = "https://image.tmdb.org/t/p/original/muIaHotSaSUQr0KZCIJOYQEe7y2.jpg",
                    Genres = new List<Genre> 
                    {
                        genreData.ElementAt(4),
                        genreData.ElementAt(5),
                    }
                },
            }.AsQueryable();

            var movieMockSet = new Mock<DbSet<Movie>>();
            movieMockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(movieData.Provider);
            movieMockSet.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(movieData.Expression);
            movieMockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(movieData.ElementType);
            movieMockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(movieData.GetEnumerator());

            var genreMockSet = new Mock<DbSet<Genre>>();
            genreMockSet.As<IQueryable<Genre>>().Setup(m => m.Provider).Returns(genreData.Provider);
            genreMockSet.As<IQueryable<Genre>>().Setup(m => m.Expression).Returns(genreData.Expression);
            genreMockSet.As<IQueryable<Genre>>().Setup(m => m.ElementType).Returns(genreData.ElementType);
            genreMockSet.As<IQueryable<Genre>>().Setup(m => m.GetEnumerator()).Returns(genreData.GetEnumerator());

            m_DbName = $"mymoviedb_{DateTime.Now.ToFileTimeUtc}";
            var contextOptions = new DbContextOptionsBuilder<MovieDbContext>().UseInMemoryDatabase(m_DbName);

            m_DbContext = new MovieDbContext(contextOptions.Options);

            m_DbContext.AddRange(movieMockSet.Object);
            m_DbContext.SaveChanges();

            m_DbContext.Database.EnsureCreated();
            m_Repository = new MovieRepository(m_DbContext);
        }

        [Test]
        public async Task GetAllMovies()
        {
            var service = new MovieService(m_Repository);
            var movies = await service.ListAsync();

            Assert.Multiple(() =>
            {
                Assert.That(movies.Data.Any(m => m.Title.Equals("Movie1")));
                Assert.That(movies.Data.Any(m => m.Title.Equals("Movie2")));
                Assert.That(movies.Data.Any(m => m.Title.Equals("Movie3")));
            });
        }

        [Test]
        public async Task GetMovieById()
        {
            var service = new MovieService(m_Repository);
            var movie = await service.GetByIdAsync(2);

            Assert.That(movie.Title, Is.EqualTo("Movie2"));
        }

        [Test]
        public async Task GetMovieByTitle()
        {
            var service = new MovieService(m_Repository);
            var movies = await service.GetByTitleAsync("Movie1");

            Assert.That(movies.Data.Any(m => m.Id == 1));
        }

        [Test]
        public async Task GetMovieByGenre()
        {
            var service = new MovieService(m_Repository);
            var movies = await service.GetByGenreAsync("Family");

            Assert.Multiple(() => 
            {
                Assert.That(movies.Data.Any(m => m.Title.Equals("Movie1")));
                Assert.That(movies.Data.Any(m => m.Title.Equals("Movie3")));
            });
        }

        [TearDown]
        public void TeardownTestData()
        {
            m_DbContext.Database.EnsureDeleted();
        }
    }
}