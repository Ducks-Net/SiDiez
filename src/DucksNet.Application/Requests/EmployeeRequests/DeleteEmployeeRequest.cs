using DucksNet.Application.Responses;
using MediatR;

namespace DucksNet.Application.Requests.EmployeeRequests;
public class DeleteEmployeeRequest : IRequest<EmployeeResultResponse>
{
    public Guid EmployeeId { get; set; }
}
