using Microsoft.AspNetCore.Mvc;
using moviesapi.Models;

namespace moviesapi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private static List<Movie> movies = new List<Movie>();
        private static int id = 0;

        [HttpPost]
        public IActionResult AddMovie([FromBody]Movie movie)
        {
            movie.Id = id++;
            movies.Add(movie);
            return CreatedAtAction(nameof(GetMovieById),
           new { id = movie.Id },
           movie);
        }

        [HttpGet]
        public IEnumerable<Movie> GetMovies() 
        { 
            return movies; 
        }
        [HttpGet("{id}")]
        public Movie? GetMovieById(int id) 
        {
            return movies.FirstOrDefault(movie => movie.Id == id);
        }
    }
}
