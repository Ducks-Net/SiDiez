using DucksNet.Application.Requests.EmployeeRequests;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using MediatR;

namespace DucksNet.Application.Handlers.EmployeeHandlers;
public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeRequest, EmployeeResultResponse>
{
    private readonly IRepositoryAsync<Employee> _employeesRepository;
    private readonly IRepositoryAsync<Office> _officeRepository;

    public UpdateEmployeeHandler(IRepositoryAsync<Employee> _employeesRepository, IRepositoryAsync<Office> _officeRepository)
    {
        this._employeesRepository = _employeesRepository;
        this._officeRepository = _officeRepository;
    }
    public async Task<EmployeeResultResponse> Handle(UpdateEmployeeRequest request, CancellationToken cancellationToken)
    {
        var oldEmployee = await _employeesRepository.GetAsync(request.EmployeeId);

        if (oldEmployee.IsFailure)
        {
            return new EmployeeResultResponse(null, oldEmployee.Errors, ETypeRequests.BAD_REQUEST);
        }
        var employees = await _employeesRepository.GetAllAsync();
        foreach (var employee in employees)
        {
            if (employee.OwnerEmail == request.Value.OwnerEmail)
            {
                return new EmployeeResultResponse(null, new List<string> { "The updated email already exists" }, ETypeRequests.BAD_REQUEST);
            }
            if (employee.OwnerPhone == request.Value.OwnerPhone)
            {
                return new EmployeeResultResponse(null, new List<string> { "The updated telephone number already exists" }, ETypeRequests.BAD_REQUEST);
            }
        }

        await _employeesRepository.UpdateAsync(oldEmployee.Value!);
        return new EmployeeResultResponse(null, new List<string> { "The information has been updated" }, ETypeRequests.OK);
    }
}
