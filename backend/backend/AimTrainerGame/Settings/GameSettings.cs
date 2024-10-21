namespace backend.AimTrainerGame.Settings;

public static class GameSettings
{
    public static DifficultySettings GetDifficultySettings(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.Easy => _easySettings,
            Difficulty.Medium => _mediumSettings,
            Difficulty.Hard => _hardSettings,
            _ => _defaultSettings,
        };
    }

    // Might be a good idea to make public in the future
    private static readonly DifficultySettings _easySettings = new(dotCount: 10, timeToLive: 1500, spawnTime: new(500, 1500));
    private static readonly DifficultySettings _mediumSettings = new(dotCount: 15, timeToLive: 1250, spawnTime: new(250, 1250));
    private static readonly DifficultySettings _hardSettings = new(dotCount: 20, timeToLive: 1000, spawnTime: new(0, 1000));
    private static readonly DifficultySettings _defaultSettings = new(dotCount: 15, timeToLive: 1250, spawnTime: new(250, 1250));
}
