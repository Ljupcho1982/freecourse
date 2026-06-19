namespace freecourse.Common.Mediator;

/// <summary>
/// Dispatches a request to its single registered handler.
/// </summary>
public interface IMediator
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}
