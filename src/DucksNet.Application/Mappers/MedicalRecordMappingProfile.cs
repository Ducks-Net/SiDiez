using AutoMapper;
using DucksNet.API.DTO;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;

namespace DucksNet.Application.Mappers;
public class MedicalRecordMappingProfile : Profile
{
    public MedicalRecordMappingProfile()
    {
        CreateMap<MedicalRecord, MedicalRecordResponse>().ReverseMap();
        CreateMap<MedicalRecord, MedicalRecordResultResponse>().ReverseMap();
        CreateMap<MedicalRecord, MedicalRecordDto>().ReverseMap();
    }
}
