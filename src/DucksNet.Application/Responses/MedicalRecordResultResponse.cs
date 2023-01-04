using DucksNet.Domain.Model;

namespace DucksNet.Application.Responses;
public class MedicalRecordResultResponse
{
    public MedicalRecord? Value { get; set; }
    public List<string>? Errors { get; set; } = new List<string>();
    public ETypeRequests TypeRequest { get; set; }

    public MedicalRecordResultResponse(MedicalRecord? value, List<string>? errors, ETypeRequests typeRequest)
    {
        Value = value;
        Errors = errors ?? new List<string>();
        TypeRequest = typeRequest;
    }
}
