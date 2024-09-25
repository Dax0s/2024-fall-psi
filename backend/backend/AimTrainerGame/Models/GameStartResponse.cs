// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace backend.AimTrainerGame.Models;

public record GameStartResponse(List<DotInfo> dotInfos, int amountOfDots, int timeToLive);
