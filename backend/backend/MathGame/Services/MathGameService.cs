using System.Globalization;
using backend.MathGame.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace backend.MathGame.Services;

public class MathGameService
{
    private readonly GamesDbContext _context;
    private readonly IMemoryCache _cache;
    private const string LogFilePath = "logs/MathGameService.log";

    public MathGameService(GamesDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
        LoadPuzzlesAsync().Wait(); // Ensure puzzles are loaded asynchronously.
    }

    private async Task LoadPuzzlesAsync()
    {
        try
        {
            if (!_cache.TryGetValue("Puzzles", out List<string>? puzzles) || puzzles == null)
            {
                puzzles = await _context.Puzzles
                    .Where(p => p.Content != null)
                    .Select(p => p.Content!)
                    .ToListAsync()
                    .ConfigureAwait(false);

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

    public async Task<string?> GetNextPuzzleAsync()
    {
        var puzzles = _cache.Get<List<string>>("Puzzles");
        if (puzzles == null || puzzles.Count == 0)
        {
            return null;
        }

        int puzzleIndex = _cache.GetOrCreate("PuzzleIndex", entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(30);
            return 0;
        });

        string puzzle = puzzles[puzzleIndex];
        _cache.Set("PuzzleIndex", (puzzleIndex + 1) % puzzles.Count);
        await Task.CompletedTask.ConfigureAwait(false);
        return puzzle;
    }

    public async Task<bool> CheckAnswerAsync(string puzzle, string answer)
    {
        var tokens = puzzle.Split(' ');
        if (tokens.Length != 3) { return false; }

        int leftOperand;
        bool isValidOperand1 = int.TryParse(tokens[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out leftOperand);

        int rightOperand;
        bool isValidOperand2 = int.TryParse(tokens[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out rightOperand);

        if (!isValidOperand1 && !isValidOperand2)
        {
            return false;
        }

        string operation = tokens[1];
        int? correctAnswer;

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

        await Task.CompletedTask.ConfigureAwait(false);
        return correctAnswer.HasValue &&
               int.TryParse(answer, NumberStyles.Integer, CultureInfo.InvariantCulture, out int userAnswer) &&
               userAnswer == correctAnswer.Value;
    }
}
