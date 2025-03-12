using Microsoft.AspNetCore.Mvc;

namespace Appointment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentTemplateController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAppointmenTemplate()
    {
        return Ok();
    }
}