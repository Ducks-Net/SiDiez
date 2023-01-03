using DucksNet.Application.Requests.EmployeeRequests;
using DucksNet.Application.Requests.MedicalRecordRequests;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using MediatR;

namespace DucksNet.Application.Handlers.MedicalRecordHandlers;
public class GetMedicalRecordHandler : IRequestHandler<GetMedicalRecordRequest, MedicalRecordResultResponse>
{
    private readonly IRepositoryAsync<MedicalRecord> _medicalRecordRepository;

    public GetMedicalRecordHandler(IRepositoryAsync<MedicalRecord> _medicalRecordRepository)
    {
        this._medicalRecordRepository = _medicalRecordRepository;
    }
    public async Task<MedicalRecordResultResponse> Handle(GetMedicalRecordRequest request, CancellationToken cancellationToken)
    {
        var medicalRecord = await _medicalRecordRepository.GetAsync(request.MedicalRecordId);
        if (medicalRecord.IsFailure)
        {
            return new MedicalRecordResultResponse(null, medicalRecord.Errors, ETypeRequests.NOT_FOUND);
        }
        return new MedicalRecordResultResponse(medicalRecord.Value, null, ETypeRequests.OK);
    }
}
