using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace freecourse.Common.Mediator;

/// <summary>
/// DI registration for the custom mediator and its request handlers.
/// </summary>
public static class MediatorServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="IMediator"/> and scans <paramref name="assembly"/> for every
    /// <see cref="IRequestHandler{TRequest,TResponse}"/> implementation, registering each
    /// against its closed handler interface.
    /// </summary>
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddScoped<IMediator, Mediator>();

        var openHandler = typeof(IRequestHandler<,>);

        foreach (var type in assembly.GetTypes().Where(t => t is { IsAbstract: false, IsInterface: false }))
        {
            var handlerInterfaces = type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == openHandler);

            foreach (var handlerInterface in handlerInterfaces)
            {
                services.AddScoped(handlerInterface, type);
            }
        }

        return services;
    }
}
