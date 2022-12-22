using DucksNet.Application.Mappers;
using DucksNet.Application.Requests.EmployeeRequests;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using MediatR;

namespace DucksNet.Application.Handlers.EmployeeHandlers;
public class GetEmployeeHandler : IRequestHandler<GetEmployeeRequest, EmployeeResultResponse>
{
    private readonly IRepositoryAsync<Employee> _repository;
    public GetEmployeeHandler(IRepositoryAsync<Employee> repository)
    {
        _repository = repository;
    }

    public async Task<EmployeeResultResponse> Handle(GetEmployeeRequest request, CancellationToken cancellationToken)
    {
        var employee = await _repository.GetAsync(request.EmployeeId);
        EmployeeResultResponse response;
        if (employee.IsFailure)
        {
            response = new EmployeeResultResponse(employee.Value, employee.Errors, ETypeRequests.NOT_FOUND);
            return response;
        }
        response = new EmployeeResultResponse(employee.Value, null, ETypeRequests.OK);
        return response;
    }
}
