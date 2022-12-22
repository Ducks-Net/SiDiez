using AutoMapper;
using DucksNet.API.DTO;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;

namespace DucksNet.Application.Mappers;
public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    {
        CreateMap<Employee, EmployeeResponse>().ReverseMap();
        CreateMap<Employee, EmployeeResultResponse>().ReverseMap();
        CreateMap<Employee, EmployeeDto>().ReverseMap();
    }
}
