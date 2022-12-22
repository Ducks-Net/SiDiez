using System.ComponentModel.DataAnnotations;
using DucksNet.API.DTO;
using DucksNet.Application.Mappers;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using FluentValidation;
using MediatR;

namespace DucksNet.Application.Handlers.EmployeeHandlers;
public class CreateEmployeeHandler : IRequestHandler<EmployeeDto, EmployeeResultResponse>
{
    private readonly IRepositoryAsync<Employee> _employeesRepository;
    private readonly IRepositoryAsync<Office> _officeRepository;

    public CreateEmployeeHandler(IRepositoryAsync<Employee> _employeesRepository, IRepositoryAsync<Office> _officeRepository)
    {
        this._employeesRepository = _employeesRepository;
        this._officeRepository = _officeRepository;
    }

    public async Task<EmployeeResultResponse> Handle(EmployeeDto request, CancellationToken cancellationToken)
    {
        var office = await _officeRepository.GetAsync(request.IdOffice);
        if (office.IsFailure)
        {
            return new EmployeeResultResponse(null, office.Errors, ETypeRequests.BAD_REQUEST);
        }
        var employeePost = EmployeeMapper.Mapper.Map<Employee>(request);
        employeePost.AssignToOffice(office.Value!.ID);
        var employees = await _employeesRepository.GetAllAsync();
        foreach (var employee in employees)
        {
            if (employee.OwnerEmail == request.OwnerEmail)
            {
                return new EmployeeResultResponse(null, new List<string> { "The email already exists" }, ETypeRequests.BAD_REQUEST);
            }
            if (employee.OwnerPhone == request.OwnerPhone)
            {
                return new EmployeeResultResponse(null, new List<string> { "The telephone number already exists" }, ETypeRequests.BAD_REQUEST);
            }
        }
        await _employeesRepository.AddAsync(employeePost);
        return new EmployeeResultResponse(employeePost, null, ETypeRequests.OK);
    }
}
