namespace Assets.CustomerQueueDomain.Entities
{
    public class Customer
    {
        public Customer(string name, PreferenceRange preferenceRange, string reflexion)
        {
            Name = name;
            Preference = preferenceRange;
            Reflexion = reflexion;
        }

        public string Name { get; private set; }

        public PreferenceRange Preference { get; private set; }

        public string Reflexion { get; private set; }

        internal static Customer Regular()
        {
            var rangeSize = RoundToQuarter(UnityEngine.Random.Range(3f, 5f));
            var min = RoundToQuarter(UnityEngine.Random.Range(0f, 5f - rangeSize));
            var max = min + rangeSize;
            var preference = new PreferenceRange(min, max);

            return new Customer(CustomerNames.Random(), preference, CustomerReflexions.RandomAbout(preference));
        }

        private static float RoundToQuarter(float value)
        {
            return UnityEngine.Mathf.Round(value * 4f) / 4f;
        }
    }
}
