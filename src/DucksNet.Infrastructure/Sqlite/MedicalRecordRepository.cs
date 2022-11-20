using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Infrastructure.Sqlite;

public class MedicalRecordRepository : IRepository<MedicalRecord>
{
    private readonly IDatabaseContext databaseContext;

    public MedicalRecordRepository(IDatabaseContext databaseContext)
    {
        this.databaseContext = databaseContext;
    }
    public Result Add(MedicalRecord entity)
    {
        databaseContext.MedicalRecords.Add(entity);
        databaseContext.SaveChanges();
        return Result.Ok();
    }

    public Result Delete(MedicalRecord entity)
    {
        databaseContext.MedicalRecords.Remove(entity);
        databaseContext.SaveChanges();
        return Result.Ok();
    }

    public Result<MedicalRecord> Get(Guid id)
    {
        var medicalRecord = databaseContext.MedicalRecords.Find(id);
        return medicalRecord is null ? Result<MedicalRecord>.Error("Medical Record not found") : Result<MedicalRecord>.Ok(medicalRecord);
    }

    public IEnumerable<MedicalRecord> GetAll()
    {
        return databaseContext.MedicalRecords;
    }

    public Result Update(MedicalRecord entity)
    {
        databaseContext.MedicalRecords.Update(entity);
        databaseContext.SaveChanges();
        return Result.Ok();
    }
}
