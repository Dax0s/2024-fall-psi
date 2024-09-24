using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemoryGameWithNumbersController : ControllerBase
    {
        [HttpGet("start")]
        public ActionResult<List<int?>> StartGame([FromQuery] int maxNumber)
        {
            List<int> generatedNumbers = new List<int>();

            Random rand = new Random();

            generatedNumbers = Enumerable.Range(1, maxNumber)
                                         .OrderBy(x => rand.Next())
                                         .ToList();

            List<int?> gridNumbers = new List<int?>(generatedNumbers.Select(n => (int?)n));

            while (gridNumbers.Count < 16)
            {
                gridNumbers.Add(null);
            }

            gridNumbers = gridNumbers.OrderBy(x => rand.Next()).ToList();
            return Ok(gridNumbers);
        }
    }
}
