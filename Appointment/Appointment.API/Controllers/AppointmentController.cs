using Appointment.API.DTOs;
using AppointmentData;
using AppointmentService;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController: ControllerBase
{
    private readonly IScheduleService _scheduleService;
    
    public AppointmentController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }    

    
    //customer
    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateDto model)
    {
        // create appointment

        return Ok("");
    }

    // customer
    [HttpPost]
    public async Task<IActionResult> GetScheduleList([FromBody] ScheduleListDto model)
    {
        var schedules = await _scheduleService.GetScheduleList(model.LocationId, model.EmployeeId, model.StartDate,model.EndDate);
        if (schedules?.Schedules == null  || schedules.Schedules.Count == 0)
        {
            return NotFound();
        }
            
        return Ok(schedules);
    }

  


}