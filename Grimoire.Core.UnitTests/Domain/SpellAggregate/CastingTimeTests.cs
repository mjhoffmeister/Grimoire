using FluentResults;
using Grimoire.Core.Domain.SpellAggregate;

namespace Grimoire.Core.UnitTests.Domain.SpellAggregate;

/// <summary>
/// Tests the <see cref="CastingTime"/> record."/>
/// </summary>
public static class CastingTimeTests
{
    /// <summary>
    /// Tests creating a new instance of the <see cref="CastingTime"/> record.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="time">Time.</param>
    /// <param name="expectedIsSuccess">Expected success indicator.</param>
    [Theory]
    [InlineData("Action", null, true)]
    [InlineData("Bonus Action", null, true)]
    [InlineData("Reaction", null, true)]
    [InlineData("Time", "00:01:00", true)]
    [InlineData("Time", null, false)]
    [InlineData("Time", "foo", false)]
    [InlineData(null, null, false)]
    [InlineData("Foo", null, false)]
    public static void TryCreate_Multiple_ReturnsExpectedIsSuccess(
        string? type, string? time, bool expectedIsSuccess)
    {
        // Act

        Result<CastingTime> createCastingTimeResult = CastingTime
            .TryCreate(type, time);

        // Assert

        Assert.Equal(expectedIsSuccess, createCastingTimeResult.IsSuccess);
    }
}
