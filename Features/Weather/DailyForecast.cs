namespace freecourse.Features.Weather;

/// <summary>A single day's forecast in the <see cref="WeatherResult"/>.</summary>
public sealed record DailyForecast(
    DateOnly Date,
    double MaxTemperatureC,
    double MinTemperatureC,
    double PrecipitationSumMm,
    int PrecipitationProbabilityPercent,
    double UvIndexMax,
    string Description,
    string Icon);
