namespace backend.SequenceGame.Services;

public interface ISequenceGameService
{
    public bool ParseAndValidateSequence(string sequence = "");
    public List<int> ExtendSequence();
}
