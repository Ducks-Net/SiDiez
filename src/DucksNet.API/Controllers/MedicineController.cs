using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MedicineController : ControllerBase
{
    private readonly IRepository<Medicine> _medicineRepository;
    public MedicineController(IRepository<Medicine> repository)
    {
        _medicineRepository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var medicines = _medicineRepository.GetAll();
        return Ok(medicines);
    }

    [HttpGet("byName/{name}")]
    public IActionResult GetByName(string name)
    {
        var medicine = _medicineRepository.GetAll().Where(m => m.Name == name).ToList();
        return Ok(medicine);
    }

    [HttpGet("byDescription/{description}")]
    public IActionResult GetByDescription(string description)
    {
        var medicine = _medicineRepository.GetAll().Where(m => m.Description == description).ToList();
        return Ok(medicine);
    }

    [HttpGet("byDrugAdministration/{drugAdministration}")]
    public IActionResult GetByClinicID(string drugAdministration)
    {
        var medicine = _medicineRepository.GetAll().Where(m => m.DrugAdministration.Name == drugAdministration).ToList();
        return Ok(medicine);
    }

    [HttpPost]
    public IActionResult Create([FromBody] MedicineDTO dto)
    {
        var medicinePost = Medicine.Create(dto.Name, dto.Description, dto.Price, dto.DrugAdministrationString);
        if (medicinePost.IsFailure)
        {
            return BadRequest(medicinePost.Errors);
        }

        var result = _medicineRepository.Add(medicinePost.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(medicinePost.Value);
    }

    [HttpDelete("{token:guid}")]
    public IActionResult Delete(Guid token)
    {
        var medicine = _medicineRepository.Get(token);
        if (medicine.IsFailure)
        {
            return BadRequest(medicine.Errors);
        }
        var result = _medicineRepository.Delete(medicine.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }

    [HttpPut("{token:guid}")]
    public IActionResult UpdateMedicine(Guid token, [FromBody] MedicineDTO dto)
    {
        var oldMedicine = _medicineRepository.Get(token);
        if (oldMedicine.IsFailure)
        {
            return BadRequest(oldMedicine.Errors);
        }
        oldMedicine.Value!.UpdateMedicineFields(dto.Name, dto.Description, dto.Price, dto.DrugAdministrationString);
        var result = _medicineRepository.Update(oldMedicine.Value);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(oldMedicine.Value);
    }
}
