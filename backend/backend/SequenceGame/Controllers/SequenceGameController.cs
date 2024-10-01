using Microsoft.AspNetCore.Mvc;

namespace backend.SequenceGame.Controllers;

[ApiController]
[Route("[controller]")]
public class SequenceGameController : ControllerBase
{
    private List<int> NextSequence { get; set; } = new List<int>();

    [HttpGet("getSequence")]
    public ActionResult<List<int>> GetSequence([FromQuery] string sequence = "")
    {
        Random random = new Random();

        if (!string.IsNullOrEmpty(sequence))
        {
            NextSequence = sequence.Split(',').Select(int.Parse).ToList();
        }
        NextSequence.Add(random.Next(minValue: 1, maxValue: 10));

        return Ok(NextSequence);
    }
}
