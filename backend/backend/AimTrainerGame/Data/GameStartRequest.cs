using backend.AimTrainerGame.Settings;
using backend.Utils;

namespace backend.AimTrainerGame.Data;

public record GameStartRequest(Difficulty difficulty, Vec2<int> screenSize);
