using System.Text.Json.Serialization;

namespace freecourse.Features.Weather.OpenMeteo;

/// <summary>Daily-forecast block of the Open-Meteo forecast response (parallel arrays).</summary>
public sealed class DailyWeatherDto
{
    [JsonPropertyName("time")]
    public IReadOnlyList<string> Time { get; init; } = [];

    [JsonPropertyName("weather_code")]
    public IReadOnlyList<int> WeatherCode { get; init; } = [];

    [JsonPropertyName("temperature_2m_max")]
    public IReadOnlyList<double> TemperatureMax { get; init; } = [];

    [JsonPropertyName("temperature_2m_min")]
    public IReadOnlyList<double> TemperatureMin { get; init; } = [];

    [JsonPropertyName("precipitation_sum")]
    public IReadOnlyList<double> PrecipitationSum { get; init; } = [];

    [JsonPropertyName("precipitation_probability_max")]
    public IReadOnlyList<int> PrecipitationProbabilityMax { get; init; } = [];

    [JsonPropertyName("uv_index_max")]
    public IReadOnlyList<double> UvIndexMax { get; init; } = [];

    [JsonPropertyName("sunrise")]
    public IReadOnlyList<string> Sunrise { get; init; } = [];

    [JsonPropertyName("sunset")]
    public IReadOnlyList<string> Sunset { get; init; } = [];
}
