using System.Globalization;

namespace Grimoire.Core.Domain.SpellAggregate;

/// <summary>
/// Area of effect shape.
/// </summary>
public record AreaOfEffectShape
{
    // Cone name
    const string ConeName = "Cone";

    // Cube name
    const string CubeName = "Cube";

    // Cylinder name
    const string CylinderName = "Cylinder";

    // Emanation name
    const string EmanationName = "Emanation";

    // Line name
    const string LineName = "Line";

    // Sphere name
    const string SphereName = "Sphere";

    /// <summary>
    /// Cone.
    /// </summary>
    public static AreaOfEffectShape Cone => new(1, ConeName);

    /// <summary>
    /// Cube.
    /// </summary>
    public static AreaOfEffectShape Cube => new(2, CubeName);

    /// <summary>
    /// Cylinder.
    /// </summary>
    public static AreaOfEffectShape Cylinder => new(3, CylinderName);

    /// <summary>
    /// Emanation.
    /// </summary>
    public static AreaOfEffectShape Emanation => new(4, EmanationName);

    /// <summary>
    /// Line.
    /// </summary>
    public static AreaOfEffectShape Line => new(5, LineName);

    /// <summary>
    /// Sphere.
    /// </summary>
    public static AreaOfEffectShape Sphere => new(6, SphereName);

    /// <summary>
    /// Creates a new instance of the <see cref="AreaOfEffectShape"/> record.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    private AreaOfEffectShape(int id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Tries to parse an area of effect shape.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>
    /// An <see cref="AreaOfEffectShape"/> if the parse was successful; null, otherwise.
    /// </returns>
    public static AreaOfEffectShape? TryParse(string? value)
    {
        if (value == null)
            return null;

        TextInfo enUS = new CultureInfo("en-US", false).TextInfo;

        string valueTitleCase = enUS.ToTitleCase(value.Trim().ToLower());

        return valueTitleCase switch
        {
            ConeName => Cone,
            CubeName => Cube,
            CylinderName => Cylinder,
            EmanationName => Emanation,
            LineName => Line,
            SphereName => Sphere,
            _ => null
        };
    }
}
