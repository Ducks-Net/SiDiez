using DucksNet.Domain.Model;

namespace DucksNet.WebUI.Pages.Services;
public interface ITreatmentDataService
{
    Task<IEnumerable<Treatment>> GetAllTreatments();
    Task<Treatment> GetTreatmentDetail(Guid treatmentId);
}
