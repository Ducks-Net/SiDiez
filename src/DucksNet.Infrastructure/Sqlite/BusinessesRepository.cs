using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;
using DucksNet.Infrastructure.Prelude;

namespace DucksNet.Infrastructure.Sqlite;

public class BusinessesRepository : IRepository<Business>
{
    private readonly IDatabaseContext _databaseContext;

    public BusinessesRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    Result<Business> IRepository<Business>.Get(Guid id)
    {
        var business = _databaseContext.Businesses.Find(id);
        return business is null ? Result<Business>.Error("Business not found") : Result<Business>.Ok(business);
    }

    IEnumerable<Business> IRepository<Business>.GetAll()
    {
        return _databaseContext.Businesses;
    }

    Result IRepository<Business>.Add(Business entity)
    {
        _databaseContext.Businesses.Add(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<Business>.Update(Business entity)
    {
        _databaseContext.Businesses.Update(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<Business>.Delete(Business entity)
    {
        _databaseContext.Businesses.Remove(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }
}
