using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using moviesapi.Data;
using moviesapi.Data.Dto;
using moviesapi.Models;

namespace moviesapi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {

        private MovieContext _context;
        private IMapper _mapper;

        public MovieController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Add a movie to the database
        /// </summary>
        /// <param name="movieDto">Object with the required fields to create a movie</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">If insertion is successful</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddMovie(
            [FromBody] CreateMovieDto movieDto)
        {
            Movie movie = _mapper.Map<Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMovieById),
            new { id = movie.Id },
            movie);
        }

        /// <summary>
        /// Get movies from database
        /// </summary>
        /// <param name="skip">How many movies to skip</param>
        ///<param name="take">How many movies to take</param>
        /// <returns>IEnumerable</returns>
        /// <response code="200">If get method is successful</response>
        [HttpGet]
        public IEnumerable<ReadMovieDto> GetMovies([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return _mapper.Map<List<ReadMovieDto>>(_context.Movies.Skip(skip).Take(take));
        }

        /// <summary>
        /// Add a movie according to id
        /// </summary>
        /// <param name="id">Movie Id number</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">If get method is successful</response>
        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (movie == null) return NotFound();
            var movieDto = _mapper.Map<ReadMovieDto>(movie);
            return Ok(movieDto);
        }

        /// <summary>
        /// Update a movie's data from database
        /// </summary>
        /// <param name="id">Movie Id number</param>
        /// <param name="updateMovieDto">Object with movie fields updated</param>
        /// <returns>IActionResult</returns>
        /// <response code="202">If update is successful</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDto updateMovieDto)
        {
            var movie = _context.Movies.FirstOrDefault(
                movie => movie.Id == id);
            if (movie == null) return NotFound();
            _mapper.Map(updateMovieDto, movie);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Update a movie from the database without having to resubmit all the data
        /// </summary>
        /// <param name="id">Movie Id number</param>
        /// <param name="patch">Object with movie required info and field to be updated</param>
        /// <returns>IActionResult</returns>
        /// <response code="202">If update is successful</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult UpdateMoviePatch(int id, 
            JsonPatchDocument<UpdateMovieDto> patch)
        {
            var movie = _context.Movies.FirstOrDefault(
                movie => movie.Id == id);
            if (movie == null) return NotFound();

            var movieToUpdate = _mapper.Map<UpdateMovieDto>(movie);

            patch.ApplyTo(movieToUpdate, ModelState);

            if (!TryValidateModel(movieToUpdate))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(movieToUpdate, movie);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Delete a movie's data from database
        /// </summary>
        /// <param name="id">Movie Id number</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">If delete is successful</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteMovie(int id) 
        {
            var movie = _context.Movies.FirstOrDefault(
                movie => movie.Id == id);
            if (movie == null) return NotFound();
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
