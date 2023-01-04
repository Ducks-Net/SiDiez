using DucksNet.Application.Requests.MedicalRecordRequests;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using MediatR;

namespace DucksNet.Application.Handlers.MedicalRecordHandlers;
public class UpdateMedicalRecordClientHandler : IRequestHandler<UpdateMedicalRecordClientRequest, MedicalRecordResultResponse>
{
    private readonly IRepositoryAsync<MedicalRecord> _medicalRepository;
    private readonly IRepositoryAsync<Pet> _clientRepository;

    public UpdateMedicalRecordClientHandler(IRepositoryAsync<MedicalRecord> medicalRepository, IRepositoryAsync<Pet> clientRepository)
    {
        _medicalRepository = medicalRepository;
        _clientRepository = clientRepository;
    }
    public async Task<MedicalRecordResultResponse> Handle(UpdateMedicalRecordClientRequest request, CancellationToken cancellationToken)
    {
        var oldMedicalRecord = await _medicalRepository.GetAsync(request.MedicalRecordId);
        if (oldMedicalRecord.IsFailure)
        {
            return new MedicalRecordResultResponse(null, oldMedicalRecord.Errors, ETypeRequests.BAD_REQUEST);
        }
        var newClient = await _clientRepository.GetAsync(request.NewClientId);
        if (newClient.IsFailure)
        {
            return new MedicalRecordResultResponse(null, newClient.Errors, ETypeRequests.BAD_REQUEST);
        }
        oldMedicalRecord.Value!.AssignToClient(request.NewClientId);
        await _medicalRepository.UpdateAsync(oldMedicalRecord.Value);
        return new MedicalRecordResultResponse(oldMedicalRecord.Value, null, ETypeRequests.OK);
    }
}
