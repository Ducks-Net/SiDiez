using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;
using DucksNet.Infrastructure.Prelude;

namespace DucksNet.Infrastructure.Sqlite;

public class MedicinesRepository : IRepository<Medicine>
{
    private readonly IDatabaseContext _databaseContext;

    public MedicinesRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    Result<Medicine> IRepository<Medicine>.Get(Guid id)
    {
        var medicine = _databaseContext.Medicines.Find(id);
        return medicine is null ? Result<Medicine>.Error("Medicine not found") : Result<Medicine>.Ok(medicine);
    }

    IEnumerable<Medicine> IRepository<Medicine>.GetAll()
    {
        return _databaseContext.Medicines;
    }

    Result IRepository<Medicine>.Add(Medicine entity)
    {
        _databaseContext.Medicines.Add(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<Medicine>.Update(Medicine entity)
    {
        _databaseContext.Medicines.Update(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<Medicine>.Delete(Medicine entity)
    {
        _databaseContext.Medicines.Remove(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }
}
