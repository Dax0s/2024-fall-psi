using backend.Utils;

namespace backend.Properties;

// May be a good idea to separate into different files located in some subdirectory
public static class Settings
{
    // TODO: More elegant, generic, less clunky aim trainer settings structure
    public struct AimTrainerGame
    {
        public struct Defaults
        {
            public const int Dots = 15;
            public const int TimeToLive = 1250;
            public static readonly IntBounds SpawnTime = new(250, 1250);
        }

        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }

        public struct Easy
        {
            public const int Dots = 10;
            public const int TimeToLive = 1500;
            public static readonly IntBounds SpawnTime = new(500, 1500);
        }

        public struct Medium
        {
            public const int Dots = 15;
            public const int TimeToLive = 1250;
            public static readonly IntBounds SpawnTime = new(250, 1250);
        }

        public struct Hard
        {
            public const int Dots = 20;
            public const int TimeToLive = 1000;
            public static readonly IntBounds SpawnTime = new(0, 1000);
        }
    }

    public struct DotCountGame
    {
        public static readonly IntBounds DotCount = new(1, 1000);

        // In pixels
        public const int DefaultRadius = 10;
        public const int SmallRadius = 5;
        public const int DotCountLimitForDefaultRadius = 100;

        // What part of max. radius should min. radius be
        public const float MinRadiusPercentage = 0.8f;
    }

    public struct ReactionTimeGame
    {
        public static readonly IntBounds Wait = new(2000, 5000);
    }
}
