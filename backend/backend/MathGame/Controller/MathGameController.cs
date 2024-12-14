using backend.MathGame.Models;
using backend.MathGame.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.MathGame.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MathGameController : ControllerBase
{
    private readonly MathGameService _mathGameService;

    public MathGameController(MathGameService mathGameService)
    {
        _mathGameService = mathGameService;
    }

    [HttpGet("next")]
    public async Task<ActionResult<string>> GetNextPuzzle()
    {
        var puzzle = await _mathGameService.GetNextPuzzleAsync().ConfigureAwait(false);
        if (puzzle == null)
        {
            return NotFound("No puzzles available. Please check the database.");
        }

        return Ok(puzzle);
    }

    [HttpPost("solve")]
    public async Task<ActionResult<bool>> CheckAnswer([FromBody] SolvePuzzleRequest request)
    {
        bool isCorrect = await _mathGameService.CheckAnswerAsync(request.Puzzle, request.Answer).ConfigureAwait(false);
        return Ok(isCorrect);
    }
}
