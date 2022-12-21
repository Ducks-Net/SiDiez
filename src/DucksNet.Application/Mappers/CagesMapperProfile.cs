using AutoMapper;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.API.Mappers;

public class CageMappingProfile : Profile
{
    public CageMappingProfile()
    {
        CreateMap<CreateCageDto, Result<Cage>>()
        .ConstructUsing(dto => Cage.Create(dto.SizeString));
    }
}
