
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MedicineController : ControllerBase
{
    private readonly IRepositoryAsync<Medicine> _medicineRepository;
    private readonly IValidator<MedicineDTO> _validatorMedicine;
    public MedicineController(IValidator<MedicineDTO> validatorMedicine, IRepositoryAsync<Medicine> repository)
    {
        _validatorMedicine = validatorMedicine;
        _medicineRepository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var medicines = await _medicineRepository.GetAllAsync();
        return Ok(medicines);
    }

    [HttpGet("byName/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var medicine = await _medicineRepository.GetAllAsync();
        medicine = medicine.Where(m => m.Name == name).ToList();
        return Ok(medicine);
    }

    [HttpGet("byDescription/{description}")]
    public async Task<IActionResult> GetByDescription(string description)
    {
        var medicine = await _medicineRepository.GetAllAsync();
        medicine = medicine.Where(m => m.Description == description).ToList();
        return Ok(medicine);
    }

    [HttpGet("byDrugAdministration/{drugAdministration}")]
    public async Task<IActionResult> GetByClinicId(string drugAdministration)
    {
        var medicine = await _medicineRepository.GetAllAsync();
        medicine = medicine.Where(m => m.DrugAdministration.Name == drugAdministration).ToList();
        return Ok(medicine);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MedicineDTO dto)
    {
        ValidationResult resultValidate = await _validatorMedicine.ValidateAsync(dto,
            options => options.IncludeRuleSets("CreateMedicine"));
        if (!resultValidate.IsValid)
        {
            List<string> errorsList = new List<string>();
            foreach (var error in resultValidate.Errors)
            {
                errorsList.Add(error.ErrorMessage);
            }
            return BadRequest(errorsList);
        }
        //NOTE (MG) : add validator to remove this
        var medicinePost = Medicine.Create(dto.Name, dto.Description, dto.Price, dto.DrugAdministrationString);
        if (medicinePost.IsFailure)
        {
            return BadRequest(medicinePost.Errors);
        }

        var result = await _medicineRepository.AddAsync(medicinePost.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(medicinePost.Value);
    }

    [HttpDelete("{token:guid}")]
    public async Task<IActionResult> Delete(Guid token)
    {
        var medicine = await _medicineRepository.GetAsync(token);
        if (medicine.IsFailure)
        {
            return BadRequest(medicine.Errors);
        }
        var result = await _medicineRepository.DeleteAsync(medicine.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }

    [HttpPut("{token:guid}")]
    public async Task<IActionResult> UpdateMedicine(Guid token, [FromBody] MedicineDTO dto)
    {
        var oldMedicine = await _medicineRepository.GetAsync(token);
        if (oldMedicine.IsFailure)
        {
            return BadRequest(oldMedicine.Errors);
        }
        ValidationResult resultValidate = await _validatorMedicine.ValidateAsync(dto,
            options => options.IncludeRuleSets("CreateMedicine"));
        if (!resultValidate.IsValid)
        {
            List<string> errorsList = new List<string>();
            foreach (var error in resultValidate.Errors)
            {
                errorsList.Add(error.ErrorMessage);
            }
            return BadRequest(errorsList);
        }
        oldMedicine.Value!.UpdateMedicineFields(dto.Name, dto.Description, dto.Price, dto.DrugAdministrationString);
        var result = await _medicineRepository.UpdateAsync(oldMedicine.Value);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(oldMedicine.Value);
    }
}
