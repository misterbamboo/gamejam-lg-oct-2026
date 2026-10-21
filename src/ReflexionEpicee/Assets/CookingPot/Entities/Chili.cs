namespace Assets.CookingPotDomain.Entities
{
    public class Chili
    {
        public const int Capacity = 10;

        public int Portions { get; private set; }

        // Average Shu of every pepper added since the pot was last empty.
        public float Shu { get; private set; }

        public float Level => (float)Portions / Capacity;
        public bool IsFull => Portions >= Capacity;
        public bool IsEmpty => Portions <= 0;

        public bool TryAdd(IPiment piment)
        {
            if (IsFull)
            {
                return false;
            }

            Shu = (Shu * Portions + piment.Shu) / (Portions + 1);
            Portions++;
            return true;
        }

        // A scoop takes one portion out; the remaining chili keeps the same heat.
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
            }

            return true;
        }
    }
}
