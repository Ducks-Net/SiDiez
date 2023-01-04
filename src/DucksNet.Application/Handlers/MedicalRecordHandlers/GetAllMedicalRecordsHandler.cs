using DucksNet.Application.Mappers;
using DucksNet.Application.Requests.EmployeeRequests;
using DucksNet.Application.Requests.MedicalRecordRequests;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using MediatR;

namespace DucksNet.Application.Handlers.MedicalRecordHandlers;
public class GetAllMedicalRecordsHandler : IRequestHandler<GetAllMedicalRecordRequest, List<MedicalRecordResponse>>
{
    private readonly IRepositoryAsync<MedicalRecord> _repository;
    public GetAllMedicalRecordsHandler(IRepositoryAsync<MedicalRecord> _repository)
    {
        this._repository = _repository;
    }
    public async Task<List<MedicalRecordResponse>> Handle(GetAllMedicalRecordRequest request, CancellationToken cancellationToken)
    {
        var result = MedicalRecordMapper.Mapper.Map<List<MedicalRecordResponse>>(await _repository.GetAllAsync());
        return result;
    }
}
