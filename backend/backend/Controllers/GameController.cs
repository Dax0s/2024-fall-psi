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

        // Endpoint to start the game and generate the grid numbers
        [HttpGet("start")]
        public ActionResult<List<int>> StartGame()
        {
            Random rand = new Random();
            generatedNumbers = Enumerable.Range(1, 6)
                                         .OrderBy(x => rand.Next())
                                         .Take(5)
                                         .ToList();

            // Pad the rest of the grid with random numbers between 1 and 6
            List<int> gridNumbers = new List<int>(generatedNumbers);
            while (gridNumbers.Count < 16)
            {
                gridNumbers.Add(rand.Next(1, 7)); // Numbers between 1 and 6
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
