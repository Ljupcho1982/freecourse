namespace freecourse.Common.Mediator;

/// <summary>
/// Marker for a request (command or query) that, when sent through the
/// mediator, produces a response of type <typeparamref name="TResponse"/>.
/// </summary>
public interface IRequest<out TResponse>;
