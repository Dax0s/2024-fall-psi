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
        var isValid = _service.ParseAndValidateSequence(sequence);
        if (!isValid) return BadRequest();

        return Ok(_service.ExtendSequence());
    }
}
