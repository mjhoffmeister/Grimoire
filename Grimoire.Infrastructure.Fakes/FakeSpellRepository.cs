using FluentResults;
using Grimoire.Core.Domain.SpellAggregate;
using Grimoire.Core.Interfaces;

namespace Grimoire.Infrastructure.Fakes;

/// <summary>
/// Fake spell repository.
/// </summary>
public class FakeSpellRepository : IReadOnlyRepository<string, Spell>
{
    // Spells
    private readonly List<Spell> _spells =
    [
        Spell
            .TryCreate(
                Guid.NewGuid().ToString(),
                1,
                false,
                false,
                "Mage Armor",
                "The target's AC becomes 13 plus their Dexterity modifier.",
                "Action",
                null,
                0,
                null,
                null,
                ["V", "S", "M"],
                "Time span",
                false,
                "08:00:00",
                null)
            .Value,
        Spell
            .TryCreate(
                    Guid.NewGuid().ToString(),
                    3,
                    false,
                    false,
                    "Fireball",
                    "A ball of fire detonates at the point you specify in range for 8d6 fire " +
                        "damage.",
                    "Action",
                    null,
                    150,
                    20,
                    "Sphere",
                    ["V", "S", "M"],
                    "Instantaneous",
                    false,
                    null,
                    "DEX")
            .Value
    ];

    /// <summary>
    /// Tries to find spells.
    /// </summary>
    /// <param name="filter">Filter.</param>
    /// <returns>The result of the attempt.</returns>
    public Task<Result<IEnumerable<Spell>>> TryFindAsync(string filter)
    {
        var filteredSpells = _spells
            .Where(spell => 
                spell.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                spell.Description.Contains(filter, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(Result.Ok(filteredSpells));
    }

    /// <inheritdoc />
    public Task<Result<Spell>> TryGetByIdAsync(string id)
    {
        var spell = _spells.Find(s => s.Id == id);
        if (spell != null)
        {
            return Task.FromResult(Result.Ok(spell));
        }
        else
        {
            return Task.FromResult(Result.Fail<Spell>("Spell not found"));
        }
    }

    /// <inheritdoc />
    public Task<Result<IEnumerable<Spell>>> TryListAsync()
    {
        return Task.FromResult(Result.Ok<IEnumerable<Spell>>(_spells));
    }
}