using Grimoire.Core.Domain.SpellAggregate;
using Grimoire.Core.Interfaces;
using Grimoire.Infrastructure.Fakes;
using Microsoft.Extensions.DependencyInjection;

namespace Grimoire.Bootstrapper;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Grimoire services.
    /// </summary>
    /// <param name="services">Services.</param>
    /// <returns><see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddGrimoireServices(this IServiceCollection services)
    {
        services.AddScoped<IReadOnlyRepository<string, Spell>, FakeSpellRepository>();

        return services;
    }
}
