using Microsoft.AspNetCore.Mvc;
using Task.Services;

namespace Task.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuessController : ControllerBase
{
    private IGuessService _service;
    private ILogger<GuessController> _logger;

    public GuessController(ILogger<GuessController> logger, IGuessService service)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost("{name}")]
    public ActionResult UserSave(string name)
    {

        var user = _service.ExistUser(name);
        if (user == null)
        {
            return BadRequest();
        }
        return Ok(user);
    }
    [HttpPost("GameStart/{id}")]
    public ActionResult StartPlay(int id, [FromBody] int number)
    {
        var result = _service.PlayResult(id, number);

        return Ok(result);
    }

    [HttpGet("{min}")]
    public ActionResult GetAllLeaders(int min = 0)
    {
      var gameLeader = _service.GameLeader(min);

        return Ok(gameLeader);
    }
}

