using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Application.Responses;
public class EmployeeResultResponse
{
    public Employee? Value { get; set; }
    public bool IsSuccess { get; set; }
    public List<string>? Errors { get; set; } = new List<string>();

    public EmployeeResultResponse(Employee? value, bool isSuccess, List<string>? errors)
    {
        Value = value;
        IsSuccess = isSuccess;
        Errors = errors ?? new List<string>();
    }
}
