using DucksNet.SharedKernel.Utils;

namespace DucksNet.Application.Responses;
public class EmployeeResponse
{
    public Guid Id { get; private set; }
    public Guid IdOffice { get; private set; }
    public string? Surname { get; private set; }
    public string? FirstName { get; private set; }
    public string? Address { get; private set; }
    public string? OwnerPhone { get; private set; }
    public string? OwnerEmail { get; private set; }
}
