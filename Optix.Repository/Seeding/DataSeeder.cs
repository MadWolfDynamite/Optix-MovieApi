using CsvHelper;
using Optix.Domain.Models;
using Optix.Repository.Contexts;
using System.Globalization;
using System.Text;

namespace Optix.Repository.Seeding
{
    public class DataSeeder
    {
        public static void Seed(MovieDbContext context) {
            if (context.Movies.Any())
                return;

            var resourceName = @$"..\Optix.{nameof(Domain)}\SeedData\mymoviedb.csv";

            using var reader = new StreamReader(resourceName, Encoding.UTF8);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            while (csvReader.Read())
            {
                var movie = csvReader.GetRecord<Movie>();

                var genres = csvReader.GetField<string>("Genre").Split(',').Select(g => new Genre { Name = g, Movie = movie }).ToList();
                movie.Genres = genres;

                context.Add(movie);
            }

            context.SaveChanges();
        }
    }
}
