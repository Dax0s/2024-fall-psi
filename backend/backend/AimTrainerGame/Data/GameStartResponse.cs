namespace backend.AimTrainerGame.Data;

public record GameStartResponse(List<DotInfo> dotInfos, int dotCount, int timeToLive);
