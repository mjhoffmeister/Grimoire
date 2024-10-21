namespace Grimoire.Core.UseCases.GetSpells;

/// <summary>
/// Request for getting spells.
/// </summary>
/// <param name="Filter">Filter.</param>
public record GetSpellsRequest(string? Filter);