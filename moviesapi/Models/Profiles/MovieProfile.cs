using AutoMapper;
using moviesapi.Data.Dto;


namespace moviesapi.Models.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<CreateMovieDto, Movie>();
        }
    }
}
