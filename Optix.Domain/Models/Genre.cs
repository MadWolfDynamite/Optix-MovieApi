using System.ComponentModel.DataAnnotations.Schema;

namespace Optix.Domain.Models
{
    public class Genre
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public Movie Movie { get; set; }
    }
}
