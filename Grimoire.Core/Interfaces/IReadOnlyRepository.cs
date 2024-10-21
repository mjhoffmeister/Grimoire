using FluentResults;

namespace Grimoire.Core.Interfaces;

/// <summary>
/// Async read/write repository.
/// </summary>
/// <typeparam name="TId">Id type.</typeparam>
/// <typeparam name="TEntity">Entity type.</typeparam>
public interface IReadOnlyRepository<TId, TEntity> where TEntity : class
{
    /// <summary>
    /// Tries to find entities by a filter.
    /// </summary>
    /// <param name="filter">Filter.</param>
    /// <returns>The result of the attempt.</returns>
    Task<Result<IEnumerable<TEntity>>> TryFindAsync(string filter);

    /// <summary>
    /// Tries to get an entity by its id.
    /// </summary>
    /// <param name="id">The id of the entity to get.</param>
    /// <returns>The result of the attempt.</returns>
    Task<Result<TEntity>> TryGetByIdAsync(TId id);

    /// <summary>
    /// Tries to list all entities.
    /// </summary>
    /// <returns>The result of the attempt.</returns>
    Task<Result<IEnumerable<TEntity>>> TryListAsync();
}
