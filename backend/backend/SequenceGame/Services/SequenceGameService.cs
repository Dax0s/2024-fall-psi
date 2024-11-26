namespace backend.SequenceGame.Services;

public class SequenceGameService : ISequenceGameService
{
    private List<int> NextSequence { get; set; } = [];

    public List<int> GetSequence(string sequence = "")
    {
        var random = new Random();

        if (!string.IsNullOrEmpty(sequence))
        {
            NextSequence = sequence.Split(',').Select(int.Parse).ToList();
        }
        NextSequence.Add(random.Next(minValue: 1, maxValue: 10));

        return NextSequence;
    }
}
