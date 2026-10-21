using Assets.Shared;
using UnityEngine;
using UnityEngine.InputSystem;

// Lets the player drag a pepper and drop it into the cooking pot (or on the table).
[RequireComponent(typeof(Piment), typeof(BoxCollider))]
public class DraggablePiment : MonoBehaviour
{
    private Piment piment;
    private Renderer pimentRenderer;
    private Camera mainCamera;
    private bool isDragging;
    private float dragDepth;
    private float fallSpeed;

    private void Awake()
    {
        piment = GetComponent<Piment>();
        pimentRenderer = GetComponentInChildren<Renderer>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null)
        {
            return;
        }

        var screenPosition = mouse.position.ReadValue();

        if (!isDragging)
        {
            TableFall.Step(transform, pimentRenderer, ref fallSpeed);

            if (mouse.leftButton.wasPressedThisFrame && IsUnder(screenPosition))
            {
                isDragging = true;
                dragDepth = mainCamera.WorldToScreenPoint(transform.position).z;
            }

            return;
        }

        // Keep the same distance from the camera so the pepper doesn't jump in depth.
        transform.position = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, dragDepth));

        if (mouse.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
            fallSpeed = 0f;

            // The pot takes it if dropped over it and not full; otherwise it falls on the table.
            var drop = new PimentDrop(screenPosition, piment);
            EventBus.Publish(GameEvents.PimentDropped, drop);
            if (drop.IsTaken)
            {
                Destroy(gameObject);
            }
        }
    }

    private bool IsUnder(Vector2 screenPosition)
    {
        var ray = mainCamera.ScreenPointToRay(screenPosition);
        return Physics.Raycast(ray, out var hit) && hit.transform.IsChildOf(transform);
    }
}
