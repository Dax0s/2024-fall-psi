using System.Linq;
using backend.SequenceGame.Services;
using Xunit;

namespace testing.SequenceGame.Services;

public class SequenceGameServiceTest
{
    private readonly SequenceGameService _service = new();

    [Fact]
    public void GetSequence_EmptyInput_ShouldReturnSequenceWithRandomNumber()
    {
        _service.ParseAndValidateSequence("");
        var result = _service.ExtendSequence();

        Assert.Single(result);
        Assert.InRange(result.First(), 1, 9);
    }

    [Fact]
    public void GetSequence_ValidSequence_ShouldReturnSequenceWithRandomNumber()
    {
        const string validSequence = "1,2,3";

        _service.ParseAndValidateSequence(validSequence);
        var result = _service.ExtendSequence();

        Assert.True(result.Count > 3);
        Assert.InRange(result.Last(), 1, 9);
    }
}
