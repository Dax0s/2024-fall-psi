using backend.MathGame.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.MathGame;

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
    public ActionResult<string> GetNextPuzzle()
    {
        var puzzle = _mathGameService.GetNextPuzzle();
        if (puzzle == null)
        {
            return NotFound("No puzzles available. Please check the puzzle file.");
        }

        return puzzle;
    }

    [HttpPost("solve")]
    public ActionResult<bool> CheckAnswer([FromBody] SolvePuzzleRequest request)
    {
        bool isCorrect = _mathGameService.CheckAnswer(request.Puzzle, request.Answer);
        return Ok(isCorrect);
    }
}
