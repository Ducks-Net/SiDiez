﻿using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;
public class Employee
{
    private Employee(Guid idSediu, string surname, string firstName, string address, string ownerPhone, string ownerEmail)
    {
        Id = Guid.NewGuid();
        IdSediu = idSediu;
        Surname = surname;
        FirstName = firstName;
        Address = address;
        OwnerPhone = ownerPhone;
        OwnerEmail = ownerEmail;
    }

    public Guid Id { get; private set; }
    public Guid IdSediu { get; private set; }
    public string Surname { get; private set; }
    public string FirstName { get; private set; }
    public string Address { get; private set; }
    public string OwnerPhone { get; private set; }
    public string OwnerEmail { get; private set; }

    public static Result<Employee> Create(Guid idSediu, string surname, string firstName, string address, string ownerPhone, string ownerEmail)
    {
        //TODO (RO): to check if the employee is valid
        var employee = new Employee(idSediu, surname, firstName, address, ownerPhone, ownerEmail);
        return Result<Employee>.Ok(employee);
    }
    public void AssignToSediu(Guid idSediu)
    {
        IdSediu = idSediu;
    }
}
