using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Application.Mapping
{
    public class DivisionMapping : Profile
    {
        public DivisionMapping()
        {
            CreateMap<Division, DivisionDto>().ReverseMap();
            CreateMap<Division, CreateDivisionDto>().ReverseMap();
            CreateMap<Division, AddParentDivisionDto>().ReverseMap();
        }
    }
}
