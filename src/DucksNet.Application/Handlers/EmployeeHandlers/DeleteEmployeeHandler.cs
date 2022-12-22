using DucksNet.Application.Requests.EmployeeRequests;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using MediatR;

namespace DucksNet.Application.Handlers.EmployeeHandlers;

public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeRequest, EmployeeResultResponse>
{
    private readonly IRepositoryAsync<Employee> _repository;

    public DeleteEmployeeHandler(IRepositoryAsync<Employee> repository)
    {
        _repository = repository;
    }
    public async Task<EmployeeResultResponse> Handle(DeleteEmployeeRequest request, CancellationToken cancellationToken)
    {
        var employee = await _repository.GetAsync(request.EmployeeId);
        if (employee.IsFailure)
        {
            return new EmployeeResultResponse(null, employee.Errors, ETypeRequests.BAD_REQUEST);
        }
        await _repository.DeleteAsync(employee.Value!);
        return new EmployeeResultResponse(null, null, ETypeRequests.OK);
    }
}
