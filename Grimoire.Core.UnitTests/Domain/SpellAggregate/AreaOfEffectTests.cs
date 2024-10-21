using FluentResults;
using Grimoire.Core.Domain.SpellAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Core.UnitTests.Domain.SpellAggregate;

/// <summary>
/// Tests the <see cref="Area"/> record.
/// </summary>
public static class AreaOfEffectTests
{
    [Theory]
    [InlineData(15, "Cone", true)]
    [InlineData(20, "Cube", true)]
    [InlineData(10, "Cylinder", true)]
    [InlineData(10, "Emanation", true)]
    [InlineData(60, "Line", true)]
    [InlineData(30, "Sphere", true)]
    [InlineData(null, null, false)]
    [InlineData(0, "Cone", false)]
    [InlineData(15, null, false)]
    [InlineData(15, "foo", false)]
    public static void TryCreate_Multiple_ReturnsExpectedIsSuccess(
        int? sizeInFeet, string? shape, bool expectedIsSuccess)
    {
        // Act

        Result<AreaOfEffect> createAreaResult = AreaOfEffect
            .TryCreate(sizeInFeet, shape);

        // Assert

        Assert.Equal(expectedIsSuccess, createAreaResult.IsSuccess);
    }
}
