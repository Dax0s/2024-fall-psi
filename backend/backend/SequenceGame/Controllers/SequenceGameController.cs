
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace backend.SequenceGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SequenceGameController : ControllerBase
    {
        [HttpGet("getSequence")]
        public ActionResult<List<int>> GetSequence([FromQuery][Optional] string? sequence)
        {
            string newSequenceString = string.IsNullOrEmpty(sequence) ? "" : sequence;
            List<int> newSequence = new List<int>([1, 5, 2, 3]);

            return Ok(newSequence);
        }
    }
}
