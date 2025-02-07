using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Core.UseCases.GetSpells;

/// <summary>
/// Get Spells boundary.
/// </summary>
/// <typeparam name="TOutput">Output type.</typeparam>
public interface IGetSpellsBoundary<TOutput>
{
    /// <summary>
    /// Failure.
    /// </summary>
    /// <param name="response">Response.</param>
    /// <returns>Output.</returns>
    public TOutput Failure(GetSpellsResponse response);

    /// <summary>
    /// No spells found.
    /// </summary>
    /// <param name="response">Response.</param>
    /// <returns>Output.</returns>
    public TOutput NoSpellsFound(GetSpellsResponse response);

    /// <summary>
    /// Success.
    /// </summary>
    /// <param name="response">Response.</param>
    /// <returns>Output.</returns>
    public TOutput Success(GetSpellsResponse response);
}
