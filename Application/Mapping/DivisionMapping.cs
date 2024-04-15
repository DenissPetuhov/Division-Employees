using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Application.Mapping
{
    public class DivisionMapping : Profile
    {
        protected DivisionMapping()
        {
            CreateMap<Division, DivisionDto>().ReverseMap();
        }
    }
}
