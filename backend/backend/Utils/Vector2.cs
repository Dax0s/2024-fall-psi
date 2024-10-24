using System.Numerics;
using System.Text.Json.Serialization;

namespace backend.Utils;

public struct Vector2(int x = 0, int y = 0)
:
    IAdditionOperators<Vector2, Vector2, Vector2>,
    ISubtractionOperators<Vector2, Vector2, Vector2>
{
    [JsonPropertyName("x")]
    public int X { get; set; } = x;
    [JsonPropertyName("y")]
    public int Y { get; set; } = y;

    public Vector2(Vector2 other) : this(other.X, other.Y)
    {
    }

    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(x: a.X + b.X, y: a.Y + b.Y);
    }
    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return new Vector2(x: a.X - b.X, y: a.Y - b.Y);
    }
}
