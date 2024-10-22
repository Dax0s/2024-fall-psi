using Microsoft.AspNetCore.Mvc;

namespace backend.MemoryGameWithNumbers.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemoryGameWithNumbersController : ControllerBase
{
    private static List<int?>? _correctSequence;

    [HttpGet("start")]
    public ActionResult<List<int?>> StartGame([FromQuery] int maxNumber)
    {
        Random rand = new Random();

        List<int?> correctSequence = Enumerable.Range(1, maxNumber)
                                               .Select(n => (int?)n) // Boxing happens here when converting int to int?
                                               .ToList();

        _correctSequence = new List<int?>(correctSequence);

        List<int?> gridNumbers = new List<int?>(correctSequence.OrderBy(x => rand.Next()));

        while (gridNumbers.Count < 16)
        {
            gridNumbers.Add(null);
        }

        gridNumbers = gridNumbers.OrderBy(x => rand.Next()).ToList();

        return Ok(gridNumbers);
    }

    [HttpPost("attempt")]
    public ActionResult<bool> CheckAttempt([FromBody] List<int?> userAttempt)
    {
        if (_correctSequence == null)
        {
            return BadRequest("Game not started.");
        }

        for (int i = 0; i < _correctSequence.Count; i++)
        {
            if (i >= userAttempt.Count || _correctSequence[i] == null || userAttempt[i] == null)
            {
                return Ok(false);
            }

        }

        return Ok(true);
    }
}
