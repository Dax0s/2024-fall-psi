using Microsoft.AspNetCore.Mvc;

namespace backend.SequenceGame.Services;

public interface ISequenceGameService
{
    public List<int> GetSequence(string sequence = "");
}