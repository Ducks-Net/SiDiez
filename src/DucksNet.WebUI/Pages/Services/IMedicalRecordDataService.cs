using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;

namespace DucksNet.WebUI.Pages.Services;
public interface IMedicalRecordDataService
{
    Task CreateMedicalRecord(CreateMedicalRecord createMedicalRecord);
    Task DeleteMedicalRecord(string medicalRecordId);
    Task<IEnumerable<MedicalRecord>> GetAllMedicalRecords();
    Task UpdateEmployee(string medicalRecordId, UpdateMedicalRecord updateMedicalRecordModel);
}
