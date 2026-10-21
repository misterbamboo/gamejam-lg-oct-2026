using Assets.Shared;
using UnityEngine;

namespace Assets.CookingPotDomain.Scripts
{
    // A scoop of chili: dragged out of the pot onto a customer, or dropped and left to physics.
    public class Ladle : MonoBehaviour
    {
        private CookingPot cookingPot;
        private bool isReleasing;
        private bool isServed;

        // Heat of the chili when it was scooped.
        public float Shu { get; private set; }

        // Color of the chili when it was scooped.
        public Color Color { get; private set; }

        // Starts held, so the click that created it keeps dragging it.
        public void Init(CookingPot pot, float shu, Color color)
        {
            cookingPot = pot;
            Shu = shu;
            Color = color;

            var grabbable = gameObject.AddComponent<Grabbable>();
            grabbable.Dragged += OnDragged;
            grabbable.Released += OnReleased;
            grabbable.Grab();
        }

        private void OnEnable()
        {
            EventBus.Subscribe<ChiliServing>(GameEvents.ChiliServed, OnChiliServed);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<ChiliServing>(GameEvents.ChiliServed, OnChiliServed);
        }

        // Several ladles may exist; only the one being released is the one served.
        private void OnChiliServed(ChiliServing serving)
        {
            if (isReleasing)
            {
                isServed = true;
            }
        }

        private void OnDragged(Vector2 screenPosition)
        {
            EventBus.Publish(GameEvents.LadleDragged, screenPosition);
        }

        // A customer under the mouse takes it; else the pot gets it back; else it falls with physics.
        private void OnReleased(Vector2 screenPosition)
        {
            isReleasing = true;
            EventBus.Publish(GameEvents.LadleReleased, new LadleDrop(screenPosition, Shu));
            isReleasing = false;

            if (isServed)
            {
                Destroy(gameObject);
            }
            else if (cookingPot.IsUnder(screenPosition))
            {
                cookingPot.PourBack(this);
            }
        }
    }
}
