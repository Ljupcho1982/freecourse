using System.Text.Json.Serialization;

namespace freecourse.Features.Weather.OpenMeteo;

/// <summary>Open-Meteo geocoding API response.</summary>
public sealed class GeocodingResponse
{
    [JsonPropertyName("results")]
    public IReadOnlyList<GeocodingLocation>? Results { get; init; }
}
