using DucksNet.Domain.Model;

namespace DucksNet.WebUI.Pages.Services;
public interface IMedicineDataService
{
    Task<IEnumerable<Medicine>> GetAllMedicine();
    Task<Medicine> GetMedicineDetail(Guid medicineId);
}
