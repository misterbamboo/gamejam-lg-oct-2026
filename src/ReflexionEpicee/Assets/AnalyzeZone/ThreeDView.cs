using UnityEngine;
using UnityEngine.InputSystem;

// Hold the right mouse button and use W/A/S/D/Q/E to turn the object and look at it from every side.
public class ThreeDView : MonoBehaviour
{
    // Degrees per second.
    [SerializeField]
    float rotationSpeed = 90f;

    public void Update()
    {
        var mouse = Mouse.current;
        var keyboard = Keyboard.current;
        if (mouse == null || keyboard == null || !mouse.rightButton.isPressed)
        {
            return;
        }

        var rotation = Vector3.zero;
        if (keyboard.dKey.isPressed)
            rotation.y += 1f;
        if (keyboard.aKey.isPressed)
            rotation.y -= 1f;
        if (keyboard.wKey.isPressed)
            rotation.x += 1f;
        if (keyboard.sKey.isPressed)
            rotation.x -= 1f;
        if (keyboard.qKey.isPressed)
            rotation.z += 1f;
        if (keyboard.eKey.isPressed)
            rotation.z -= 1f;

        transform.Rotate(rotation * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
