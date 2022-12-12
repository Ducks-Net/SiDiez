using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;

namespace DucksNet.WebUI.Pages.Services;
public interface IMedicineDataService
{
    Task CreateMedicine(CreateMedicineModel createMedicineModel);
    Task DeleteMedicine(string medicineId);
    Task<IEnumerable<Medicine>> GetAllMedicine();
    Task UpdateMedicine(string medicineId, UpdateMedicineModel updateMedicineModel);
}
