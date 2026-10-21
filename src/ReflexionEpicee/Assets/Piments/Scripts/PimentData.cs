using System;
using UnityEngine;

// One generated pepper, before it is spawned in the scene.
public class PimentData : IPiment
{
    private const float MinPositive = 0.01f;

    public PimentColor Couleur { get; private set; }
    public string Nom { get; private set; }
    public string Pays { get; private set; }
    public string MatDir => "Materials/" + Couleur;

    public float Shu { get; private set; }
    public float Volume { get; private set; }
    public float Dryness { get; private set; }
    public float Bouncy { get; private set; }
    public float Slimy { get; private set; }
    public float Rough { get; private set; }
    public float Soft { get; private set; }
    public float Spiky { get; private set; }
    public float Reflection { get; private set; }
    public float Refraction => 1f - Reflection;
    public float Tint { get; private set; }

    // Rerolls until the spice instructions give an answer, since some color/country pairs have none.
    public static PimentData Random()
    {
        while (true)
        {
            var couleur = RandomColor();
            var pays = global::Pays.Random();
            var shu = SpiceRecipe.Evaluate(couleur, pays);
            if (!shu.HasValue)
            {
                continue;
            }

            return new PimentData
            {
                Couleur = couleur,
                Pays = pays,
                Nom = $"Piment {couleur} {pays}",
                Shu = shu.Value,
                Volume = UnityEngine.Random.Range(MinPositive, 1f),
                Dryness = UnityEngine.Random.Range(MinPositive, 1f),
                Bouncy = UnityEngine.Random.Range(MinPositive, 1f),
                Slimy = UnityEngine.Random.Range(MinPositive, 1f),
                Rough = UnityEngine.Random.Range(MinPositive, 1f),
                Soft = UnityEngine.Random.Range(MinPositive, 1f),
                Spiky = UnityEngine.Random.Range(MinPositive, 1f),
                Reflection = UnityEngine.Random.value,
                Tint = UnityEngine.Random.value,
            };
        }
    }

    private static PimentColor RandomColor()
    {
        var colors = (PimentColor[])Enum.GetValues(typeof(PimentColor));
        return colors[UnityEngine.Random.Range(0, colors.Length)];
    }

    public override string ToString()
    {
        return $"{Nom} epice: {Shu:0.##} mat: {MatDir}";
    }
}
