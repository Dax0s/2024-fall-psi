using backend.ReactionTimeGame.Data;
using Xunit;

namespace testing.ReactionTimeGame.Data;

public class WaitDurationTests
{
    [Fact]
    public void Construction()
    {
        var msToWait = 4321;
        var waitDuration = new WaitDuration(msToWait);

        Assert.Equal(msToWait, waitDuration.MillisecondsToWait);
    }
}
