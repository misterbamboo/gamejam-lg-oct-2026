namespace Assets.CustomerQueueDomain.Entities
{
    public static class CustomerNames
    {
        private static readonly string[] All =
        {
            // Female
            "Marie", "Julie", "Isabelle", "Nathalie", "Sophie",
            "Geneviève", "Mélanie", "Catherine", "Véronique", "Annie",
            "Chantal", "Josée", "Manon", "Sylvie", "Johanne",
            "Lucie", "Diane", "Francine", "Ginette", "Louise",
            "Jocelyne", "Lise", "Nicole", "Monique", "Céline",
            "Thérèse", "Gisèle", "Huguette", "Rollande", "Pierrette",
            "Marie-Ève", "Marie-Claude", "Marie-Pier", "Audrey", "Émilie",
            "Camille", "Léa", "Rosalie", "Florence", "Laurence",
            "Béatrice", "Noémie", "Maude", "Karine", "Valérie",
            "Stéphanie", "Mylène", "Amélie", "Josiane", "Ariane",

            // Male
            "Jean", "Pierre", "Michel", "André", "Claude",
            "Jacques", "Gilles", "Réjean", "Gaétan", "Marcel",
            "Normand", "Yvon", "Denis", "Luc", "Richard",
            "Daniel", "Alain", "Serge", "Sylvain", "Stéphane",
            "Martin", "Éric", "François", "Patrick", "Sébastien",
            "Mathieu", "Jean-François", "Jean-Philippe", "Marc-André", "Maxime",
            "Simon", "Guillaume", "Olivier", "Alexandre", "Samuel",
            "Gabriel", "William", "Félix", "Thomas", "Antoine",
            "Louis", "Émile", "Raphaël", "Xavier", "Ludovic",
            "Benoît", "Yannick", "Jonathan", "Ghislain", "Rosaire",
        };

        internal static string Random()
        {
            return All[UnityEngine.Random.Range(0, All.Length)];
        }
    }
}
