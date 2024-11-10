using backend.Utils;

namespace backend.AimTrainerGame.Settings;

public record DifficultySettings(int dotCount, int timeToLive, Bounds<int> spawnTime);
