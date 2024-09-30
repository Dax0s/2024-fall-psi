using System.Text.Json.Serialization;

namespace backend.Utils;

public struct Vector2
{
    [JsonPropertyName("x")]
    public int X { get; set; }
    [JsonPropertyName("y")]
    public int Y { get; set; }

    public Vector2(int x = 0, int y = 0)
    {
        X = x;
        Y = y;
    }

    public Vector2(Vector2 vector)
    {
        X = vector.X;
        Y = vector.Y;
    }

    public int SquaredDistance(Vector2 other)
    {
        int dx = this.X - other.X;
        int dy = this.Y - other.Y;
        return dx * dx + dy * dy;
    }
}
