using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;
using DucksNet.Infrastructure.Prelude;

namespace DucksNet.Infrastructure.Sqlite;

public class OfficesRepository : IRepository<Office>
{
    private readonly IDatabaseContext _databaseContext;

    public OfficesRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    Result<Office> IRepository<Office>.Get(Guid id)
    {
        var office = _databaseContext.Offices.Find(id);
        return office is null ? Result<Office>.Error("Office not found") : Result<Office>.Ok(office);
    }

    IEnumerable<Office> IRepository<Office>.GetAll()
    {
        return _databaseContext.Offices;
    }

    Result IRepository<Office>.Add(Office entity)
    {
        _databaseContext.Offices.Add(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<Office>.Update(Office entity)
    {
        _databaseContext.Offices.Update(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<Office>.Delete(Office entity)
    {
        _databaseContext.Offices.Remove(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }
}
