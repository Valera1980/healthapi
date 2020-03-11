using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class UserController : ControllerBase
{
    [Route("user")]
    [Authorize]
    public IActionResult Get(){
        return Ok("88888888");
    }
}