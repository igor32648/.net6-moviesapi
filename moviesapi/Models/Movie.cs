using System.ComponentModel.DataAnnotations;

namespace moviesapi.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is missing.")]
        [MaxLength(100, ErrorMessage = "Title is too long.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description missing.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Director is missing.")]
        public string Director { get; set; }
        [Required(ErrorMessage = "Year is missing.")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Duration is missing.")]
        [Range(15, 500, ErrorMessage = "Should be between 15 to 500 minutes.")]
        public int Minute { get; set; }


    }
}
