using AppointmentData;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController: ControllerBase
{
    

    [HttpPost]
    public async Task CreateAppointment([FromBody] AppointmentCreateDto model)
    {
        // create appointment
        
    }


}
