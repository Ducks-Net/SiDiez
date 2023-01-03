using DucksNet.Application.Responses;
using MediatR;

namespace DucksNet.Application.Requests.MedicalRecordRequests;
public class UpdateMedicalRecordAppointmentRequest : IRequest<MedicalRecordResultResponse>
{
    public Guid MedicalRecordId { get; set; }
    public Guid NewAppointmentId { get; set; }
}
