using DucksNet.Application.Requests.EmployeeRequests;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using MediatR;

namespace DucksNet.Application.Handlers.EmployeeHandlers;

public class UpdateOfficeEmployeeHandler : IRequestHandler<UpdateEmployeeOfficeRequest, EmployeeResultResponse>
{
    private readonly IRepositoryAsync<Employee> _employeesRepository;
    private readonly IRepositoryAsync<Office> _officeRepository;

    public UpdateOfficeEmployeeHandler(IRepositoryAsync<Employee> _employeesRepository, IRepositoryAsync<Office> _officeRepository)
    {
        this._employeesRepository = _employeesRepository;
        this._officeRepository = _officeRepository;
    }

    public async Task<EmployeeResultResponse> Handle(UpdateEmployeeOfficeRequest request, CancellationToken cancellationToken)
    {
        var oldEmployee = await _employeesRepository.GetAsync(request.EmployeeId);
        if (oldEmployee.IsFailure)
        {
            return new EmployeeResultResponse(null, oldEmployee.Errors, ETypeRequests.BAD_REQUEST);
        }
        var newOffice = await _officeRepository.GetAsync(request.OfficeId);
        if (newOffice.IsFailure)
        {
            return new EmployeeResultResponse(null, newOffice.Errors, ETypeRequests.BAD_REQUEST);
        }
        oldEmployee.Value!.AssignToOffice(request.OfficeId);
        await _employeesRepository.UpdateAsync(oldEmployee.Value);
        return new EmployeeResultResponse(oldEmployee.Value, null, ETypeRequests.OK);
    }
}
