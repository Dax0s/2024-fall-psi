namespace backend.AimTrainerGame.Models;

public record GameStartResponse(List<DotInfo> dotInfos, int amountOfDots, int timeToLive);
