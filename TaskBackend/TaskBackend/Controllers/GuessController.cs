using System.Text;
using Microsoft.AspNetCore.Mvc;
using Task.Modeles;
using Task.Repositories;
using Task.Services;

namespace Task.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuessController : ControllerBase
{
    private IGuessService _service;
    private ILogger<GuessController> _logger;
    private IUnitOfWork _unitOfWork;

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

    [HttpGet]
    public ActionResult GetAllLeaders()
    {
      var gameLeaders = _service.GameLeader();

        return Ok(gameLeaders);
    }
}

