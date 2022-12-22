using DucksNet.API.DTO;
using DucksNet.Application.Responses;
using MediatR;

namespace DucksNet.Application.Requests.EmployeeRequests;
public class UpdateEmployeeRequest : IRequest<EmployeeResultResponse>
{
    public Guid EmployeeId { get; set;}
    public EmployeeDto Value { get; set; }
}
