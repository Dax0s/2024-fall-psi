namespace backend.SequenceGame.Services;

public class SequenceGameService : ISequenceGameService
{
    private List<int> Sequence { get; set; } = [];

    public bool ParseAndValidateSequence(string sequence)
    {
        if (!string.IsNullOrEmpty(sequence))
        {
            var parts = sequence.Split(',');
            var isValid = parts.All(part => int.TryParse(part, out var number) && number is >= 1 and <= 9);

            if (isValid)
            {
                Sequence = parts.Select(int.Parse).ToList();
            }

            return isValid;
        }

        return true;
    }

    public List<int> ExtendSequence()
    {
        Sequence.Add(new Random().Next(minValue: 1, maxValue: 10));

        return Sequence;
    }
}
