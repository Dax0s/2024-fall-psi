namespace backend.Utils;

public static class RandomExtensions
{
    public static Vector2 NextOffset(this Random random, Vector2 offsetBounds)
    {
        return new Vector2(random.Next(offsetBounds.X), random.Next(offsetBounds.Y));
    }
}
