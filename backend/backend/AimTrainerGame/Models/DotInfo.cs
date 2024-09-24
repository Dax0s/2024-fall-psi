// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace backend.AimTrainerGame.Models;

public struct DotInfo
{
    [JsonPropertyName("pos")]
    public Vector2 Pos { get; set; }

    [JsonPropertyName("spawnTime")]
    public int SpawnTime { get; set; }

    public DotInfo(Vector2 pos, int spawnTime)
    {
        Pos = pos;
        SpawnTime = spawnTime;
    }
}
