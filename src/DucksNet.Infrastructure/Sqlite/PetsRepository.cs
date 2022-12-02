using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;
using DucksNet.Infrastructure.Prelude;

namespace DucksNet.Infrastructure.Sqlite;

public class PetsRepository : IRepository<Pet>
{
    private readonly IDatabaseContext _databaseContext;

    public PetsRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    Result<Pet> IRepository<Pet>.Get(Guid id)
    {
        var pet = _databaseContext.Pets.Find(id);
        return pet is null ? Result<Pet>.Error("Pet not found") : Result<Pet>.Ok(pet);
    }

    IEnumerable<Pet> IRepository<Pet>.GetAll()
    {
        return _databaseContext.Pets;
    }

    Result IRepository<Pet>.Add(Pet entity)
    {
        _databaseContext.Pets.Add(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<Pet>.Update(Pet entity)
    {
        _databaseContext.Pets.Update(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<Pet>.Delete(Pet entity)
    {
        _databaseContext.Pets.Remove(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }
}
