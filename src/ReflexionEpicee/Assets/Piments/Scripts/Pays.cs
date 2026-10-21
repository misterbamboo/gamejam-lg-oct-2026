// Countries a pepper can come from (the ones with a flag in Images/Flags).
public static class Pays
{
    public const string Bresil = "Brésil";
    public const string Canada = "Canada";
    public const string Chine = "Chine";
    public const string Espagne = "Espagne";
    public const string Inde = "Inde";
    public const string Indonesie = "Indonésie";
    public const string Irak = "Iraq"; // Matches the flag file name in Resources/Drapeau
    public const string Mexique = "Mexique";
    public const string Vietnam = "Vietnam";
    public const string EtatsUnis = "États-Unis";

    public static readonly string[] All =
    {
        Bresil, Canada, Chine, Espagne, Inde, Indonesie, Irak, Mexique, Vietnam,
    };

    public static readonly string[] AmeriqueDuNord = { Canada, EtatsUnis, Mexique };

    public static string Random() => All[UnityEngine.Random.Range(0, All.Length)];
}
