using DucksNet.Application.Responses;
using MediatR;

namespace DucksNet.API.DTO;

public class EmployeeDto : IRequest<EmployeeResponse>
{
    public EmployeeDto(Guid idOffice, string surname, string firstName, string address, string ownerPhone, string ownerEmail)
    {
        IdOffice = idOffice;    
        Surname = surname;
        FirstName = firstName;
        Address = address;
        OwnerPhone = ownerPhone;
        OwnerEmail = ownerEmail;
    }
    public Guid IdOffice { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string Address { get; set; }
    public string OwnerPhone { get; set; }
    public string OwnerEmail { get; set; }
}
