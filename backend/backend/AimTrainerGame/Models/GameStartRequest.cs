using backend.AimTrainerGame.Settings;
using backend.Utils;

namespace backend.AimTrainerGame.Models;

public record GameStartRequest(Difficulty difficulty, Vec2<int> screenSize);
