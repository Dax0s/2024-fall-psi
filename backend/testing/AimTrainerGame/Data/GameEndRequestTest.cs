using backend.AimTrainerGame.Data;
using Xunit;

namespace testing.AimTrainerGame.Data;

public class GameEndRequestTests
{
    [Fact]
    public void Constructor_SetsProperties_Correctly()
    {
        var request = new GameEndRequest("Username", 10);

        Assert.Equal("Username", request.Username);
        Assert.Equal(10, request.Score);
    }

    [Fact]
    public void EqualRequests_ShouldBeEqual()
    {
        var request1 = new GameEndRequest("Username", 10);
        var request2 = new GameEndRequest("Username", 10);

        Assert.Equal(request1, request2);
    }

    [Fact]
    public void DifferentRequests_ShouldNotBeEqual()
    {
        var request1 = new GameEndRequest("Username1", 10);
        var request2 = new GameEndRequest("Username2", 10);

        Assert.NotEqual(request1, request2);
    }
}
