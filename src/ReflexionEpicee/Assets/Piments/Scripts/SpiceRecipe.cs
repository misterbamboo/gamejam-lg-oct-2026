using System;

// The spice level instructions (Piments/Instructions), translated from main.py step by step.
public static class SpiceRecipe
{
    public static float Evaluate(PimentColor color, string pays)
    {
        return FinalLevel(Step1(0f, color, pays));
    }

    private static float Step1(float shu, PimentColor color, string pays)
    {
        if (color == PimentColor.Rouge)
        {
            return Step2(shu + 3f, color, pays);
        }

        if (color == PimentColor.Vert)
        {
            return Step3(shu - 5.5f, color, pays);
        }

        return Step4(shu + 6.1f, color, pays);
    }

    private static float Step2(float shu, PimentColor color, string pays)
    {
        if (pays.StartsWith("I"))
        {
            return Step5(shu + 7.6f, color, pays);
        }

        return Step8(shu - 7.9f, color, pays);
    }

    private static float Step3(float shu, PimentColor color, string pays)
    {
        if (pays == Pays.Inde || pays == Pays.Canada)
        {
            return Step6(shu + 0.7f, color, pays);
        }

        if (pays == Pays.Chine)
        {
            return Step4(shu - 2f, color, pays);
        }

        if (pays == Pays.Indonesie && shu > 9f)
        {
            return 2f;
        }

        if (pays == Pays.Bresil)
        {
            return Step5(shu * 2f, color, pays);
        }

        return Step4(shu * 3f, color, pays);
    }

    private static float Step4(float shu, PimentColor color, string pays)
    {
        // "Le piment a l'air joli" is never true in main.py, so it always goes to 9 or 6.
        return shu > 5f ? Step9(shu, color, pays) : Step6(shu, color, pays);
    }

    private static float Step5(float shu, PimentColor color, string pays)
    {
        if (pays == Pays.Indonesie)
        {
            return shu + (UnityEngine.Random.value < 0.5f ? 3f : -3f);
        }

        return color != PimentColor.Rouge ? shu + 15f : shu + 351f;
    }

    private static float Step6(float shu, PimentColor color, string pays)
    {
        if (shu >= 5f && shu <= 6f)
        {
            return Step1(shu, color, pays);
        }

        if (pays == Pays.Canada)
        {
            return shu / 2f;
        }

        return Step9(shu, color, pays);
    }

    // Step 7 ("répétez votre étape précédente") is only reached from step 8, so it loops back into it.
    private static float Step8(float shu, PimentColor color, string pays)
    {
        shu /= 2f;
        return shu > -2f && shu < 5f ? Step9(shu, color, pays) : Step8(shu, color, pays);
    }

    private static float Step9(float shu, PimentColor color, string pays)
    {
        return Array.IndexOf(Pays.AmeriqueDuNord, pays) >= 0 ? shu + 4.4f : shu - 4.4f;
    }

    // "Si votre niveau d'épice n'est pas entre 0 et 10, enlevez ou ajoutez 10 jusqu'à ce qu'il l'est."
    private static float FinalLevel(float shu)
    {
        while (shu < 0f)
        {
            shu += 10f;
        }

        while (shu > 10f)
        {
            shu -= 10f;
        }

        return shu;
    }
}
