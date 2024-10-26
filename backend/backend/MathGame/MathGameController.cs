using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace backend.MathGame;

[Route("api/[controller]")]
[ApiController]
public class MathGameController : ControllerBase
{
    private static readonly string puzzleFilePath = Path.Combine("data", "puzzles.txt");
    private static readonly List<string> puzzles = new List<string>();
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
                return;
            }

            using (FileStream fileStream = new FileStream(puzzleFilePath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string? line;
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
            if (tokens.Length < 3)
            {
                return false;
            }

            if (int.TryParse(tokens[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int leftOperand) &&
                int.TryParse(tokens[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out int rightOperand))
            {
                string operation = tokens[1];

                int correctAnswer = operation switch
                {
                    "+" => leftOperand + rightOperand,
                    "-" => leftOperand - rightOperand,
                    "*" => leftOperand * rightOperand,
                    "/" => rightOperand != 0 ? leftOperand / rightOperand : 0,
                    _ => 0
                };

                return int.TryParse(answer, NumberStyles.Integer, CultureInfo.InvariantCulture, out int userAnswer) &&
                       userAnswer == correctAnswer;
            }
            else
            {
                return false; // Invalid puzzle format or parsing issue
            }
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
