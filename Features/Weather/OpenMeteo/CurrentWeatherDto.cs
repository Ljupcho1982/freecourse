using System.Text.Json.Serialization;

namespace freecourse.Features.Weather.OpenMeteo;

/// <summary>Current-conditions block of the Open-Meteo forecast response.</summary>
public sealed class CurrentWeatherDto
{
    [JsonPropertyName("temperature_2m")]
    public double Temperature { get; init; }

    [JsonPropertyName("apparent_temperature")]
    public double ApparentTemperature { get; init; }

    [JsonPropertyName("relative_humidity_2m")]
    public int RelativeHumidity { get; init; }

    [JsonPropertyName("dew_point_2m")]
    public double DewPoint { get; init; }

    [JsonPropertyName("wind_speed_10m")]
    public double WindSpeed { get; init; }

    [JsonPropertyName("wind_gusts_10m")]
    public double WindGusts { get; init; }

    [JsonPropertyName("wind_direction_10m")]
    public int WindDirection { get; init; }

    [JsonPropertyName("surface_pressure")]
    public double Pressure { get; init; }

    [JsonPropertyName("cloud_cover")]
    public int CloudCover { get; init; }

    [JsonPropertyName("precipitation")]
    public double Precipitation { get; init; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; init; }

    [JsonPropertyName("weather_code")]
    public int WeatherCode { get; init; }
}
