using UnityEngine;

namespace Assets.Shared
{
    public static class GameEvents
    {
        // Payload: IPiment
        public const string PimentAdded = "PimentAdded";

        // Payload: Vector2 screen position of the ladle
        public const string LadleDragged = "LadleDragged";

        // Payload: LadleDrop
        public const string LadleReleased = "LadleReleased";

        // Payload: ChiliServing
        public const string ChiliServed = "ChiliServed";
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
