using Appointment.API.DTOs;
using Appointment.Data;
using Appointment.Service;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController: ControllerBase
{
    private readonly IScheduleService _scheduleService;
    private readonly IAppointmentEntityService _appointmentEntityService;
    
    public AppointmentController(IScheduleService scheduleService, IAppointmentEntityService appointmentEntityService)
    {
        _scheduleService = scheduleService;
        _appointmentEntityService = appointmentEntityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAppointmentById(long id)
    {
        var appointment = await _appointmentEntityService.GetById(id);

        if (appointment == null)
        {
            return NotFound();
        }
        
        return Ok(appointment);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateDto model)
    {
        var appointment = await _appointmentEntityService.CreateAppointment(
            customerId: model.CustomerId, 
            employeeId: model.EmployeeId, 
            locationId: model.LocationId,
            model.Description, 
            model.StartDate, 
            model.EndDate);

        if (appointment == null)
        {
            return NotFound();
        }

        return Ok(appointment);
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