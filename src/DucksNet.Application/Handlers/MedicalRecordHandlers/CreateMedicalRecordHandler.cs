using DucksNet.API.DTO;
using DucksNet.Application.Mappers;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using MediatR;

namespace DucksNet.Application.Handlers.MedicalRecordHandlers;
public class CreateMedicalRecordHandler : IRequestHandler<MedicalRecordDto, MedicalRecordResultResponse>
{
    private readonly IRepositoryAsync<MedicalRecord> _medicalRepository;
    private readonly IRepositoryAsync<Pet> _clientRepository;
    private readonly IRepositoryAsync<Appointment> _appointmentRepository;

    public CreateMedicalRecordHandler(IRepositoryAsync<MedicalRecord> medicalRepository, IRepositoryAsync<Pet> clientRepository, IRepositoryAsync<Appointment> appointmentRepository)
    {
        _medicalRepository = medicalRepository;
        _clientRepository = clientRepository;
        _appointmentRepository = appointmentRepository;
    }
    public async Task<MedicalRecordResultResponse> Handle(MedicalRecordDto request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetAsync(request.IdAppointment);
        if (appointment.IsFailure)
        {
            return new MedicalRecordResultResponse(null, appointment.Errors, ETypeRequests.BAD_REQUEST);
        }
        var client = await _clientRepository.GetAsync(request.IdClient);
        if (client.IsFailure)
        {
            return new MedicalRecordResultResponse(null, client.Errors, ETypeRequests.BAD_REQUEST);
        }
        var medicalRecordPost = MedicalRecordMapper.Mapper.Map<MedicalRecord>(request);
        await _medicalRepository.AddAsync(medicalRecordPost);
        return new MedicalRecordResultResponse(medicalRecordPost, null, ETypeRequests.OK);
    }
}
