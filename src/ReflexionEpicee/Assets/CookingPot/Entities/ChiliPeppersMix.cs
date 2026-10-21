using UnityEngine;

namespace Assets.CookingPotDomain.Entities
{
    public class ChiliPeppersMix
    {
        public const int Capacity = 10;
        public const float MaxShu = 10f;

        // Keeps Log() finite for color channels at 0.
        private const float MinChannel = 0.01f;

        // Average of the log of each color channel: a geometric mean mixes like paint,
        // so red + red stays red but red + green turns brown.
        private Vector3 logColor;

        public int Portions { get; private set; }

        // Average Shu of every pepper added since the pot was last empty.
        public float Shu { get; private set; }

        public Color Color => IsEmpty
            ? Color.clear
            : new Color(Mathf.Exp(logColor.x), Mathf.Exp(logColor.y), Mathf.Exp(logColor.z));

        public float Level => (float)Portions / Capacity;
        public bool IsFull => Portions >= Capacity;
        public bool IsEmpty => Portions <= 0;

        public bool TryAdd(IPiment piment) => TryMix(piment.Shu, piment.DisplayColor);

        // A scoop poured back mixes in with its own heat and color.
        public bool TryPourBack(float shu, Color color) => TryMix(shu, color);

        private bool TryMix(float shu, Color color)
        {
            if (IsFull)
            {
                return false;
            }

            var log = new Vector3(
                Mathf.Log(Mathf.Max(color.r, MinChannel)),
                Mathf.Log(Mathf.Max(color.g, MinChannel)),
                Mathf.Log(Mathf.Max(color.b, MinChannel)));

            Shu = (Shu * Portions + shu) / (Portions + 1);
            logColor = (logColor * Portions + log) / (Portions + 1);
            Portions++;
            return true;
        }

        // A scoop takes one portion out; the remaining chili keeps the same heat and color.
        public bool TryScoop()
        {
            if (IsEmpty)
            {
                return false;
            }

            Portions--;
            if (IsEmpty)
            {
                Shu = 0f;
                logColor = Vector3.zero;
            }

            return true;
        }
    }
}
