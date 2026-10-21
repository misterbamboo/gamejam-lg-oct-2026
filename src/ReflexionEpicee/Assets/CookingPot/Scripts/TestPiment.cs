using UnityEngine;

namespace Assets.CookingPotDomain.Scripts
{
    // Temporary pepper with random valid values, until Piment implements IPiment.
    public class TestPiment : IPiment
    {
        private const float Epsilon = 0.01f;

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
        public Color DisplayColor { get; private set; }

        public static TestPiment Random()
        {
            return new TestPiment
            {
                Shu = UnityEngine.Random.Range(0, 11),
                Volume = UnityEngine.Random.Range(Epsilon, 1f),
                Dryness = UnityEngine.Random.Range(Epsilon, 1f),
                Bouncy = UnityEngine.Random.Range(Epsilon, 1f),
                Slimy = UnityEngine.Random.Range(Epsilon, 1f),
                Rough = UnityEngine.Random.Range(Epsilon, 1f),
                Soft = UnityEngine.Random.Range(Epsilon, 1f),
                Spiky = UnityEngine.Random.Range(Epsilon, 1f),
                Reflection = UnityEngine.Random.Range(0f, 1f),
                Tint = UnityEngine.Random.Range(0f, 1f),
                DisplayColor = Resources.Load<Material>("Materials/" + (PimentColor)UnityEngine.Random.Range(0, 3)).color,
            };
        }
    }
}
