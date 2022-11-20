using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;
using DucksNet.Infrastructure.Prelude;

namespace DucksNet.Infrastructure.Sqlite;

public class AppointmentsRepository : IRepository<Appointment>
{
    private readonly IDatabaseContext _databaseContext;

    public AppointmentsRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    Result<Appointment> IRepository<Appointment>.Get(Guid id)
    {
        var appointment = _databaseContext.Appointments.Find(id);
        return appointment is null ? Result<Appointment>.Error("Appointment not found") : Result<Appointment>.Ok(appointment);
    }

    IEnumerable<Appointment> IRepository<Appointment>.GetAll()
    {
        return _databaseContext.Appointments;
    }

    Result IRepository<Appointment>.Add(Appointment entity)
    {
        _databaseContext.Appointments.Add(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<Appointment>.Update(Appointment entity)
    {
        _databaseContext.Appointments.Update(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }

    Result IRepository<Appointment>.Delete(Appointment entity)
    {
        _databaseContext.Appointments.Remove(entity);
        _databaseContext.SaveChanges();
        return Result.Ok();
    }
}
