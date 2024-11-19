using System.Globalization;
using backend.MathGame.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace backend.MathGame;

public class MathGameService
{
    private readonly GamesDbContext _context;
    private readonly IMemoryCache _cache;
    private const string LogFilePath = "logs/MathGameService.log";

    public MathGameService(GamesDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
        LoadPuzzles();
    }

    private void LoadPuzzles()
    {
        try
        {
            if (!_cache.TryGetValue("Puzzles", out List<string>? puzzles) || puzzles == null)
            {
                puzzles = [.. _context.Puzzles.Select(p => p.Content)];
                if (!puzzles.Any())
                {
                    throw new PuzzlesNotFoundException("No puzzles found in the database.");
                }

                _cache.Set("Puzzles", puzzles);
                Console.WriteLine($"Loaded {puzzles.Count} puzzles from the database.");
            }
        }
        catch (PuzzlesNotFoundException ex)
        {
            LogException(ex);
            Console.WriteLine("Error: Unable to load puzzles. Please seed the database.");
            throw;
        }
        catch (Exception ex)
        {
            LogException(ex);
            Console.WriteLine("An unexpected error occurred while loading puzzles.");
            throw;
        }
    }

    private static void LogException(Exception ex)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath)!); // Ensure directory exists
            using var writer = new StreamWriter(LogFilePath, append: true);
            writer.WriteLine($"{DateTime.Now}: {ex.GetType().Name} - {ex.Message}");
            writer.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
        catch
        {
            Console.WriteLine("Failed to write to the log file.");
        }
    }

    public string? GetNextPuzzle()
    {
        var puzzles = _cache.Get<List<string>>("Puzzles");
        if (puzzles == null || puzzles.Count == 0) { return null; }

        int puzzleIndex = _cache.GetOrCreate("PuzzleIndex", entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(30); // Optional expiration
            return 0;
        });

        string puzzle = puzzles[puzzleIndex];
        _cache.Set("PuzzleIndex", (puzzleIndex + 1) % puzzles.Count);
        return puzzle;
    }

    public bool CheckAnswer(string puzzle, string answer)
    {
        var tokens = puzzle.Split(' ');
        if (tokens.Length != 3) { return false; }

        // Initialize operands with default values
        int rightOperand = 0;
        int leftOperand;
        bool isValidOperands = int.TryParse(tokens[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out leftOperand) &&
                               int.TryParse(tokens[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out rightOperand);

        if (!isValidOperands)
        {
            return false;
        }

        string operation = tokens[1];
        int? correctAnswer;

        // Safely perform the operation
        switch (operation)
        {
            case "+":
                correctAnswer = leftOperand + rightOperand;
                break;
            case "-":
                correctAnswer = leftOperand - rightOperand;
                break;
            case "*":
                correctAnswer = leftOperand * rightOperand;
                break;
            case "/":
                correctAnswer = rightOperand != 0 ? leftOperand / rightOperand : null;
                break;
            default:
                return false;
        }

        return correctAnswer.HasValue &&
               int.TryParse(answer, NumberStyles.Integer, CultureInfo.InvariantCulture, out int userAnswer) &&
               userAnswer == correctAnswer.Value;
    }

    // public void SeedPuzzles()
    // {
    //     if (!_context.Puzzles.Any())
    //     {
    //         var seedPuzzles = new List<Puzzle>
    //         {
    //             new Puzzle { Id = Guid.NewGuid(), Content = "5 + 3" },
    //             new Puzzle { Id = Guid.NewGuid(), Content = "10 - 7" },
    //             new Puzzle { Id = Guid.NewGuid(), Content = "6 * 2" },
    //             new Puzzle { Id = Guid.NewGuid(), Content = "8 / 2" }
    //         };

    //         _context.Puzzles.AddRange(seedPuzzles);
    //         _context.SaveChanges();
    //         Console.WriteLine($"Seeded {seedPuzzles.Count} puzzles into the database.");
    //     }
    // }
}
