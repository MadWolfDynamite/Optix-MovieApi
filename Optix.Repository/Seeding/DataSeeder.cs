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

            using StreamReader reader = new(resourceName, Encoding.UTF8);
            using CsvReader csvReader = new(reader, CultureInfo.InvariantCulture);

            while (csvReader.Read())
            {
                var movie = csvReader.GetRecord<Movie>();

                var genres = csvReader.GetField<string>("Genre").Split(',').Select(g => new Genre { Name = g.Trim(), MovieId = movie.Id }).ToList();
                movie.Genres = genres;

                context.Add(movie);
            }

            context.SaveChanges();
        }
    }
}
