using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model.Enums;

public class AppointmentType : Enumeration
{
    public static readonly AppointmentType Consultation = new(1, "Consultation");
    public static readonly AppointmentType Vaccination = new(2, "Vaccination");
    public static readonly AppointmentType Surgery = new(3, "Surgery");

    public AppointmentType(int id, string name) : base(id, name) { }

    public static Result<AppointmentType> CreateFromString(string str)
    {
        var at = GetAll<AppointmentType>().FirstOrDefault(x => x.Name == str);
        var validAppoitnmentTypes = string.Join(", ", GetAll<AppointmentType>().Select(x => x.Name));
        if (at is null)
            return Result<AppointmentType>.Error($"Invalid AppointmentType string. Valid values are: {validAppoitnmentTypes}."); 

        return Result<AppointmentType>.Ok(at);
    }

    public static Result<AppointmentType> CreateFromInt(int id)
    {
        var at = GetAll<AppointmentType>().FirstOrDefault(x => x.Id == id);
        var validAppoitnmentTypes = string.Join(", ", GetAll<AppointmentType>().Select(x => x.Id));
        if (at is null)
            return Result<AppointmentType>.Error($"Invalid AppointmentType id. Valid values are: {validAppoitnmentTypes}."); 

        return Result<AppointmentType>.Ok(at);
    }
}
