namespace VetAppointment.Model;


class Employee
{
    private Guid ID;
    public string CNP { get; }
    public string Name { get; }
    public string Surname { get; }
    public Gender Gender { get; }
    public DateOnly Birthday { get; }
    public string Address { get; }
    public string EmailAddress { get; }
    public string Phone { get; set; }
    public EmployeeFunction Function { get; }
    
    private Employee(Guid id, string CNP, string Name, string Surname, Gender geneder, DateOnly birthday,
                    string address, string emailAddress, string phone, EmployeeFunction function) {
        
        this.ID = id;
        this.CNP = CNP;
        this.Name = Name;
        this.Surname = Surname;
        this.Gender = Gender;
        this.Birthday = birthday;
        this.Address = address;
        this.EmailAddress = emailAddress;
        this.Phone = phone;
        this.Function = function;
    }
}