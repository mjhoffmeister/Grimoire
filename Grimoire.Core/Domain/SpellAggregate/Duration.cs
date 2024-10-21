using FluentResults;

namespace Grimoire.Core.Domain.SpellAggregate;

public record Duration
{
    /// <summary>
    /// Requires concentration indicator.
    /// </summary>
    public bool RequiresConcentration { get; init; }

    /// <summary>
    /// Time.
    /// </summary>
    public TimeSpan? Time { get; init; }

    /// <summary>
    /// Type.
    /// </summary>
    public DurationType Type { get; init; }

    /// <summary>
    /// Creates a new instance of the <see cref="Duration"/> record.
    /// </summary>
    /// <param name="requiresConcentration">Requires concentration indicator.</param>
    /// <param name="time">Time span.</param>
    /// <param name="type">Type.</param>
    private Duration(bool requiresConcentration, TimeSpan? time, DurationType type)
    {
        RequiresConcentration = requiresConcentration;
        Time = time;
        Type = type;
    }

    /// <summary>
    /// Tries to create a <see cref="Duration"/>.
    /// </summary>
    /// <param name="durationTypeName">Duration type name.</param>
    /// <param name="requiresConcentration">Requires concentration indicator.</param>
    /// <param name="timeString">Time string.</param>
    /// <returns>The result of the attempt.</returns>
    public static Result<Duration> TryCreate(
        string? durationTypeName, bool? requiresConcentration, string? timeString)
    {
        DurationType? durationType = DurationType.TryParse(durationTypeName);

        if (durationType == null)
            return Result.Fail<Duration>($"'{durationTypeName}' isn't a valid duration type.");

        TimeSpan? time = null;

        if (durationType == DurationType.TimeSpan)
        {
            if (!TimeSpan.TryParse(timeString, out TimeSpan parsedTime))
                return Result.Fail("A valid time is required for casting time type 'Time'.");

            time = parsedTime;
        }

        return Result.Ok(new Duration(requiresConcentration ?? false, time, durationType));
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        if (Type == DurationType.Instantaneous)
            return DurationType.Instantaneous.Name;

        return $"{(RequiresConcentration ? "Concentration, up to " : "")}" +
            $"{Time!.Value.GetTimeString()}";
    }
}
