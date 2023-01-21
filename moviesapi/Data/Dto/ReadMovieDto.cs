namespace moviesapi.Data.Dto
{
    public class ReadMovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public int Minute { get; set; }
        public DateTime SearchTime { get; set; } = DateTime.Now;
    }
}
