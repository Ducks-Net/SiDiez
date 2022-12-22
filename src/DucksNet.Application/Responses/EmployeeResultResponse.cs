using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Application.Responses;
public class EmployeeResultResponse
{
    public Employee? Value { get; set; }
    public List<string>? Errors { get; set; } = new List<string>();
    public ETypeRequests TypeRequest { get; set; }

    public EmployeeResultResponse(Employee? value, List<string>? errors, ETypeRequests typeRequest)
    {
        Value = value;
        Errors = errors ?? new List<string>();
        TypeRequest = typeRequest;
    }
}
