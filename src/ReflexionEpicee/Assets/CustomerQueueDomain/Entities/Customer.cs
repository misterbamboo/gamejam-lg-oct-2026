namespace Assets.CustomerQueueDomain.Entities
{
    public class Customer
    {
        public Customer(string name, PreferenceRange preferenceRange)
        {
            Name = name;
            Preference = preferenceRange;
        }

        public string Name { get; private set; }

        public PreferenceRange Preference { get; private set; }

        internal static Customer Regular()
        {
            var rangeSize = RoundToQuarter(UnityEngine.Random.Range(3f, 5f));
            var min = RoundToQuarter(UnityEngine.Random.Range(0f, 5f - rangeSize));
            var max = min + rangeSize;

            return new Customer(CustomerNames.Random(), new PreferenceRange(min, max));
        }

        private static float RoundToQuarter(float value)
        {
            return UnityEngine.Mathf.Round(value * 4f) / 4f;
        }
    }
}