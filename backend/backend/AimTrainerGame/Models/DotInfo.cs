using System.Text.Json.Serialization;
using backend.Utils;

namespace backend.AimTrainerGame.Models;

public struct DotInfo
{
    [JsonPropertyName("pos")]
    public Vector2 Pos { get; set; }

    [JsonPropertyName("spawnTime")]
    public int SpawnTime { get; set; }

    public DotInfo(Vector2 pos, int spawnTime)
    {
        Pos = pos;
        SpawnTime = spawnTime;
    }
}
