using freecourse.Common.Mediator;

namespace freecourse.Features.Weather;

/// <summary>Query for the current weather and forecast of a named city.</summary>
public sealed record GetWeatherQuery(string City) : IRequest<WeatherResult>;
