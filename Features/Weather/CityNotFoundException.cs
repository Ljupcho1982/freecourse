namespace freecourse.Features.Weather;

/// <summary>Thrown when the geocoding lookup returns no match for a city name.</summary>
public sealed class CityNotFoundException(string city)
    : Exception($"Could not find a location named \"{city}\".")
{
    public string City { get; } = city;
}
