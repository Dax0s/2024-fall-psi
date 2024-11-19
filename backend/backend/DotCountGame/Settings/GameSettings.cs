using backend.Utils;

namespace backend.DotCountGame.Settings;

public static class GameSettings
{
    public static readonly Bounds<int> DotCount = new(1, 1000);

    // In pixels
    public static readonly int DefaultRadius = 10;
    public static readonly int SmallRadius = 5;
    public static readonly int DotCountLimitForDefaultRadius = 100;

    // What part of max. radius should min. radius be
    public static readonly float MinRadiusPercentage = 0.8f;

    public static Bounds<int> GetRadiusBounds(int dotCount)
    {
        var maxRadius = dotCount < DotCountLimitForDefaultRadius
            ? DefaultRadius
            : SmallRadius;

        var minRadius = Math.Max(
                1,
                (int)(MinRadiusPercentage * maxRadius)
            ); // So that _minRadius > 0

        return new Bounds<int>(minRadius, maxRadius);
    }
}
