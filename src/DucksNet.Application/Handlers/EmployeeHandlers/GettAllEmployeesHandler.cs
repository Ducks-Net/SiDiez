using DucksNet.Application.Mappers;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Application.Requests.EmployeeRequests;
using MediatR;

namespace DucksNet.Application.Handlers.EmployeeHandlers;
public class GettAllEmployeesHandler : IRequestHandler<GetAllEmployeesRequest, List<EmployeeResponse>>
{
    private readonly IRepositoryAsync<Employee> _repository;

    public GettAllEmployeesHandler(IRepositoryAsync<Employee> repository)
    {
        _repository = repository;
    }

    public async Task<List<EmployeeResponse>> Handle(GetAllEmployeesRequest request, CancellationToken cancellationToken)
    {
        var result = EmployeeMapper.Mapper.Map<List<EmployeeResponse>>(await _repository.GetAllAsync());
        return result;
    }
}
