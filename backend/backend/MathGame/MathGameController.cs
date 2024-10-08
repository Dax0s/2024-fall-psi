using Microsoft.AspNetCore.Mvc;

namespace backend.MathGame.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MathGameController : ControllerBase
{
    private static readonly string puzzleFilePath = Path.Combine("data", "puzzles.txt");
    private static List<string> puzzles = new List<string>();
    private static int puzzleIndex;
    static MathGameController()
    {
        LoadPuzzlesFromFile();
    }
    [HttpGet("next")]
    public ActionResult<string> GetNextPuzzle()
    {
        if (puzzles.Count == 0)
        {
            return NotFound("No puzzles available. Please check the puzzle file.");
        }

        if (puzzleIndex >= puzzles.Count)
        {
            puzzleIndex = 0;
        }

        string puzzle = puzzles[puzzleIndex];
        puzzleIndex++;
        return puzzle;
    }

    [HttpPost("solve")]
    public ActionResult<bool> CheckAnswer([FromBody] SolvePuzzleRequest request)
    {
        bool isCorrect = CheckPuzzleAnswer(request.Puzzle, request.Answer);
        return Ok(isCorrect);
    }

    private static void LoadPuzzlesFromFile()
    {
        try
        {
            if (!System.IO.File.Exists(puzzleFilePath))
            {
                Console.WriteLine($"Puzzle file not found at {puzzleFilePath}");
            }

            using (FileStream fileStream = new FileStream(puzzleFilePath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line;
                puzzles.Clear();

                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        puzzles.Add(line.Trim());
                    }
                }
            }

            if (puzzles.Count == 0)
            {
                Console.WriteLine("The puzzle file is empty.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading puzzle file: {ex.Message}");
        }
    }

    private static bool CheckPuzzleAnswer(string puzzle, string answer)
    {
        try
        {
            var tokens = puzzle.Split(' ');
            int leftOperand = int.Parse(tokens[0]);
            string operation = tokens[1];
            int rightOperand = int.Parse(tokens[2]);

            // Solve the puzzle based on the operator
            int correctAnswer = operation switch
            {
                "+" => leftOperand + rightOperand,
                "-" => leftOperand - rightOperand,
                "*" => leftOperand * rightOperand,
                "/" => leftOperand / rightOperand,
                _ => 0
            };

            return int.Parse(answer) == correctAnswer;
        }
        catch
        {
            return false;
        }
    }

    public class SolvePuzzleRequest
    {
        public required string Puzzle { get; set; }
        public required string Answer { get; set; }
    }
}
