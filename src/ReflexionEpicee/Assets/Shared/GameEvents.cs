using UnityEngine;

namespace Assets.Shared
{
    public static class GameEvents
    {
        // Payload: IPiment
        public const string PimentAdded = "PimentAdded";

        // Payload: PimentDrop
        public const string PimentDropped = "PimentDropped";

        // Payload: Vector2 screen position of the ladle
        public const string LadleDragged = "LadleDragged";

        // Payload: LadleDrop
        public const string LadleReleased = "LadleReleased";

        // Payload: ChiliServing
        public const string ChiliServed = "ChiliServed";
    }

    public class PimentDrop
    {
        public Vector2 ScreenPosition { get; private set; }
        public IPiment Piment { get; private set; }

        // Set by whoever accepts the pepper, so the dropper knows it is gone.
        public bool IsTaken { get; private set; }

        public PimentDrop(Vector2 screenPosition, IPiment piment)
        {
            ScreenPosition = screenPosition;
            Piment = piment;
        }

        public void Take()
        {
            IsTaken = true;
        }
    }

    public class LadleDrop
    {
        public Vector2 ScreenPosition { get; private set; }
        public float Shu { get; private set; }

        public LadleDrop(Vector2 screenPosition, float shu)
        {
            ScreenPosition = screenPosition;
            Shu = shu;
        }
    }

    public class ChiliServing
    {
        public string CustomerName { get; private set; }
        public float Shu { get; private set; }
        public float Taste { get; private set; }

        public ChiliServing(string customerName, float shu, float taste)
        {
            CustomerName = customerName;
            Shu = shu;
            Taste = taste;
        }
    }
}
