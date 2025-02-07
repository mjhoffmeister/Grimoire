using FluentResults;
using Grimoire.Core.Domain.SpellAggregate;
using Grimoire.Core.Interfaces;

namespace Grimoire.Core.UseCases.GetSpells;

/// <summary>
/// Get Spells interactor.
/// </summary>
/// <typeparam name="TOutput">Output.</typeparam>
public class GetSpellsInteractor<TOutput>(
    IReadOnlyRepository<string, Spell> spellRepository,
    IGetSpellsBoundary<TOutput> boundary)
    : IUseCaseInteractor<GetSpellsRequest, TOutput>
{
    // Spell repository
    private readonly IReadOnlyRepository<string, Spell> _spellRepository = spellRepository;
    
    // Boundary
    private readonly IGetSpellsBoundary<TOutput> _boundary = boundary;

    /// <inheritdoc />
    public async Task<TOutput> HandleAsync(GetSpellsRequest request)
    {
        // Validate filter
        string? filter = string.IsNullOrWhiteSpace(request.Filter) ? null : request.Filter.Trim();

        // Get spells
        Result<IEnumerable<Spell>> getSpellsResult = request.Filter == null ?
            await _spellRepository.TryListAsync().ConfigureAwait(false) :
            await _spellRepository.TryFindAsync(request.Filter).ConfigureAwait(false);

        // Return a failure response if the retrieval failed
        if (getSpellsResult.IsFailed)
        {
            return _boundary
                .Failure(GetSpellsResponse
                    .Failure(getSpellsResult.Errors.First().Message));
        }

        // Return a no spells found response if no spells were found
        if (getSpellsResult.Value.Count() == 0)
            return _boundary.NoSpellsFound(GetSpellsResponse.NoSpellsFound());

        // Return a success response with the spells
        return _boundary.Success(GetSpellsResponse.Success(
            getSpellsResult.Value.Select(spell => SpellUseCaseModel.FromSpell(spell))));
    }
}
