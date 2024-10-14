using backend.Utils;

namespace backend.AimTrainerGame.Utils;

public static class RandomExtensions
{
    public static Vector2 NextVector2FromScreenSize(this Random random, Vector2 screenSize)
    {
        return new Vector2(random.Next(screenSize.X), random.Next(screenSize.Y));
    }
}
