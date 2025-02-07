using Grimoire.Core.UseCases.GetSpells;

namespace Grimoire.WebApi.UseCases.GetSpells;

public class GetSpellsApiBoundary(string spellsBaseUri) : IGetSpellsBoundary<IResult>
{
    // Spells base URI
    private readonly string _spellsBaseUri = spellsBaseUri;

    /// <inheritdoc />
    public IResult Failure(GetSpellsResponse response)
    {
        return Results.Problem(response.Message);
    }

    /// <inheritdoc />
    public IResult NoSpellsFound(GetSpellsResponse response)
    {
        return Results.NotFound(response);
    }

    /// <inheritdoc />
    public IResult Success(GetSpellsResponse response)
    {
        // Return a No Spells Found result if Success was incorrectly called when there are no
        // spells
        if (response.Spells.Any() == false)
            return NoSpellsFound(response);

        // Create a list of spell API models
        List<SpellApiModel> spellApiModels = [];

        // Try to convert spells to API models, adding successful conversions to the list
        foreach (var spell in response.Spells)
        {
            var result = SpellApiModel.TryCreateFromUseCaseModel(_spellsBaseUri, spell);
            if (result.IsSuccess)
                spellApiModels.Add(result.Value);
        }

        // Return a 500 result if no spells could be converted to API models
        if (spellApiModels.Count == 0)
            return Results.Problem("No spells could be converted to API models.", statusCode: 500);

        // Return a 200 OK result with the spell API models
        return Results.Ok(spellApiModels);
    }
}