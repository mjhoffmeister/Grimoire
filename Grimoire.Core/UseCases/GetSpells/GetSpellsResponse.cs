namespace Grimoire.Core.UseCases.GetSpells;

/// <summary>
/// Get Spells response.
/// </summary>
public record GetSpellsResponse
{
    /// <summary>
    /// Success indicator.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Message.
    /// </summary>
    public string Message { get; init; }

    /// <summary>
    /// Spells.
    /// </summary>
    public IEnumerable<SpellUseCaseModel> Spells { get; init; }

    /// <summary>
    /// Creates a new instance of the <see cref="GetSpellsResponse"/> record.
    /// </summary>
    /// <param name="isSuccess">Success indicator.</param>
    /// <param name="message">Message.</param>
    /// <param name="spells">Spells.</param>
    private GetSpellsResponse(bool isSuccess, string message, IEnumerable<SpellUseCaseModel> spells)
    {
        IsSuccess = isSuccess;
        Message = message;
        Spells = spells;
    }

    /// <summary>
    /// Failure response.
    /// </summary>
    /// <param name="errorMessage">Error message.</param>
    /// <returns>The response.</returns>
    public static GetSpellsResponse Failure(string errorMessage)
    {
        return new GetSpellsResponse(false, errorMessage, []);
    }

    /// <summary>
    /// No spells found response.
    /// </summary>
    /// <returns></returns>
    public static GetSpellsResponse NoSpellsFound()
    {
        return new GetSpellsResponse(false, "No spells were found.", []);
    }

    /// <summary>
    /// Success response.
    /// </summary>
    /// <param name="spells">Spells.</param>
    /// <returns>The response.</returns>
    public static GetSpellsResponse Success(IEnumerable<SpellUseCaseModel> spells)
    {
        return new GetSpellsResponse(true, "Success.", spells);
    }
}
