namespace ePOS.API.Controllers;

[ApiController]
[Route("api/v1")]
public class PingController : ControllerBase
{
    [HttpGet]
    [Route("ping")]
    public Task<IActionResult> Ping()
    {
        return Task.FromResult<IActionResult>(Ok("Pong"));
    }
}