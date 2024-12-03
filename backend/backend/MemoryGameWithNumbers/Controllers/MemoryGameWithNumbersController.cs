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
        return Ok(_memoryGameService.StartGame(maxNumber));
    }

    [HttpPost("attempt")]
    public ActionResult<bool> CheckAttempt([FromBody] List<int?> userAttempt)
{
    if (userAttempt == null || !userAttempt.Any())
    {
        return BadRequest(new { error = "Invalid attempt. The input is null or empty." });
    }

    if (_memoryGameService.IsGameStarted() == false)
    {
        return BadRequest(new { error = "Game not started." });
    }

    return Ok(_memoryGameService.CheckAttempt(userAttempt));
}

[HttpPost("restart")]
public IActionResult RestartGame()
{
    _memoryGameService.ResetGame();
    return Ok(new { message = "Game has been restarted." });
}

}
