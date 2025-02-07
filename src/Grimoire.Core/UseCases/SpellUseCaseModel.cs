using Grimoire.Core.Domain.SpellAggregate;

namespace Grimoire.Core.UseCases;

/// <summary>
/// Spell use case model.
/// </summary>
public record SpellUseCaseModel
{
    /// <summary>
    /// Area of effect.
    /// </summary>
    public string? AreaOfEffect { get; init; }

    /// <summary>
    /// Casting time.
    /// </summary>
    public string CastingTime { get; init; }

    /// <summary>
    /// Components.
    /// </summary>
    public string Components { get; init; }

    /// <summary>
    /// Description.
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    /// Duration.
    /// </summary>
    public string Duration { get; init; }

    /// <summary>
    /// Id.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Attack indicator.
    /// </summary>
    public bool IsAttack { get; init; }

    /// <summary>
    /// Ritual indicator.
    /// </summary>
    public bool IsRitual { get; init; }

    /// <summary>
    /// Level.
    /// </summary>
    public int Level { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Range.
    /// </summary>
    public string Range { get; init; }

    /// <summary>
    /// Saving throw type.
    /// </summary>
    public string? SavingThrowType { get; init; }

    /// <summary>
    /// Creates a new instance of the <see cref="SpellUseCaseModel"/> record
    /// </summary>
    /// <param name="id">Id.</param>
    /// <param name="areaOfEffect">Area of effect.</param>
    /// <param name="castingTime">Casting time.</param>
    /// <param name="components">Components.</param>
    /// <param name="description">Description.</param>
    /// <param name="duration">Duration.</param>
    /// <param name="isAttack">Attack indicator.</param>
    /// <param name="isRitual">Ritual indicator.</param>
    /// <param name="level">Level.</param>
    /// <param name="name">Name.</param>
    /// <param name="range">Range.</param>
    /// <param name="savingThrowType">Saving throw type.</param>
    private SpellUseCaseModel(
        string? id,
        string? areaOfEffect,
        string castingTime,
        string components,
        string description,
        string duration,
        bool isAttack,
        bool isRitual,
        int level,
        string name,
        string range,
        string? savingThrowType)
    {
        AreaOfEffect = areaOfEffect;
        CastingTime = castingTime;
        Components = components;
        Description = description;
        Duration = duration;
        Id = id;
        IsAttack = isAttack;
        IsRitual = isRitual;
        Level = level;
        Name = name;
        Range = range;
        SavingThrowType = savingThrowType;
    }

    /// <summary>
    /// Converts a <see cref="Spell"/> to a <see cref="SpellUseCaseModel"/>.
    /// </summary>
    /// <param name="spell">The spell to convert.</param>
    /// <returns><see cref="SpellUseCaseModel"/>.</returns>
    public static SpellUseCaseModel FromSpell(Spell spell)
    {
        return new(
            spell.Id,
            spell.AreaOfEffect?.ToString(),
            spell.CastingTime.ToString(),
            spell.GetComponentsString(),
            spell.Description,
            spell.Duration.ToString(),
            spell.IsAttack,
            spell.IsRitual,
            spell.Level,
            spell.Name,
            spell.RangeInFeet == 0 ? "Self" : $"{spell.RangeInFeet} feet",
            spell.SavingThrowType?.Abbreviation);
    }
}
