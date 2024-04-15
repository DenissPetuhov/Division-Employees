using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Application.Mapping
{
    public class EmployeeMapping : Profile
    {
        protected EmployeeMapping()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
