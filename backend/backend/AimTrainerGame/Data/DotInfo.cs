using System.Text.Json.Serialization;
using backend.Utils;

namespace backend.AimTrainerGame.Data;

public struct DotInfo
{
    [JsonPropertyName("pos")]
    public Vec2<int> Pos { get; set; }

    [JsonPropertyName("spawnTime")]
    public int SpawnTime { get; set; }

    public DotInfo(Vec2<int> pos, int spawnTime)
    {
        Pos = pos;
        SpawnTime = spawnTime;
    }
}

public static partial class RandomExtensions
{
    public static DotInfo NextDotInfo(this Random random, Vec2<int> screenSize, Bounds<int> spawnTime)
        => new DotInfo(random.NextOffset(screenSize), random.NextWithinBounds(spawnTime));
}
