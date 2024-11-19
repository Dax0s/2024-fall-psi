namespace backend.MemoryGameWithNumbers.Services;

public class MemoryGameService
{
    private List<int?>? _correctSequence;

    public List<int?> StartGame(int maxNumber)
    {
        Random rand = new Random();

        // Create the correct sequence in order
        _correctSequence = Enumerable.Range(1, maxNumber)
                                     .Select(n => (int?)n)
                                     .ToList();

        // Create a shuffled grid with nulls up to 16 items
        List<int?> gridNumbers = new List<int?>(_correctSequence.OrderBy(x => rand.Next()));

        while (gridNumbers.Count < 16)
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
        return _correctSequence != null;
    }

}
