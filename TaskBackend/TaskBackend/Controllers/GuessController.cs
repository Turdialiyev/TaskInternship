using System.Text;
using Microsoft.AspNetCore.Mvc;
using Task.Services;

namespace Task.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuessController : ControllerBase
{
    private IGuessService _service;

    public GuessController(IGuessService service)
    {
        _service = service;
    }
    [HttpPost]
    public ActionResult StartPlay(string userName, int number)
    {
        var result = _service.PlayResult(userName, number);

        return Ok(result.Item3.ToList());
    }
}
