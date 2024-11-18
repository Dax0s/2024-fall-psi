using backend.Utils;
using Xunit;

namespace testing.Utils;

public class ScoreCreationInfoTests
{
    [Fact]
    public void Construction()
    {
        var (username, value) = ("User", 10);
        var scoreCreationInfo = new ScoreCreationInfo
        {
            Username = username,
            Value = value,
        };

        Assert.Equal((username, value), (scoreCreationInfo.Username, scoreCreationInfo.Value));
    }
}
