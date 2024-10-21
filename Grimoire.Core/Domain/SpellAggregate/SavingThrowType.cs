namespace Grimoire.Core.Domain.SpellAggregate;

/// <summary>
/// Saving throw type.
/// </summary>
public record SavingThrowType
{
    // Strength abbreviation
    const string StrengthAbbreviation = "STR";

    // Dexterity abbreviation
    const string DexterityAbbreviation = "DEX";

    // Constitution abbreviation
    const string ConstitutionAbbreviation = "CON";

    // Intelligence abbreviation
    const string IntelligenceAbbreviation = "INT";

    // Wisdom abbreviation
    const string WisdomAbbreviation = "WIS";

    // Charisma abbreviation
    const string CharismaAbbreviation = "CHA";

    // Strength name
    const string StrengthName = "Strength";

    // Dexterity name
    const string DexterityName = "Dexterity";

    // Constitution name
    const string ConstitutionName = "Constitution";

    // Intelligence name
    const string IntelligenceName = "Intelligence";

    // Wisdom name
    const string WisdomName = "Wisdom";

    // Charisma abbreviation
    const string CharismaName = "Charisma";

    /// <summary>
    /// Strength.
    /// </summary>
    public static SavingThrowType Strength => new(1, StrengthAbbreviation, StrengthName);

    /// <summary>
    /// Dexterity.
    /// </summary>
    public static SavingThrowType Dexterity => new(2, DexterityName, DexterityAbbreviation);

    /// <summary>
    /// Constitution.
    /// </summary>
    public static SavingThrowType Constitution => new(
        3, ConstitutionName, ConstitutionAbbreviation);

    /// <summary>
    /// Intelligence.
    /// </summary>
    public static SavingThrowType Intelligence => new(
        4, IntelligenceName, IntelligenceAbbreviation);

    /// <summary>
    /// Wisdom.
    /// </summary>
    public static SavingThrowType Wisdom => new(5, WisdomName, WisdomAbbreviation);

    /// <summary>
    /// Charisma.
    /// </summary>
    public static SavingThrowType Charisma => new(6, CharismaName, CharismaAbbreviation);

    /// <summary>
    /// Abbreviation.
    /// </summary>
    public string Abbreviation { get; init; }

    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Creates a new instance of the <see cref="SavingThrowType"/> record.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <param name="abbreviation">Abbreviation.</param>
    /// <param name="name">Name.</param>
    private SavingThrowType(int id, string abbreviation, string name)
    {
        Abbreviation = abbreviation;
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Tries to parse the value into a <see cref="SavingThrowType"/>.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>
    /// A <see cref="SavingThrowType"/> if the parse was successful; null, otherwise.
    /// </returns>
    public static SavingThrowType? TryParse(string? value)
    {
        return value switch
        {
            StrengthAbbreviation or StrengthName => Strength,
            DexterityAbbreviation or DexterityName => Dexterity,
            ConstitutionAbbreviation or ConstitutionName => Constitution,
            IntelligenceAbbreviation or IntelligenceName => Intelligence,
            WisdomAbbreviation or WisdomName => Wisdom,
            CharismaAbbreviation or CharismaName => Charisma,
            _ => null
        };
    }
}