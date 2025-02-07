using FluentResults;

namespace Grimoire.Core.Domain.SpellAggregate;

public record AreaOfEffect
{
    /// <summary>
    /// Shape.
    /// </summary>
    public AreaOfEffectShape Shape { get; }

    /// <summary>
    /// Size in feet.
    /// </summary>
    public int SizeInFeet { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="AreaOfEffect"/> record.
    /// </summary>
    /// <param name="shape">Shape.</param>
    /// <param name="sizeInFeet">Size in feet.</param>
    private AreaOfEffect(AreaOfEffectShape shape, int sizeInFeet)
    {
        Shape = shape;
        SizeInFeet = sizeInFeet;
    }

    /// <summary>
    /// Tries to create an <see cref="AreaOfEffect"/>.
    /// </summary>
    /// <param name="sizeInFeet">Size in feet.</param>
    /// <param name="shapeName">Shape name.</param>
    /// <returns>The result of the attempt.</returns>
    public static Result<AreaOfEffect> TryCreate(int? sizeInFeet, string? shapeName)
    {
        if (sizeInFeet < 1)
        {
            return Result.Fail<AreaOfEffect>(
                "Size in feet is required and must be greater than 0.");
        }

        AreaOfEffectShape? shape = AreaOfEffectShape.TryParse(shapeName);

        if (shape == null)
            return Result.Fail<AreaOfEffect>($"'{shapeName}' isn't a valid area of effect shape.");

        return new AreaOfEffect(shape, sizeInFeet!.Value);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{SizeInFeet} foot {Shape.Name.ToLowerInvariant()}";
    }
}