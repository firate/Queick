using Appointment.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route($"api/[controller]/[action]")]
public class CustomerController: ControllerBase
{
    public CustomerController()
    {
        
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        return BadRequest();
    }
    

}
