using DucksNet.Application.Responses;
using MediatR;

namespace DucksNet.Application.Requests.MedicalRecordRequests;
public class UpdateMedicalRecordClientRequest : IRequest<MedicalRecordResultResponse>
{
    public Guid MedicalRecordId { get; set; }
    public Guid NewClientId { get; set; }
}
