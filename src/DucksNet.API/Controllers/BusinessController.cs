using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BusinessController : ControllerBase
{

    private readonly ILogger<BusinessController> _logger;

    public BusinessController(ILogger<BusinessController> logger)
    {
        _logger = logger;
    }
}
