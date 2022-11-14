namespace VetAppointment.Model;

using VetAppointment.Util;

class Clinic
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public int Capacity { get; private set; }
    public string OwnerName { get; private set; }
    public string OwnerEmail { get; private set; }
    public string OwnerPhone { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    List<Appointment> Appointments { get; }
    CageSchedule CageSchedule { get; }

    private Clinic(Guid Id, string Name, string Address, int Capacity, string OwnerName, string OwnerEmail, string OwnerPhone, DateTime RegistrationDate)
    {
        this.Id = Id;
        this.Name = Name;
        this.Address = Address;
        this.Capacity = Capacity;
        this.OwnerName = OwnerName;
        this.OwnerEmail = OwnerEmail;
        this.OwnerPhone = OwnerPhone;
        this.RegistrationDate = RegistrationDate;
        this.Appointments = new List<Appointment>();
        this.CageSchedule = new CageSchedule();
    }

    public static Result<Clinic> Create(string name, string address, int capacity, string ownerName, string ownerEmail, string ownerPhone)
    {
        var minimumNumberOfPlaces = 0;
        if (capacity <= 0)
        {
            return Result<Clinic>.Failure($"The number of places for the clinic needs to be greater than '{minimumNumberOfPlaces}'");
        }
        var clinic = new Clinic
        (
            Guid.NewGuid(),
            name,
            address,
            capacity,
            ownerName,
            ownerEmail,
            ownerPhone,
            DateTime.Now
        );
        return Result<Clinic>.Success(clinic);
    }

}