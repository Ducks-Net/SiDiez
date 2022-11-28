using DucksNet.Domain.Model;

namespace DucksNet.WebUI.Pages.Services;
public interface IMedicalRecordDataService
{
    Task<IEnumerable<MedicalRecord>> GetAllPersons();
    Task<MedicalRecord> GetPersonDetail(Guid personId);
}
