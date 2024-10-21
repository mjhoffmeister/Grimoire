using FluentResults;

namespace Grimoire.Core.Domain.SpellAggregate;

/// <summary>
/// Casting time of a spell.
/// </summary>
public record class CastingTime
{
    /// <summary>
    /// Creates a new instance of the <see cref="CastingTime"/> record.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="activationTime"></param>
    private CastingTime(CastingTimeType type, TimeSpan? time)
    {
        Type = type;
        Time = time;
    }

    /// <summary>
    /// Type.
    /// </summary>
    public CastingTimeType Type { get; }

    /// <summary>
    /// Activation time.
    /// </summary>
    public TimeSpan? Time { get; }

    /// <summary>
    /// Tries to create a casting time.
    /// </summary>
    /// <param name="typeString">Type string.</param>
    /// <param name="timeString">Time string.</param>
    /// <returns></returns>
    public static Result<CastingTime> TryCreate(string? typeString, string? timeString)
    {
        CastingTimeType? castingTimeType = CastingTimeType.TryParse(typeString);

        if (castingTimeType == null)
            return Result.Fail<CastingTime>($"'{typeString}' isn't a valid casting time type.");

        TimeSpan? time = null;

        if (castingTimeType == CastingTimeType.Time)
        {
            if (!TimeSpan.TryParse(timeString, out TimeSpan parsedTime))
                return Result.Fail("A valid time is required for casting time type 'Time'.");

            time = parsedTime;
        }

        return Result.Ok(new CastingTime(castingTimeType, time));
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Type.Name switch
        {
            "Action" => "1 Action",
            "Bonus Action" => "1 Bonus Action",
            "Reaction" => "1 Reaction",
            _ => Time!.Value.GetTimeString()
        };
    }
}
