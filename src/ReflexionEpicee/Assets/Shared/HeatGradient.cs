using UnityEngine;

namespace Assets.Shared
{
    public static class HeatGradient
    {
        private static readonly Color[] Colors =
        {
            Color.green,
            Color.yellow,
            new Color(1f, 0.5f, 0f),
            Color.red,
        };

        // Lerps through green, yellow, orange, red for a ratio between 0 and 1.
        public static Color Evaluate(float ratio)
        {
            var scaled = Mathf.Clamp01(ratio) * (Colors.Length - 1);
            var index = Mathf.Min((int)scaled, Colors.Length - 2);
            return Color.Lerp(Colors[index], Colors[index + 1], scaled - index);
        }
    }
}
