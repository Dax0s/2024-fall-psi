using System.Text.Json.Serialization;

namespace backend.AimTrainerGame.Models;

public struct Vector2
{
    [JsonPropertyName("x")]
    public int X { get; set; }
    [JsonPropertyName("y")]
    public int Y { get; set; }

    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector2(Vector2 vector)
    {
        X = vector.X;
        Y = vector.Y;
    }

    public Vector2()
    {
        X = 0;
        Y = 0;
    }
}
