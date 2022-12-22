using DucksNet.Application.Responses;
using MediatR;

namespace DucksNet.Application.Requests.EmployeeRequests;
public class UpdateEmployeeOfficeRequest : IRequest<EmployeeResultResponse>
{
    public Guid EmployeeId { get; set;}
    public Guid OfficeId { get; set; }
}
