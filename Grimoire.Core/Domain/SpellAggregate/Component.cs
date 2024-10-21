using System.Globalization;

namespace Grimoire.Core.Domain.SpellAggregate;

public record Component
{
    // Material abbreviation
    const string MaterialAbbreviation = "M";

    // Somatic abbreviation
    const string SomaticAbbreviation = "S";

    // Verbal abbreviation
    const string VerbalAbbreviation = "V";

    // Material name
    const string MaterialName = "Material";

    // Somatic name
    const string SomaticName = "Somatic";

    // Verbal name
    const string VerbalName = "Verbal";

    /// <summary>
    /// Material.
    /// </summary>
    public static Component Material => new(MaterialAbbreviation, MaterialName);

    /// <summary>
    /// Somatic.
    /// </summary>
    public static Component Somatic => new(SomaticAbbreviation, SomaticName);

    /// <summary>
    /// Verbal.
    /// </summary>
    public static Component Verbal => new(VerbalAbbreviation, VerbalName);

    /// <summary>
    /// Abbreviation.
    /// </summary>
    public string Abbreviation { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Creates a new instance of the <see cref="Component"/> record.
    /// </summary>
    /// <param name="abbreviation">Abbreviation.</param>
    /// <param name="name">Name.</param>
    private Component(string abbreviation, string name)
    {
        Abbreviation = abbreviation;
        Name = name;
    }

    /// <inheritdoc/>
    public static Component? TryParse(string? value)
    {
        if (value == null)
            return null;

        TextInfo enUS = new CultureInfo("en-US", false).TextInfo;

        string valueTitleCase = enUS.ToTitleCase(value.Trim().ToLower());

        return valueTitleCase switch
        {
            MaterialAbbreviation or MaterialName => Material,
            SomaticAbbreviation or SomaticName => Somatic,
            VerbalAbbreviation or VerbalName => Verbal,
            _ => null
        };
    }
}
