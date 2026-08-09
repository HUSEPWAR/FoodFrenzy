using Microsoft.AspNetCore.Mvc;

namespace FoodFrenzy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodFrenzyController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            message = "Welcome to FoodFrenzy API",
            version = "v1",
            status = "Running"
        });
    }
}