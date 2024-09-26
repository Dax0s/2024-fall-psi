// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace backend.AimTrainerGame.Models;

public struct Vector2
{
    [JsonPropertyName("x")]
    public int X { get; set; }
    [JsonPropertyName("y")]
    public int Y { get; set; }

    public Vector2(int x = 0, int y = 0)
    {
        X = x;
        Y = y;
    }

    public Vector2(Vector2 vector)
    {
        X = vector.X;
        Y = vector.Y;
    }
}
