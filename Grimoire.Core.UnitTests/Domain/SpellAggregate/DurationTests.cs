using FluentResults;
using Grimoire.Core.Domain.SpellAggregate;

namespace Grimoire.Core.UnitTests.Domain.SpellAggregate;

public static class DurationTests
{
    [Theory]
    [InlineData("Instantaneous", false, null, true)]
    public static void TryCreate_Multiple_ReturnsExpectedIsSuccess(
        string? durationTypeName,
        bool? requiresConcentration,
        string? timeSpan,
        bool expectedIsSuccess)
    {
        // Act

        Result<Duration> createDurationResult = Duration
            .TryCreate(durationTypeName, requiresConcentration, timeSpan);

        // Assert

        Assert.Equal(expectedIsSuccess, createDurationResult.IsSuccess);
    }
}
