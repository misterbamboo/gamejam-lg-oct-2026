using UnityEngine;
using System.Collections;

public class Piment : MonoBehaviour, IPiment
{
    // Niveau d'épice 0 à 10
    [SerializeField]
    float shu;

    public float Shu
    {
        get { return shu; }
        private set
        {
            if (value >= 0 && value <= 10)
                shu = value;
            else
            {
                Debug.Log("Piment créer avec une mauvaise valeur de shu.");
                shu = 0;
            }
        }
    }

    [SerializeField]
    string nom;

    public string Nom
    {
        get { return nom; }
        private set { nom = value; }
    }

    [SerializeField]
    string pays;

    public string Pays
    {
        get { return pays; }
        private set { pays = value; }
    }

    [SerializeField]
    string matDir;

    public string MatDir
    {
        get { return matDir; }
        private set { matDir = value; }
    }

    // Properties for the enjoyment formula (Shared/User Enjoyment Formula.txt).
    [SerializeField, Range(0.01f, 1f)]
    float volume = 0.5f;

    [SerializeField, Range(0.01f, 1f)]
    float dryness = 0.5f;

    [SerializeField, Min(0.01f)]
    float bouncy = 0.5f;

    [SerializeField, Min(0.01f)]
    float slimy = 0.5f;

    [SerializeField, Min(0.01f)]
    float rough = 0.5f;

    [SerializeField, Min(0.01f)]
    float soft = 0.5f;

    [SerializeField, Min(0.01f)]
    float spiky = 0.5f;

    [SerializeField, Range(0f, 1f)]
    float reflection = 0.5f;

    [SerializeField, Range(0f, 1f)]
    float tint = 0.5f;

    public float Volume => volume;
    public float Dryness => dryness;
    public float Bouncy => bouncy;
    public float Slimy => slimy;
    public float Rough => rough;
    public float Soft => soft;
    public float Spiky => spiky;
    public float Reflection => reflection;
    public float Refraction => 1f - reflection;
    public float Tint => tint;

    // The color of the material PimentsManager put on it (Resources/Materials).
    public Color DisplayColor => GetComponentInChildren<Renderer>().sharedMaterial.color;

    public Piment(int shu, string nom, string pays, string matDir)
    {
        Shu = shu;
        Nom = nom;
        Pays = pays;
        MatDir = matDir;
    }

    public void CopyVals(PimentData p)
    {
        Shu = p.Shu;
        Nom = p.Nom;
        Pays = p.Pays;
        MatDir = p.MatDir;
        volume = p.Volume;
        dryness = p.Dryness;
        bouncy = p.Bouncy;
        slimy = p.Slimy;
        rough = p.Rough;
        soft = p.Soft;
        spiky = p.Spiky;
        reflection = p.Reflection;
        tint = p.Tint;
    }

    public override string ToString()
    {
        return nom + " epice: " + shu + " mat: " + MatDir;
    }
}
