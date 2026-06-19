using System.Text.Json.Serialization;

namespace freecourse.Features.Weather.OpenMeteo;

/// <summary>Open-Meteo forecast API response.</summary>
public sealed class ForecastResponse
{
    [JsonPropertyName("current")]
    public CurrentWeatherDto? Current { get; init; }

    [JsonPropertyName("daily")]
    public DailyWeatherDto? Daily { get; init; }
}
