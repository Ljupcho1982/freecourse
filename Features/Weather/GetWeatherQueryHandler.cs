using System.Globalization;
using System.Net.Http.Json;
using freecourse.Common.Mediator;
using freecourse.Features.Weather.OpenMeteo;

namespace freecourse.Features.Weather;

/// <summary>
/// Resolves a city name to coordinates via the Open-Meteo geocoding API, then fetches
/// current conditions and a 7-day forecast from the Open-Meteo forecast API.
/// </summary>
public sealed class GetWeatherQueryHandler(IHttpClientFactory httpClientFactory)
    : IRequestHandler<GetWeatherQuery, WeatherResult>
{
    public const string HttpClientName = "open-meteo";

    public async Task<WeatherResult> Handle(GetWeatherQuery request, CancellationToken cancellationToken)
    {
        var city = request.City?.Trim();
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ArgumentException("City must not be empty.", nameof(request));
        }

        var client = httpClientFactory.CreateClient(HttpClientName);
        var location = await GeocodeAsync(client, city, cancellationToken);
        return await FetchForecastAsync(client, location, cancellationToken);
    }

    private static async Task<GeocodingLocation> GeocodeAsync(
        HttpClient client, string city, CancellationToken cancellationToken)
    {
        var url = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(city)}&count=1&language=en&format=json";

        var response = await client.GetFromJsonAsync<GeocodingResponse>(url, cancellationToken);
        var match = response?.Results?.FirstOrDefault();

        return match ?? throw new CityNotFoundException(city);
    }

    private static async Task<WeatherResult> FetchForecastAsync(
        HttpClient client, GeocodingLocation location, CancellationToken cancellationToken)
    {
        var lat = location.Latitude.ToString(CultureInfo.InvariantCulture);
        var lon = location.Longitude.ToString(CultureInfo.InvariantCulture);

        var url =
            $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}" +
            "&current=temperature_2m,relative_humidity_2m,apparent_temperature,dew_point_2m," +
            "is_day,weather_code,wind_speed_10m,wind_gusts_10m,wind_direction_10m," +
            "surface_pressure,cloud_cover,precipitation" +
            "&daily=weather_code,temperature_2m_max,temperature_2m_min," +
            "precipitation_sum,precipitation_probability_max,uv_index_max,sunrise,sunset" +
            "&timezone=auto&forecast_days=7";

        var forecast = await client.GetFromJsonAsync<ForecastResponse>(url, cancellationToken);

        if (forecast?.Current is not { } current)
        {
            throw new InvalidOperationException("The weather service returned no current conditions.");
        }

        var isDay = current.IsDay == 1;
        var condition = WeatherCondition.FromCode(current.WeatherCode, isDay);
        var (sunrise, sunset) = FirstSunriseSunset(forecast.Daily);

        return new WeatherResult(
            City: location.Name,
            Region: location.Admin1,
            Country: location.Country,
            Latitude: location.Latitude,
            Longitude: location.Longitude,
            TemperatureC: current.Temperature,
            FeelsLikeC: current.ApparentTemperature,
            DewPointC: current.DewPoint,
            Humidity: current.RelativeHumidity,
            WindSpeedKmh: current.WindSpeed,
            WindGustsKmh: current.WindGusts,
            WindDirectionDegrees: current.WindDirection,
            PressureHpa: current.Pressure,
            CloudCoverPercent: current.CloudCover,
            PrecipitationMm: current.Precipitation,
            IsDay: isDay,
            Description: condition.Description,
            Icon: condition.Icon,
            Theme: condition.Theme,
            Sunrise: sunrise,
            Sunset: sunset,
            Daily: BuildDaily(forecast.Daily));
    }

    private static (DateTimeOffset? Sunrise, DateTimeOffset? Sunset) FirstSunriseSunset(DailyWeatherDto? daily)
    {
        if (daily is null || daily.Sunrise.Count == 0 || daily.Sunset.Count == 0)
        {
            return (null, null);
        }

        DateTimeOffset.TryParse(daily.Sunrise[0], CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeLocal, out var sunrise);
        DateTimeOffset.TryParse(daily.Sunset[0], CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeLocal, out var sunset);
        return (sunrise, sunset);
    }

    private static IReadOnlyList<DailyForecast> BuildDaily(DailyWeatherDto? daily)
    {
        if (daily is null)
        {
            return [];
        }

        var days = new List<DailyForecast>(daily.Time.Count);
        for (var i = 0; i < daily.Time.Count; i++)
        {
            var condition = WeatherCondition.FromCode(daily.WeatherCode[i]);
            days.Add(new DailyForecast(
                Date: DateOnly.Parse(daily.Time[i], CultureInfo.InvariantCulture),
                MaxTemperatureC: daily.TemperatureMax[i],
                MinTemperatureC: daily.TemperatureMin[i],
                PrecipitationSumMm: ValueAt(daily.PrecipitationSum, i),
                PrecipitationProbabilityPercent: ValueAt(daily.PrecipitationProbabilityMax, i),
                UvIndexMax: ValueAt(daily.UvIndexMax, i),
                Description: condition.Description,
                Icon: condition.Icon));
        }

        return days;
    }

    private static double ValueAt(IReadOnlyList<double> values, int index) =>
        index < values.Count ? values[index] : 0;

    private static int ValueAt(IReadOnlyList<int> values, int index) =>
        index < values.Count ? values[index] : 0;
}
