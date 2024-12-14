using Xunit;

using backend.ReactionTimeGame.Data;

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
