using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;
using DucksNet.Infrastructure.Prelude;

namespace DucksNet.Infrastructure.Sqlite;

public class CageTimeBlocksRepository : IRepository<CageTimeBlock>
{
    private readonly IDatabaseContext _databaseContext;

    public CageTimeBlocksRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    Result<CageTimeBlock> IRepository<CageTimeBlock>.Get(Guid id)
    {
        var cageTimeBlock = _databaseContext.CageTimeBlocks.Find(id);
        return cageTimeBlock is null ? Result<CageTimeBlock>.Error("CageTimeBlock not found") : Result<CageTimeBlock>.Ok(cageTimeBlock);
    }

    IEnumerable<CageTimeBlock> IRepository<CageTimeBlock>.GetAll()
    {
        return _databaseContext.CageTimeBlocks;
    }

    Result IRepository<CageTimeBlock>.Add(CageTimeBlock entity)
    {
        _databaseContext.CageTimeBlocks.Add(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<CageTimeBlock>.Update(CageTimeBlock entity)
    {
        _databaseContext.CageTimeBlocks.Update(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<CageTimeBlock>.Delete(CageTimeBlock entity)
    {
        _databaseContext.CageTimeBlocks.Remove(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }
}
