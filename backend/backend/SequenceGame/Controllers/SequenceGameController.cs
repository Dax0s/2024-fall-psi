using backend.SequenceGame.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.SequenceGame.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SequenceGameController : ControllerBase
{
    private readonly ISequenceGameService _service;

    public SequenceGameController(ISequenceGameService service)
    {
        _service = service;
    }

    [HttpGet("getSequence")]
    public ActionResult<List<int>> GetSequence([FromQuery] string sequence = "")
    {
        if (!string.IsNullOrEmpty(sequence))
        {
            var parts = sequence.Split(',');

            var isValidSequence = parts.All(part => int.TryParse(part, out var number) && number is >= 1 and <= 9);

            if (!isValidSequence)
            {
                return BadRequest();
            }
        }

        return Ok(_service.GetSequence(sequence));
    }
}
