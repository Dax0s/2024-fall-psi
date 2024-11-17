using backend.AimTrainerGame.Data;
using backend.AimTrainerGame.Models;
using backend.AimTrainerGame.Settings;
using backend.Utils;

namespace backend.AimTrainerGame.Services;

public class AimTrainerGameService : IAimTrainerGameService
{
    private readonly GamesDbContext _db;

    public AimTrainerGameService(GamesDbContext db)
    {
        _db = db;
    }

    public (List<DotInfo>, DifficultySettings) StartGame(GameStartRequest gameInfo)
    {
        var difficultySettings = GameSettings.GetDifficultySettings(gameInfo.difficulty);

        var random = new Random();
        var dotInfoList = Enumerable
            .Range(0, difficultySettings.dotCount)
            .Select(_ => NextDotInfo(random, gameInfo.screenSize, difficultySettings.spawnTime))
            .ToList();

        return (dotInfoList, difficultySettings);
    }

    public Highscore EndGame(GameEndRequest gameInfo)
    {
        var hs = new Highscore
        {
            Id = Guid.NewGuid(),
            Username = gameInfo.Username,
            Score = gameInfo.Score,
            Date = DateTime.UtcNow
        };
        _db.Add(hs);
        _db.SaveChanges();

        return hs;
    }

    public IEnumerable<Highscore> GetHighscores(int amount)
    {
        return _db.AimTrainerGameHighscores
            .OrderByDescending(h => h.Score)
            .ThenBy(h => h.Date)
            .Take(amount);
    }

    private static DotInfo NextDotInfo(Random random, Vec2<int> screenSize, Bounds<int> spawnTime)
    {
        var randomSpawnTime = random.NextWithinBounds(spawnTime);

        if (screenSize.X < 0 || screenSize.Y < 0)
        {
            return new DotInfo(new Vec2<int>(0, 0), randomSpawnTime);
        }

        var randomPosition = random.NextOffset(new Vec2<uint>((uint)screenSize.X, (uint)screenSize.Y));
        return new DotInfo(new Vec2<int>((int)randomPosition.X, (int)randomPosition.Y), randomSpawnTime);
    }
}
