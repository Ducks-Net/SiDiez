using DucksNet.Application.Responses;
using MediatR;

namespace DucksNet.Application.Requests.MedicalRecordRequests;
public class GetAllMedicalRecordRequest : IRequest<List<MedicalRecordResponse>> { }
