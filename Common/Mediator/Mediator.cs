using Microsoft.Extensions.DependencyInjection;

namespace freecourse.Common.Mediator;

/// <summary>
/// Default <see cref="IMediator"/> implementation. Resolves the
/// <see cref="IRequestHandler{TRequest,TResponse}"/> for the runtime type of the
/// request from the DI container and invokes it.
/// </summary>
public sealed class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic handler = serviceProvider.GetRequiredService(handlerType);

        return handler.Handle((dynamic)request, cancellationToken);
    }
}
