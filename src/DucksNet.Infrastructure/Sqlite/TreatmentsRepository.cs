//using DucksNet.Domain.Model;
//using DucksNet.SharedKernel.Utils;
//using DucksNet.Infrastructure.Prelude;

//namespace DucksNet.Infrastructure.Sqlite;

//public class TreatmentsRepository : IRepository<Treatment>
//{
//    private readonly IDatabaseContext _databaseContext;

//    public TreatmentsRepository(IDatabaseContext databaseContext)
//    {
//        _databaseContext = databaseContext;
//    }

//    Result<Treatment> IRepository<Treatment>.Get(Guid id)
//    {
//        var treatment = _databaseContext.Treatments.Find(id);
//        return treatment is null ? Result<Treatment>.Error("Treatment not found") : Result<Treatment>.Ok(treatment);
//    }

//    IEnumerable<Treatment> IRepository<Treatment>.GetAll()
//    {
//        return _databaseContext.Treatments;
//    }

//    Result IRepository<Treatment>.Add(Treatment entity)
//    {
//        _databaseContext.Treatments.Add(entity);
//        _databaseContext.SaveChanges();
//        return Result.Ok();
//    }

//    Result IRepository<Treatment>.Update(Treatment entity)
//    {
//        _databaseContext.Treatments.Update(entity);
//        _databaseContext.SaveChanges();
//        return Result.Ok();
//    }

//    Result IRepository<Treatment>.Delete(Treatment entity)
//    {
//        _databaseContext.Treatments.Remove(entity);
//        _databaseContext.SaveChanges();
//        return Result.Ok();
//    }
//}
