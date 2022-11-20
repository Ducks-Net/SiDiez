using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;
using DucksNet.Infrastructure.Prelude;
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

    [HttpGet("{name}")]
    public IActionResult GetByName(string name)
    {
        var medicine = _medicineRepository.GetAll().Where(m => m.Name == name).ToList();
        return Ok(medicine);
    }

    [HttpGet("{description}")]
    public IActionResult GetByDescription(string description)
    {
        var medicine = _medicineRepository.GetAll().Where(m => m.Description == description).ToList();
        return Ok(medicine);
    }

    [HttpGet("{drugAdministration}")]
    public IActionResult GetByClinicID(DrugAdministration drugAdministration)
    {
        var medicine = _medicineRepository.GetAll().Where(m => m.DrugAdministration == drugAdministration).ToList();
        return Ok(medicine);
    }
}
