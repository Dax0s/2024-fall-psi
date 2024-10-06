using System.Text.Json.Serialization;

namespace backend.Utils;

public struct Vector2(int x = 0, int y = 0)
{
    [JsonPropertyName("x")]
    public int X { get; set; } = x;
    [JsonPropertyName("y")]
    public int Y { get; set; } = y;

    public Vector2(Vector2 vector) : this(vector.X, vector.Y)
    {
    }
}
