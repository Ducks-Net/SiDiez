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
   // public AppointmentType Type { get; private set; }
    public bool NeedsCage { get; private set; }
    public bool IsCancelled { get; private set; }

    private Appointment(/*AppointmentType type,*/ DateTime startTime, DateTime endTime)
    {
        ID = new Guid();
        //Type = type;
        StartTime = startTime;
        EndTime = endTime;
    }

   // private Appointment(int typeId, DateTime startTime, DateTime endTime)
    //    : this(AppointmentType.CreateFromInt(typeId).Value!, startTime, endTime) { }

    public static Result<Appointment> Create(AppointmentType type, DateTime startTime, DateTime endTime)
    {
        if (startTime > endTime)
            return Result<Appointment>.Error("Start time cannot be after end time.");

        return Result<Appointment>.Ok(new Appointment(/*type,*/ startTime, endTime));
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

    public void DoesNeedsCage()
    {
        NeedsCage = true;
    }

    public void DoesNotNeedCage()
    {
        NeedsCage = false;
    }

    public void Cancel()
    {
        IsCancelled = true;
    }
}
