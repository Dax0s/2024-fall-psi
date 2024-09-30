
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace backend.SequenceGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SequenceGameController : ControllerBase
    {
        [HttpGet("getSequence")]
        public ActionResult<List<int>> GetSequence([FromQuery] string sequence = "")
        {
            Random random = new Random();
            List<int> newSequence = string.IsNullOrEmpty(sequence) ? new List<int>() : sequence.Split(',').Select(int.Parse).ToList();
            newSequence.Add(random.Next(1, 10));

            return Ok(newSequence);
        }
    }
}
