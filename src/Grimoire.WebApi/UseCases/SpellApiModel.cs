using FluentResults;
using Grimoire.Core.UseCases;
using System.Text.Json.Serialization;

namespace Grimoire.WebApi.UseCases;

/// <summary>
/// Spell API model.
/// </summary>
public class SpellApiModel
{
    /// <summary>
    /// Id.
    /// </summary>
    [JsonPropertyName("@id")]
    public string? Id { get; set; }

    /// <summary>
    /// Level.
    /// </summary>
    [JsonPropertyName("level")]
    public int Level { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Casting time.
    /// </summary>
    [JsonPropertyName("castingTime")]
    public string? CastingTime { get; set; }

    /// <summary>
    /// Durations.
    /// </summary>
    [JsonPropertyName("duration")]
    public string? Duration { get; set; }

    /// <summary>
    /// Range/area.
    /// </summary>
    [JsonPropertyName("rangeArea")]
    public string? RangeArea { get; set; }

    /// <summary>
    /// Attack/save.
    /// </summary>
    [JsonPropertyName("attackSave")]
    public string? AttackSave { get; set; }

    /// <summary>
    /// Tries to create an API model from a use case model.
    /// </summary>
    /// <param name="baseUriString">Base URI string.</param>
    /// <param name="spellUseCaseModel">Spell use case model.</param>
    /// <returns>The result of the attempt.</returns>
    public static Result<SpellApiModel> TryCreateFromUseCaseModel(
        string baseUriString, SpellUseCaseModel spellUseCaseModel)
    {
        if (!Uri.TryCreate(baseUriString, UriKind.Absolute, out Uri? baseUri))
            return Result.Fail<SpellApiModel>("Invalid base URL.");

        if (string.IsNullOrWhiteSpace(spellUseCaseModel.Id))
        {
            return Result.Fail<SpellApiModel>(
                "Cannot convert a transient spell use case model into an API model " +
                "(Id is required.)");
        }

        return Result.Ok(new SpellApiModel
        {
            Id = new Uri(baseUri, spellUseCaseModel.Id).ToString(),
            Level = spellUseCaseModel.Level,
            Name = spellUseCaseModel.Name,
            CastingTime = spellUseCaseModel.CastingTime,
            Duration = spellUseCaseModel.Duration,
            RangeArea = GetRangeArea(spellUseCaseModel),
            AttackSave = GetAttackSave(spellUseCaseModel)
        });
    }

    /// <summary>
    /// Gets the attack/save of a spell.
    /// </summary>
    /// <param name="spellUseCaseModel"><see cref="SpellUseCaseModel"/>.</param>
    /// <returns>Attack/save.</returns>
    private static string GetAttackSave(SpellUseCaseModel spellUseCaseModel)
    {
        return spellUseCaseModel.IsAttack ?
            spellUseCaseModel.Range.Contains("ft") ? "Ranged" : "Melee" :
            spellUseCaseModel.SavingThrowType ?? "";
    }

    /// <summary>
    /// Gets the range/area of a spell.
    /// </summary>
    /// <param name="spellUseCaseModel"><see cref="SpellUseCaseModel"/>.</param>
    /// <returns>Range/area.</returns>
    private static string GetRangeArea(SpellUseCaseModel spellUseCaseModel)
    {
        return $"{spellUseCaseModel.Range}" +
            $"{(spellUseCaseModel.AreaOfEffect != null ? 
            $"/{spellUseCaseModel.AreaOfEffect}" : "")}";
    }
}
