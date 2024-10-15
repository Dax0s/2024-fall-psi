using backend.Properties;
using backend.Utils;

namespace backend.AimTrainerGame.Models;

public record GameStartRequest(Settings.AimTrainerGame.Difficulty difficulty, Vector2 screenSize);
