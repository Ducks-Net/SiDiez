using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;

namespace DucksNet.WebUI.Pages.Services;
public interface ITreatmentDataService
{
    Task CreateTreatment(CreateTreatmentModel createTreatmentModel);
    Task DeleteTreatment(string treatmentId);
    Task<IEnumerable<Treatment>> GetAllTreatment();
    Task UpdateTreatment(string treatmentId, UpdateTreatmentModel updateTreatmentModel);
}