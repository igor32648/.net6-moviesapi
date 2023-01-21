using Microsoft.EntityFrameworkCore;
using moviesapi.Models;

namespace moviesapi.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
    }
}
