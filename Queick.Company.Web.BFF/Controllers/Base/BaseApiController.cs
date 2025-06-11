using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Queick.Company.Web.BFF.Controllers.Base;


[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public abstract class BaseApiController : ControllerBase
{
    
}