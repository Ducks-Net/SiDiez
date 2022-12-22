using DucksNet.Application.Responses;
using MediatR;

namespace DucksNet.Application.Requests.EmployeeRequests;
public class GetAllEmployeesRequest : IRequest<List<EmployeeResponse>>{}
