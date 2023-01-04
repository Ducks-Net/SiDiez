using DucksNet.Application.Requests.EmployeeRequests;
using DucksNet.Application.Requests.MedicalRecordRequests;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using MediatR;

namespace DucksNet.Application.Handlers.MedicalRecordHandlers;
public class DeleteMedicalRecordHandler : IRequestHandler<DeleteMedicalRecordRequest, MedicalRecordResultResponse>
{
    private readonly IRepositoryAsync<MedicalRecord> _repository;

    public DeleteMedicalRecordHandler(IRepositoryAsync<MedicalRecord> repository)
    {
        _repository = repository;
    }
    public async Task<MedicalRecordResultResponse> Handle(DeleteMedicalRecordRequest request, CancellationToken cancellationToken)
    {
        var medicalRecord = await _repository.GetAsync(request.MedicalRecordId);
        if (medicalRecord.IsFailure)
        {
            return new MedicalRecordResultResponse(null, medicalRecord.Errors, ETypeRequests.BAD_REQUEST);
        }
        await _repository.DeleteAsync(medicalRecord.Value!);
        return new MedicalRecordResultResponse(null, null, ETypeRequests.OK);
    }
}
