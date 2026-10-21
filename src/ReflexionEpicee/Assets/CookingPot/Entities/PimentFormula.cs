using UnityEngine;

namespace Assets.CookingPotDomain.Entities
{
    // Formulas from Shared/User Enjoyment Formula.txt.
    public static class PimentFormula
    {
        public static float Surface(IPiment piment) => Mathf.Abs(piment.Rough - piment.Soft) + piment.Spiky;

        public static float Dilution(IPiment piment) => piment.Volume / (piment.Volume + piment.Dryness);

        public static float Texture(IPiment piment)
        {
            var exponent = Mathf.Abs(piment.Reflection - piment.Refraction);
            return Mathf.Sqrt(Mathf.Pow(piment.Bouncy * piment.Slimy * Surface(piment), exponent));
        }

        // 0 to 10
        public static float Heat(IPiment piment)
        {
            var wave = Mathf.Sin(Dilution(piment)) * Mathf.Cos(Texture(piment)) - piment.Tint;
            return 10f * Mathf.Abs(wave) / (1f + Mathf.Sin(1f));
        }

        public static float Color(IPiment piment)
        {
            return piment.Tint * piment.Reflection + piment.Refraction * piment.Tint * (1f - piment.Reflection);
        }
    }
}
