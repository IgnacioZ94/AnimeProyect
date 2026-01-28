using AutoMapper;
using Domain.Core.DTOs;
using Infrastructure.Models.Jikan;

namespace Infrastructure.Mappings
{
    public class JikanMappingProfile : Profile
    {
        public JikanMappingProfile()
        {
            CreateMap<JikanAnimeItem, AnimeInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MalId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.Synopsis))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Images != null ? src.Images.Jpg != null ? src.Images.Jpg.ImageUrl : null : null));

            CreateMap<JikanAnimeItem, AnimeInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MalId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.Synopsis))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score))
                .ForMember(dest => dest.TrailerUrl, opt => opt.MapFrom(src => src.Trailer != null ? src.Trailer.Url : null))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Images != null ? src.Images.Jpg != null ? src.Images.Jpg.ImageUrl : null : null));
        }
    }
}
