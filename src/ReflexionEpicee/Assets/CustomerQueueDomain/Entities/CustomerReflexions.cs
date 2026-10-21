namespace Assets.CustomerQueueDomain.Entities
{
    public static class CustomerReflexions
    {
        // {0} = min, {1} = max
        private static readonly string[] PreferenceSentences =
        {
            "Mmmm ... je préfère les piments d'une force de {0} à {1}",
            "Moi, j'aime ça entre {0} et {1}, pas plus, pas moins!",
            "Donne-moi quelque chose entre {0} et {1}, s'il vous plaît.",
            "Pas trop fort, pas trop doux : entre {0} et {1}, c'est parfait.",
            "Coudonc, t'aurais-tu un piment entre {0} et {1}?",
            "J'ai le goût d'un piment de force {0} à {1} aujourd'hui.",
            "Entre {0} et {1}, sinon j'pleure ou j'm'ennuie!",
            "Mon médecin dit pas plus que {1}... mais au moins {0}!",
            "Ça me prend un piment entre {0} et {1}, pis vite!",
            "Je cherche un petit piquant entre {0} et {1}, t'as ça?",
            "Pour moi, ce serait entre {0} et {1}, merci ben!",
            "Chu pas difficile : n'importe quoi entre {0} et {1}.",
        };

        internal static string RandomAbout(PreferenceRange preference)
        {
            var sentence = PreferenceSentences[UnityEngine.Random.Range(0, PreferenceSentences.Length)];
            return string.Format(sentence, preference.Min, preference.Max);
        }
    }
}
