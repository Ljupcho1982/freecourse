namespace freecourse.Features.Weather;

/// <summary>The resolved current weather and multi-day forecast for a city.</summary>
public sealed record WeatherResult(
    string City,
    string? Region,
    string? Country,
    double Latitude,
    double Longitude,
    double TemperatureC,
    double FeelsLikeC,
    double DewPointC,
    int Humidity,
    double WindSpeedKmh,
    double WindGustsKmh,
    int WindDirectionDegrees,
    double PressureHpa,
    int CloudCoverPercent,
    double PrecipitationMm,
    bool IsDay,
    string Description,
    string Icon,
    string Theme,
    DateTimeOffset? Sunrise,
    DateTimeOffset? Sunset,
    IReadOnlyList<DailyForecast> Daily);
