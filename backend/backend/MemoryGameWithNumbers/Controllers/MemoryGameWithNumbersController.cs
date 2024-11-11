using backend.MemoryGameWithNumbers.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.MemoryGameWithNumbers.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemoryGameWithNumbersController : ControllerBase
{
    private readonly MemoryGameService _memoryGameService;

    public MemoryGameWithNumbersController(MemoryGameService memoryGameService)
    {
        _memoryGameService = memoryGameService;
    }

    [HttpGet("start")]
    public ActionResult<List<int?>> StartGame([FromQuery] int maxNumber)
    {
        var gridNumbers = _memoryGameService.StartGame(maxNumber);
        return Ok(gridNumbers);
    }

    [HttpPost("attempt")]
    public ActionResult<bool> CheckAttempt([FromBody] List<int?> userAttempt)
    {
        if (_memoryGameService.IsGameStarted() == false)
        {
            return BadRequest("Game not started.");
        }

        var result = _memoryGameService.CheckAttempt(userAttempt);
        return Ok(result);
    }

}
