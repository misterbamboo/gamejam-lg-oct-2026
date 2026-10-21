using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

// One pepper as read from Piment.json. Missing values keep a valid default.
public class PimentData : IPiment
{
    [JsonProperty("shu")] private int shu;
    [JsonProperty("nom")] private string nom;
    [JsonProperty("pays")] private string pays;
    [JsonProperty("MatDir")] private string matDir;

    [JsonProperty("volume")] private float volume = 0.5f;
    [JsonProperty("dryness")] private float dryness = 0.5f;
    [JsonProperty("bouncy")] private float bouncy = 0.5f;
    [JsonProperty("slimy")] private float slimy = 0.5f;
    [JsonProperty("rough")] private float rough = 0.5f;
    [JsonProperty("soft")] private float soft = 0.5f;
    [JsonProperty("spiky")] private float spiky = 0.5f;
    [JsonProperty("reflection")] private float reflection = 0.5f;
    [JsonProperty("tint")] private float tint = 0.5f;

    private const float MinPositive = 0.01f;

    // Values are clamped to the ranges of the enjoyment formula, so a typo in the JSON can't break it.
    [JsonIgnore] public int Shu => Mathf.Clamp(shu, 0, 10);
    [JsonIgnore] public string Nom => nom;
    [JsonIgnore] public string Pays => pays;
    [JsonIgnore] public string MatDir => matDir;

    [JsonIgnore] public float Volume => Mathf.Clamp(volume, MinPositive, 1f);
    [JsonIgnore] public float Dryness => Mathf.Clamp(dryness, MinPositive, 1f);
    [JsonIgnore] public float Bouncy => Mathf.Max(bouncy, MinPositive);
    [JsonIgnore] public float Slimy => Mathf.Max(slimy, MinPositive);
    [JsonIgnore] public float Rough => Mathf.Max(rough, MinPositive);
    [JsonIgnore] public float Soft => Mathf.Max(soft, MinPositive);
    [JsonIgnore] public float Spiky => Mathf.Max(spiky, MinPositive);
    [JsonIgnore] public float Reflection => Mathf.Clamp01(reflection);
    [JsonIgnore] public float Refraction => 1f - Reflection;
    [JsonIgnore] public float Tint => Mathf.Clamp01(tint);

    public override string ToString()
    {
        return nom + " epice: " + Shu + " mat: " + matDir;
    }
}
