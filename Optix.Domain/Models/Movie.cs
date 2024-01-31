using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Optix.Domain.Models
{
    public class Movie
    {
        [Ignore]
        public long Id { get; set; }

        public string Title { get; set; }
        public string Overview { get; set; }
        [Name("Release_Date")]
        public DateTime ReleaseDate { get; set; }

        public decimal Popularity { get; set; }
        [Name("Vote_Count")]
        public decimal VoteCount { get; set; }
        [Name("Vote_Average")]
        public decimal VoteAverage { get; set; }

        [Name("Original_Language")]
        public string OriginalLanguage { get; set; }

        [Name("Poster_Url")]
        public string PosterUrl { get; set; }

        [Ignore]
        public virtual IList<Genre> Genres { get; set; }
    }
}