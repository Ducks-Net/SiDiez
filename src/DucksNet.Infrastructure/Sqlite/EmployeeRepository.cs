using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Infrastructure.Sqlite;
public class EmployeeRepository : IRepository<Employee>
{
    private readonly IDatabaseContext _databaseContext;

    public EmployeeRepository(IDatabaseContext employeeRepository)
    {
        _databaseContext = employeeRepository;
    }
    public Result Add(Employee entity)
    {
        _databaseContext.Employees.Add(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    public Result Delete(Employee entity)
    {
        _databaseContext.Employees.Remove(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    public Result<Employee> Get(Guid id)
    {
        var employee = _databaseContext.Employees.Find(id); //TODO (RO): nu aduce si relatia, inainte de punct; nu mapeaza pe officeId -> incarci relatia + obiectul respectiv
        return employee is null ? Result<Employee>.Error("Employee not found") : Result<Employee>.Ok(employee);
    }

    public IEnumerable<Employee> GetAll()
    {
        return _databaseContext.Employees;
    }

    public Result Update(Employee entity)
    {
        _databaseContext.Employees.Update(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }
}
