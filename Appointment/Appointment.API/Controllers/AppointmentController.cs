using AppointmentData;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController: ControllerBase
{

    private readonly AppointmentDbContext _context;

    public AppointmentController(AppointmentDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task CreateAppointment([FromBody] AppointmentCreateDto model)
    {
        // create appointment
        
    }


}
