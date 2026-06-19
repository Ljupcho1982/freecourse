namespace freecourse.Features.Weather;

/// <summary>
/// Human-readable description and an icon for a WMO weather-interpretation code,
/// plus a theme key used to drive the page's visual styling.
/// </summary>
public sealed record WeatherCondition(string Description, string Icon, string Theme)
{
    /// <summary>Maps a WMO weather code (optionally day/night aware) to a condition.</summary>
    public static WeatherCondition FromCode(int code, bool isDay = true) => code switch
    {
        0 => new("Clear sky", isDay ? "☀️" : "🌙", "clear"),
        1 => new("Mainly clear", isDay ? "🌤️" : "🌙", "clear"),
        2 => new("Partly cloudy", isDay ? "⛅" : "☁️", "clouds"),
        3 => new("Overcast", "☁️", "clouds"),
        45 or 48 => new("Fog", "🌫️", "clouds"),
        51 or 53 or 55 => new("Drizzle", "🌦️", "rain"),
        56 or 57 => new("Freezing drizzle", "🌧️", "rain"),
        61 or 63 or 65 => new("Rain", "🌧️", "rain"),
        66 or 67 => new("Freezing rain", "🌧️", "rain"),
        71 or 73 or 75 => new("Snow", "🌨️", "snow"),
        77 => new("Snow grains", "🌨️", "snow"),
        80 or 81 or 82 => new("Rain showers", "🌦️", "rain"),
        85 or 86 => new("Snow showers", "🌨️", "snow"),
        95 => new("Thunderstorm", "⛈️", "storm"),
        96 or 99 => new("Thunderstorm with hail", "⛈️", "storm"),
        _ => new("Unknown", "🌡️", "clouds"),
    };
}
