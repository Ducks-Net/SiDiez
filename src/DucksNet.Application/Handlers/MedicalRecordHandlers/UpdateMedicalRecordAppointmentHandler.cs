using DucksNet.Application.Requests.MedicalRecordRequests;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using MediatR;

namespace DucksNet.Application.Handlers.MedicalRecordHandlers;
public class UpdateMedicalRecordAppointmentHandler : IRequestHandler<UpdateMedicalRecordAppointmentRequest, MedicalRecordResultResponse>
{
    private readonly IRepositoryAsync<MedicalRecord> _medicalRepository;
    private readonly IRepositoryAsync<Appointment> _appointmentRepository;

    public UpdateMedicalRecordAppointmentHandler(IRepositoryAsync<MedicalRecord> medicalRepository, IRepositoryAsync<Appointment> appointmentRepository)
    {
        _medicalRepository = medicalRepository;
        _appointmentRepository = appointmentRepository;
    }
    public async Task<MedicalRecordResultResponse> Handle(UpdateMedicalRecordAppointmentRequest request, CancellationToken cancellationToken)
    {
        var oldMedicalRecord = await _medicalRepository.GetAsync(request.MedicalRecordId);
        if (oldMedicalRecord.IsFailure)
        {
            return new MedicalRecordResultResponse(null, oldMedicalRecord.Errors, ETypeRequests.BAD_REQUEST);
        }
        var newAppointment = await _appointmentRepository.GetAsync(request.NewAppointmentId);
        if (newAppointment.IsFailure)
        {
            return new MedicalRecordResultResponse(null, newAppointment.Errors, ETypeRequests.BAD_REQUEST);
        }
        oldMedicalRecord.Value!.AssignToAppointment(request.NewAppointmentId);
        await _medicalRepository.UpdateAsync(oldMedicalRecord.Value);
        return new MedicalRecordResultResponse(oldMedicalRecord.Value, null, ETypeRequests.OK);
    }
}
