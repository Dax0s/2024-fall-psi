using backend.AimTrainerGame.Data;
<<<<<<< HEAD
using backend.AimTrainerGame.Models;
=======
>>>>>>> 1b43db0 (Rename Highscores table and move data files to data directory)
using backend.AimTrainerGame.Settings;

namespace backend.AimTrainerGame.Services;

public interface IAimTrainerGameService
{
    public (List<DotInfo>, DifficultySettings) StartGame(GameStartRequest gameInfo);
    public Highscore EndGame(GameEndRequest gameInfo);
    public IEnumerable<Highscore> GetHighscores(int amount);
}
