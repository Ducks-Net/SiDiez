using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.SharedKernel.Utils;
using SamuraiApp.Infrastructure.Sqlite;

namespace DucksNet.Infrastructure.Sqlite;
public class EmployeeRepository : IRepository<Employee>
{
    private readonly IDatabaseContext _employeeRepository;

    public EmployeeRepository(IDatabaseContext employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public Result Add(Employee entity)
    {
        _employeeRepository.Employees.Add(entity);
        _employeeRepository.SaveChanges();
        return Result.Ok();
    }

    public Result Delete(Employee entity)
    {
        _employeeRepository.Employees.Remove(entity);
        _employeeRepository.SaveChanges();
        return Result.Ok();
    }

    public Result<Employee> Get(Guid id)
    {
        var employee = _employeeRepository.Employees.Find(id);
        return employee is null ? Result<Employee>.Error("Employee not found") : Result<Employee>.Ok(employee);
    }

    public IEnumerable<Employee> GetAll()
    {
        return _employeeRepository.Employees;
    }

    public Result Update(Employee entity)
    {
        _employeeRepository.Employees.Update(entity);
        _employeeRepository.SaveChanges();
        return Result.Ok();
    }
}
