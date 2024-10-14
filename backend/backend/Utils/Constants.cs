namespace backend.Utils;

public static class Constants
{
    public struct AimTrainerGame
    {
        public struct Defaults
        {
            public const int Dots = 15;
            public const int TimeToLive = 1250;

            public struct SpawnTime
            {
                public const int Min = 250;
                public const int Max = 1250;
            }
        }
    }

    public struct DotCountGame
    {
        public struct DotCount
        {
            public const int Min = 1;
            public const int Max = 1000;
        }

        // In pixels
        public const int defaultRadius = 10;
        public const int smallRadius = 5;
        public const int dotCountLimitForDefaultRadius = 100;

        // What part of max. radius should min. radius be
        public const float MinRadiusPercentage = 0.8f;
    }

    public struct ReactionTimeGame
    {
        public struct Wait
        {
            public const int Min = 2000;
            public const int Max = 5000;
        }
    }
}
