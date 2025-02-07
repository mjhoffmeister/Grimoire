using System.Globalization;

namespace Grimoire.Core.Domain.SpellAggregate;

/// <summary>
/// Duration type.
/// </summary>
public record DurationType
{
    // Instantaneous name.
    const string InstantaneousName = "Instantaneous";

    // Time span name.
    const string TimeSpanName = "Time Span";

    /// <summary>
    /// Instantaneous.
    /// </summary>
    public static DurationType Instantaneous => new(1, InstantaneousName);

    /// <summary>
    /// Time span.
    /// </summary>
    public static DurationType TimeSpan => new(2, TimeSpanName);

    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Creates a new instance of the <see cref="DurationType"/> record.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <param name="name">Name.</param>
    private DurationType(int id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Tries to parse the value into a <see cref="DurationType"/>.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>
    /// A <see cref="DurationType"/> if the parse was successful; null, otherwise.
    /// </returns>
    public static DurationType? TryParse(string? value)
    {
        if (value == null)
            return null;

        TextInfo enUS = new CultureInfo("en-US", false).TextInfo;

        string valueTitleCase = enUS.ToTitleCase(value.Trim().ToLower());

        return valueTitleCase switch
        {
            InstantaneousName => Instantaneous,
            TimeSpanName => TimeSpan,
            _ => null
        };
    }
}
