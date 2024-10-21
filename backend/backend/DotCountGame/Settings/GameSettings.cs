using backend.Utils;

namespace backend.DotCountGame.Settings;

public static class GameSettings
{
    public static readonly IntBounds DotCount = new(1, 1000);

    // In pixels
    public static readonly int DefaultRadius = 10;
    public static readonly int SmallRadius = 5;
    public static readonly int DotCountLimitForDefaultRadius = 100;

    // What part of max. radius should min. radius be
    public static readonly float MinRadiusPercentage = 0.8f;
}
