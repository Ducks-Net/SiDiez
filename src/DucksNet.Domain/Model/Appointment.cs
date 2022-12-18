using DucksNet.Domain.Model.Enums;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;

public class Appointment 
{
    public Guid ID { get; private set; }
    public Guid? LocationId { get; private set; }
    public Guid? PetId { get; private set; }
    public Guid? VetId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public AppointmentType Type { get; private set; }
    public bool NeedsCage { get; private set; }

    public Appointment(Guid ID, Guid? locationId, Guid? petId, Guid? vetId, DateTime startTime, DateTime endTime, AppointmentType type, bool needsCage)
    {
        this.ID = ID;
        LocationId = locationId;
        PetId = petId;
        VetId = vetId;
        StartTime = startTime;
        EndTime = endTime;
        Type = type;
        NeedsCage = needsCage;
    }

    private Appointment(AppointmentType type, DateTime startTime, DateTime endTime)
    {
        ID = Guid.NewGuid();
        Type = type;
        StartTime = startTime;
        EndTime = endTime;
    }

    public static Result<Appointment> Create(string typeString, DateTime startTime, DateTime endTime)
    {
        var type = AppointmentType.CreateFromString(typeString);
        if (type.IsFailure || type.Value is null)
            return Result<Appointment>.FromError(type, "Invalid appointment type.");
        if(startTime < DateTime.Now)
            return Result<Appointment>.Error("Start time cannot be in the past.");
        if (startTime > endTime)
            return Result<Appointment>.Error("Start time cannot be after end time.");

        return Result<Appointment>.Ok(new Appointment(type.Value, startTime, endTime));
    }

    public void AssignToLocation(Guid locationId)
    {
        LocationId = locationId;
    }

    public void AssignToPet(Guid petId)
    {
        PetId = petId;
    }

    public void AssignToVet(Guid vetId)
    {
        VetId = vetId;
    }

    public void AssignAll(Guid locationId, Guid petId, Guid vetId)
    {
        LocationId = locationId;
        PetId = petId;
        VetId = vetId;
    }

    public void DoesNeedCage()
    {
        NeedsCage = true;
    }

    public void DoesNotNeedCage()
    {
        NeedsCage = false;
    }
}
