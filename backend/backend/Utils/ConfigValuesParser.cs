// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace backend.Utils;

public class ConfigValuesParser
{
    public static int GetConfigIntValue(IConfiguration configuration, string key, int defaultValue)
    {
        string? value = configuration.GetValue<string>(key);

        if (string.IsNullOrEmpty(value))
        {
            return defaultValue;
        }

        int.TryParse(value, System.Globalization.NumberStyles.Integer,
            System.Globalization.CultureInfo.InvariantCulture, out var result);
        return result;
    }
}
