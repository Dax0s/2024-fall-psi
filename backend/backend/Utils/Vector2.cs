using System.Numerics;
using System.Text.Json.Serialization;

namespace backend.Utils;

public struct Vector2
:
    IAdditionOperators<Vector2, Vector2, Vector2>,
    ISubtractionOperators<Vector2, Vector2, Vector2>
{
    [JsonPropertyName("x")]
    public int X { get; set; }
    [JsonPropertyName("y")]
    public int Y { get; set; }

    public Vector2(int x = 0, int y = 0)
    {
        (X, Y) = (x, y);
    }

    public Vector2(Vector2 other)
    {
        (X, Y) = (other.X, other.Y);
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
