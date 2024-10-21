namespace Grimoire.Core.Domain.SpellAggregate;

/// <summary>
/// Extension methods.
/// </summary>
internal static class Extensions
{
    /// <summary>
    /// Gets a time string for a time span.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static string GetTimeString(this TimeSpan timeSpan)
    {
        if (timeSpan.TotalSeconds == 6)
            return "1 round";

        if (timeSpan.TotalMinutes < 60)
            return $"{timeSpan.TotalMinutes} minutes";

        if (timeSpan.TotalHours < 24)
            return $"{timeSpan.TotalHours} hours";

        return $"{timeSpan.TotalDays} days";
    }
}
