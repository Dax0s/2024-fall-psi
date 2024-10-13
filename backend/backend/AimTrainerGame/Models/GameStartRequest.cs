using backend.AimTrainerGame.Utils;
using backend.Utils;

namespace backend.AimTrainerGame.Models;

public record GameStartRequest(Difficulty difficulty, Vector2 screenSize);
