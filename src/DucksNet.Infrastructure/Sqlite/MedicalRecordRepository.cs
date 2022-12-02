using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Infrastructure.Sqlite;

public class MedicalRecordRepository : IRepository<MedicalRecord>
{
    private readonly IDatabaseContext _databaseContext;

    public MedicalRecordRepository(IDatabaseContext databaseContext)
    {
        this._databaseContext = databaseContext;
    }
    public Result Add(MedicalRecord entity)
    {
        _databaseContext.MedicalRecords.Add(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    public Result Delete(MedicalRecord entity)
    {
        _databaseContext.MedicalRecords.Remove(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    public Result<MedicalRecord> Get(Guid id)
    {
        var medicalRecord = _databaseContext.MedicalRecords.Find(id);
        return medicalRecord is null ? Result<MedicalRecord>.Error("Medical Record not found") : Result<MedicalRecord>.Ok(medicalRecord);
    }

    public IEnumerable<MedicalRecord> GetAll()
    {
        return _databaseContext.MedicalRecords;
    }

    public Result Update(MedicalRecord entity)
    {
        _databaseContext.MedicalRecords.Update(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }
}
