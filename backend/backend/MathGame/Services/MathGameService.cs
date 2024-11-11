using System.Globalization;

namespace backend.MathGame;

public class MathGameService
{
    private readonly List<string> _puzzles = new List<string>();
    private int _puzzleIndex;
    private readonly string _puzzleFilePath = Path.Combine("data", "puzzles.txt");

    public MathGameService()
    {
        LoadPuzzlesFromFile();
    }

    public string? GetNextPuzzle()
    {
        if (_puzzles.Count == 0)
        {
            return null;
        }

        if (_puzzleIndex >= _puzzles.Count)
        {
            _puzzleIndex = 0;
        }

        return _puzzles[_puzzleIndex++];
    }

    public bool CheckAnswer(string puzzle, string answer)
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
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    private void LoadPuzzlesFromFile()
    {
        try
        {
            if (!File.Exists(_puzzleFilePath))
            {
                Console.WriteLine($"Puzzle file not found at {_puzzleFilePath}");
                return;
            }

            using var fileStream = new FileStream(_puzzleFilePath, FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(fileStream);
            string? line;
            _puzzles.Clear();

            while ((line = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    _puzzles.Add(line.Trim());
                }
            }

            if (_puzzles.Count == 0)
            {
                Console.WriteLine("The puzzle file is empty.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading puzzle file: {ex.Message}");
        }
    }
}
