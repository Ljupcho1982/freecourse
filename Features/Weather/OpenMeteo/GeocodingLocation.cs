using System.Text.Json.Serialization;

namespace freecourse.Features.Weather.OpenMeteo;

/// <summary>A single matched location from the Open-Meteo geocoding API.</summary>
public sealed class GeocodingLocation
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("admin1")]
    public string? Admin1 { get; init; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; init; }
}
