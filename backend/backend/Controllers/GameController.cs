using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private static List<int> generatedNumbers = new List<int>();

        [HttpGet("start")]
        public ActionResult<List<int?>> StartGame()
        {
            Random rand = new Random();
            // Generate unique numbers from 1 to 5
            List<int> generatedNumbers = Enumerable.Range(1, 5)
                                                   .OrderBy(x => rand.Next())
                                                   .ToList();

            // Create a list of nullable integers and add generated numbers
            List<int?> gridNumbers = new List<int?>(generatedNumbers.Select(n => (int?)n));

            // Pad the rest of the grid with nulls (to maintain 16 items in the grid)
            while (gridNumbers.Count < 16)
            {
                gridNumbers.Add(null); // Fill remaining slots with nulls
            }

            // Shuffle the grid
            gridNumbers = gridNumbers.OrderBy(x => rand.Next()).ToList();
            return Ok(gridNumbers);
        }

        // Endpoint to verify if the clicked sequence is correct
        [HttpPost("verify")]
        public ActionResult<bool> VerifySequence([FromBody] List<int> clickedNumbers)
        {
            if (clickedNumbers.SequenceEqual(generatedNumbers))
            {
                return Ok(true); // Correct order
            }
            return Ok(false); // Incorrect order
        }
    }
}
