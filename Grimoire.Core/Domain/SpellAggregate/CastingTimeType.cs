using System.Globalization;

namespace Grimoire.Core.Domain.SpellAggregate;

/// <summary>
/// Casting time type.
/// </summary>
public record CastingTimeType
{
    // Action name
    const string ActionName = "Action";

    // Bonus action name
    const string BonusActionName = "Bonus Action";

    // Reaction name
    const string ReactionName = "Reaction";

    // Time name
    const string TimeName = "Time";

    /// <summary>
    /// Action.
    /// </summary>
    public static CastingTimeType Action => new (1, ActionName);

    /// <summary>
    /// Bonus action.
    /// </summary>
    public static CastingTimeType BonusAction => new (2, BonusActionName);

    /// <summary>
    /// Reaction.
    /// </summary>
    public static CastingTimeType Reaction => new(3, ReactionName);

    /// <summary>
    /// Time.
    /// </summary>
    public static CastingTimeType Time => new(4, TimeName);

    /// <summary>
    /// Creates a new isntance of the <see cref="CastingTimeType"/> record.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    private CastingTimeType(int id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Tries to parse the specified value into a <see cref="CastingTimeType"/>.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>
    /// A <see cref="CastingTimeType"/> if the parse was successful; null, otherwise.
    /// </returns>
    public static CastingTimeType? TryParse(string? value)
    {
        if (value == null)
            return null;

        TextInfo enUS = new CultureInfo("en-US", false).TextInfo;

        string valueTitleCase = enUS.ToTitleCase(value.Trim().ToLower());

        return valueTitleCase switch
        {
            "Action" => Action,
            "Bonus Action" => BonusAction,
            "Reaction" => Reaction,
            "Time" => Time,
            _ => null
        };
    }
}
