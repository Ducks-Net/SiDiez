using DucksNet.Application.Responses;
using MediatR;

namespace DucksNet.API.DTO;

public class MedicalRecordDto : IRequest<MedicalRecordResultResponse>
{
    public MedicalRecordDto(Guid idAppointment, Guid idClient)
    {
        IdAppointment = idAppointment;
        IdClient = idClient;
    }

    public Guid IdAppointment { get; set; }
    public Guid IdClient { get; set; }
}
