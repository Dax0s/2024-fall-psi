namespace backend.SequenceGame.Services;

public class SequenceGameService : ISequenceGameService
{
    private List<int> NextSequence { get; set; } = [];

    public List<int> GetSequence(string sequence = "")
    {
        if (!string.IsNullOrEmpty(sequence))
        {
            NextSequence = sequence.Split(',').Select(int.Parse).ToList();
        }
        NextSequence.Add(new Random().Next(minValue: 1, maxValue: 10));

        return NextSequence;
    }
}
