// What the cooking pot needs from a pepper. Piment will implement it once our work is merged.
public interface IPiment
{
    /// <summary>Spice level, 0 to 10.</summary>
    float Shu { get; }

    /// <summary>The pepper's visible color, mixed into the chili.</summary>
    UnityEngine.Color DisplayColor { get; }

    /// <summary>0 &lt; Volume &lt;= 1</summary>
    float Volume { get; }

    /// <summary>0 &lt; Dryness &lt;= 1</summary>
    float Dryness { get; }

    /// <summary>&gt; 0</summary>
    float Bouncy { get; }

    /// <summary>&gt; 0</summary>
    float Slimy { get; }

    /// <summary>&gt; 0</summary>
    float Rough { get; }

    /// <summary>&gt; 0</summary>
    float Soft { get; }

    /// <summary>&gt; 0</summary>
    float Spiky { get; }

    /// <summary>0 to 1</summary>
    float Reflection { get; }

    /// <summary>0 to 1, should return 1 - Reflection.</summary>
    float Refraction { get; }

    /// <summary>0 to 1</summary>
    float Tint { get; }
}
