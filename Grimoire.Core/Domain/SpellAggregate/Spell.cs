using FluentResults;

namespace Grimoire.Core.Domain.SpellAggregate;

/// <summary>
/// Spell entity.
/// </summary>
public class Spell
{
    /// <summary>
    /// Area of effect.
    /// </summary>
    public AreaOfEffect? AreaOfEffect { get; private set; }

    /// <summary>
    /// Casting time.
    /// </summary>
    public CastingTime CastingTime { get; private set; }

    /// <summary>
    /// Components.
    /// </summary>
    public IEnumerable<Component> Components { get; private set; }

    /// <summary>
    /// Description.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Duration.
    /// </summary>
    public Duration Duration { get; private set; }

    /// <summary>
    /// Id.
    /// </summary>
    public string? Id { get; private set; }

    /// <summary>
    /// Attack indicator.
    /// </summary>
    public bool IsAttack { get; private set; }

    /// <summary>
    /// Ritual indicator.
    /// </summary>
    public bool IsRitual { get; private set; }

    /// <summary>
    /// Level.
    /// </summary>
    public int Level { get; private set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Range in feet.
    /// </summary>
    public int RangeInFeet { get; private set; }

    /// <summary>
    /// Saving throw type.
    /// </summary>
    public SavingThrowType? SavingThrowType { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="Spell"/> class.
    /// </summary>
    /// <param name="areaOfEffect">Area of effect.</param>
    /// <param name="castingTime">Casting time.</param>
    /// <param name="components">Components.</param>
    /// <param name="description">Description.</param>
    /// <param name="duration">Duration.</param>
    /// <param name="id">Id.</param>
    /// <param name="isAttack">Attack indicator.</param>
    /// <param name="isRitual">Ritual indicator.</param>
    /// <param name="level">Level.</param>
    /// <param name="name">Name.</param>
    /// <param name="rangeInFeet">Range in feet.</param>
    /// <param name="savingThrowType">Saving throw type.</param>
    private Spell(
        string? id,
        AreaOfEffect? areaOfEffect,
        CastingTime castingTime,
        IEnumerable<Component> components,
        string description,
        Duration duration,
        bool isAttack,
        bool isRitual,
        int level,
        string name,
        int rangeInFeet,
        SavingThrowType? savingThrowType)
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
        RangeInFeet = rangeInFeet;
        SavingThrowType = savingThrowType;
    }

    /// <summary>
    /// Tries to create a spell.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <param name="level">Level.</param>
    /// <param name="isAttack">Attack indicator.</param>
    /// <param name="isRitual">Ritual indicator.</param>
    /// <param name="name">Name.</param>
    /// <param name="description">Description.</param>
    /// <param name="castingTimeType">Casting time type.</param>
    /// <param name="castingTime">Casting time.</param>
    /// <param name="rangeInFeet">Range in feet.</param>
    /// <param name="areaSizeInFeet">Area size in feet.</param>
    /// <param name="areaShape">Area shape.</param>
    /// <param name="componentsArray">Components array.</param>
    /// <param name="durationType">Duration type.</param>
    /// <param name="requiresConcentration">Concentration indicator.</param>
    /// <param name="durationTime">Duration time.</param>
    /// <param name="savingThrowType">Saving throw type.</param>
    /// <returns>The result of the attempt.</returns>
    public static Result<Spell> TryCreate(
        string? id,
        int? level,
        bool? isAttack,
        bool? isRitual,
        string? name,
        string description,
        string? castingTimeType,
        string? castingTime,
        int? rangeInFeet,
        int? areaSizeInFeet,
        string? areaShape,
        string?[]? componentsArray,
        string? durationType,
        bool? requiresConcentration,
        string? durationTime,
        string? savingThrowType)
    {
        if (isAttack == null)
            return Result.Fail<Spell>("An attack indicator is required.");

        if (isRitual == null)
            return Result.Fail<Spell>("A ritual indicator is required.");

        if (isAttack == true && isRitual == true)
            return Result.Fail<Spell>("A spell can't be both an attack and a ritual.");

        Result <CastingTime> createCastingTimeResult = CastingTime.TryCreate(
            castingTimeType, castingTime);

        if (createCastingTimeResult.IsFailed)
            return Result.Fail<Spell>(createCastingTimeResult.Errors.First().Message);

        Result<Duration> createDurationResult = Duration.TryCreate(
            durationType, requiresConcentration, durationTime);

        if (createDurationResult.IsFailed)
            return Result.Fail<Spell>(createDurationResult.Errors.First().Message);

        Result<IEnumerable<Component>> parseComponentsResult = TryParseComponents(componentsArray);

        if (parseComponentsResult.IsFailed)
            return Result.Fail<Spell>(parseComponentsResult.Errors.First().Message);

        Result<AreaOfEffect?> getAreaOfEffectResult = 
            TryGetAreaOfEffect(areaSizeInFeet, areaShape);

        if (getAreaOfEffectResult.IsFailed)
            return Result.Fail<Spell>(getAreaOfEffectResult.Errors.First().Message);

        return Result.Ok(new Spell(
            id,
            getAreaOfEffectResult.Value,
            createCastingTimeResult.Value,
            parseComponentsResult.Value,
            description,
            createDurationResult.Value,
            isAttack!.Value,
            isRitual!.Value,
            level!.Value,
            name!,
            rangeInFeet!.Value,
            SavingThrowType.TryParse(savingThrowType)));
    }

    /// <summary>
    /// Gets a string representation of all components.
    /// </summary>
    /// <returns>Components string.</returns>
    public string GetComponentsString()
    {
        return string.Join(
            ", ",
            Components
                .OrderByDescending(component => component.Abbreviation)
                .Select(component => component.Abbreviation));
    }

    /// <summary>
    /// Tries to update the spell's id.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <returns>The result of the attempt.</returns>
    public Result TryUpdateId(string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return Result.Fail("Id is required.");

        Id = id;

        return Result.Ok();
    }

    /// <summary>
    /// Tries to get the area of effect.
    /// </summary>
    /// <param name="sizeInFeet">Size in feet.</param>
    /// <param name="shape">Shape.</param>
    /// <returns>The result of the attempt.</returns>
    private static Result<AreaOfEffect?> TryGetAreaOfEffect(int? sizeInFeet, string? shape)
    {
        if (sizeInFeet == null && shape == null)
            return Result.Ok<AreaOfEffect?>(null);

        return AreaOfEffect
            .TryCreate(sizeInFeet, shape)
            .Map(areaOfEffect => (AreaOfEffect?)areaOfEffect);
    }

    /// <summary>
    /// Tries to parse components.
    /// </summary>
    /// <param name="componentsArray">Components array.</param>
    /// <returns>The result of the attempt.</returns>
    private static Result<IEnumerable<Component>> TryParseComponents(string?[]? componentsArray)
    {
        if (componentsArray == null)
            return Result.Ok(Enumerable.Empty<Component>());

        List<Component> components = [];

        foreach (string? componentString in componentsArray)
        {
            Component? component = Component.TryParse(componentString);

            if (component == null)
            {
                return Result.Fail<IEnumerable<Component>>($"'{componentString}' isn't a valid" +
                    $"component.");
            }

            components.Add(component);
        }

        return Result.Ok(components.AsEnumerable());
    }
}
