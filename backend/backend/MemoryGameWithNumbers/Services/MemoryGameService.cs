namespace backend.MemoryGameWithNumbers.Services;

public class MemoryGameService
{
    private List<int?>? _correctSequence;
    private bool _isGameStarted;

    public List<int?> StartGame(int maxNumber)
    {
        Random rand = new Random();

        _isGameStarted = true;
        _correctSequence = Enumerable.Range(1, maxNumber)
                                     .Select(n => (int?)n)
                                     .ToList();

        List<int?> gridNumbers = new List<int?>(_correctSequence.OrderBy(x => rand.Next()));

        // Dynamically adjust the grid size based on the number of correct sequence numbers
        int gridSize = Math.Max(16, _correctSequence.Count + 4);

        while (gridNumbers.Count < gridSize)
        {
            gridNumbers.Add(null);
        }

        return gridNumbers.OrderBy(x => rand.Next()).ToList();
    }

    public bool CheckAttempt(List<int?> userAttempt)
    {
        if (_correctSequence == null)
        {
            return false;
        }

        for (int i = 0; i < _correctSequence.Count; i++)
        {
            if (i >= userAttempt.Count || _correctSequence[i] == null || userAttempt[i] == null || _correctSequence[i] != userAttempt[i])
            {
                return false;
            }
        }

        return true;
    }

    public bool IsGameStarted()
    {
        return _isGameStarted;
    }

    public void ResetGame()
    {
        _isGameStarted = false;
        _correctSequence = null;
    }

}
