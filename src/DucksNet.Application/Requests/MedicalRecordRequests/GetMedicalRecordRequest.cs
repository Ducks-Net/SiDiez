using DucksNet.Application.Responses;
using MediatR;

namespace DucksNet.Application.Requests.MedicalRecordRequests;
public class GetMedicalRecordRequest : IRequest<MedicalRecordResultResponse>
{
    public Guid MedicalRecordId { get; set; }
}
