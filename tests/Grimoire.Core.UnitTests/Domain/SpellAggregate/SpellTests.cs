using FluentResults;
using Grimoire.Core.Domain.SpellAggregate;

namespace Grimoire.Core.UnitTests.Domain.SpellAggregate;

public static class SpellTests
{
    [Theory]
    [InlineData(
        "1",
        1,
        false,
        false,
        "Mage Armor",
        "The target's AC becomes 13 plus their Dexterity modifier.",
        "Action",
        null,
        0,
        null,
        null,
        "V, S, M",
        "Time span",
        false,
        "08:00:00",
        null,
        true)]
    [InlineData(
        "2",
        3,
        false,
        false,
        "Fireball",
        "A ball of fire detonates at the point you specify in range for 8d6 fire damage.",
        "Action",
        null,
        150,
        20,
        "Sphere",
        "V, S, M",
        "Instantaneous",
        false,
        null,
        "DEX",
        true)]
    public static void TryCreate_Multiple_ReturnsExpectedIsSuccess(
        string? id,
        int? level,
        bool? isAttack,
        bool? isRitual,
        string? name,
        string description,
        string? castingTimeType,
        string? castingTime,
        int? rangeInFeet,
        int? areaSizeInFeet,
        string? areaShape,
        string? componentsString,
        string? durationType,
        bool? requiresConcentration,
        string? durationTime,
        string? savingThrowType,
        bool expectedIsSuccess)
    {
        // Arrange

        string[]? componentsArray = componentsString?.Split(", ");

        // Act

        Result<Spell> createSpellResult = Spell
            .TryCreate(
                id,
                level,
                isAttack,
                isRitual,
                name,
                description,
                castingTimeType,
                castingTime,
                rangeInFeet,
                areaSizeInFeet,
                areaShape,
                componentsArray,
                durationType,
                requiresConcentration,
                durationTime,
                savingThrowType);

        // Assert

        Assert.Equal(expectedIsSuccess, createSpellResult.IsSuccess);
    }
}
