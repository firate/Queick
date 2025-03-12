using Appointment.API.DTOs;
using AppointmentData;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController: ControllerBase
{
    public AppointmentController()
    {
        
    }    

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateDto model)
    {
        // create appointment

        return Ok("");
    }

    [HttpPost]
    public async Task<IActionResult> GetAvailableAppointmentList([FromBody] AppointmentCreateDto model)
    {
        // ilgili tarih aralığı için template'i getir, 
        // template'e uygun model oluştur, daha önce belirlenmiş randevuları, alındı olarak göster,
        // model'de henüz randevu oluşturulmamış, saatler için "available" göster
        
        return Ok("");
    }

  


}