using Assets.Shared;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanierManager : MonoBehaviour
{
    [SerializeField]
    Camera Camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Use the hit variable to determine what was clicked on.
                if (hit.transform.tag == "panier")
                {
                    EventBus.Publish(GameEvents.PimentRequested, new PimentRequested());
                    Debug.Log("Piment requested");
                }
            }
        }
    }
}
