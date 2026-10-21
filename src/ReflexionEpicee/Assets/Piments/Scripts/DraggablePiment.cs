using Assets.Shared;
using UnityEngine;

// Lets the player drag a pepper and drop it into the cooking pot (or let it fall with physics).
[RequireComponent(typeof(Piment), typeof(BoxCollider), typeof(Grabbable))]
public class DraggablePiment : MonoBehaviour
{
    private Piment piment;
    private Grabbable grabbable;

    private void Awake()
    {
        piment = GetComponent<Piment>();

        // RequireComponent doesn't add Grabbable to prefabs that already had this component.
        grabbable = GetComponent<Grabbable>();
        if (grabbable == null)
        {
            grabbable = gameObject.AddComponent<Grabbable>();
        }
    }

    private void OnEnable()
    {
        grabbable.Released += OnReleased;
    }

    private void OnDisable()
    {
        grabbable.Released -= OnReleased;
    }

    // The pot takes it if dropped over it and not full; otherwise physics takes over.
    private void OnReleased(Vector2 screenPosition)
    {
        var drop = new PimentDrop(screenPosition, piment);
        EventBus.Publish(GameEvents.PimentDropped, drop);
        if (drop.IsTaken)
        {
            Destroy(gameObject);
        }
    }
}
