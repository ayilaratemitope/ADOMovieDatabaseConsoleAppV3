using System.Collections.Generic;

namespace ADOMovieDatabaseConsoleAppV3.Models
{
    internal class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}