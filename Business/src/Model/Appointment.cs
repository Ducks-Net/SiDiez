namespace VetAppointment.Model;

using VetAppointment.Util;

class Appointment {
    Owner Owner { get; }
    Client Client { get; }
    Clinic Clinic { get; }
    List<Employee> Employees { get; }
    DateTime StartTime { get; }
    DateTime EndTime { get; }
    string Description { get; }

    //TODO(AL): invoice
    private Appointment(Owner owner, Client client, Clinic clinic,  List<Employee> employees, DateTime start, DateTime end, string description) 
    {
         this.Owner = owner;
         this.Client = client;
         this.Clinic = clinic;
         this.Employees = employees;
         this.StartTime = start;
         this.EndTime = end;
         this.Description = description;
    }


    //TODO(AL): maybe standardise descriptions or add another field to standardise
    //TODO(AL): prefered to overload with just ids
    public Result<Appointment> Create(Owner owner, Client client, Clinic clinic, List<Employee> employees, DateTime start, DateTime end, string description)
    {
        return Result<Appointment>.Success(new Appointment(
            owner,
            client,
            clinic,
            employees,
            start,
            end,
            description
        ));
    }    

}