using AutoMapper;
using Domain.Core.DTOs;
using Entities;

namespace Domain.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CatalogRequest, Catalog>();
        }
    }
}
