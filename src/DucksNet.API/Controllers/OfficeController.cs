using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OfficeController : ControllerBase
{

    private readonly ILogger<OfficeController> _logger;

    public OfficeController(ILogger<OfficeController> logger)
    {
        _logger = logger;
    }
}
