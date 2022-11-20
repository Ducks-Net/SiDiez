using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;
using DucksNet.Infrastructure.Prelude;

namespace DucksNet.Infrastructure.Sqlite;

public class CagesRepository : IRepository<Cage>
{
    private readonly IDatabaseContext _databaseContext;

    public CagesRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    Result<Cage> IRepository<Cage>.Get(Guid id)
    {
        var cage = _databaseContext.Cages.Find(id);
        return cage is null ? Result<Cage>.Error("Cage not found") : Result<Cage>.Ok(cage);
    }

    IEnumerable<Cage> IRepository<Cage>.GetAll()
    {
        return _databaseContext.Cages;
    }

    Result IRepository<Cage>.Add(Cage entity)
    {
        _databaseContext.Cages.Add(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<Cage>.Update(Cage entity)
    {
        _databaseContext.Cages.Update(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<Cage>.Delete(Cage entity)
    {
        _databaseContext.Cages.Remove(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }
}
