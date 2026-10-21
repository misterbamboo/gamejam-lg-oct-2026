using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Shared
{
    // Mouse drag for a physics object: held kinematic while dragged, then thrown back into the physics simulation.
    [RequireComponent(typeof(Rigidbody))]
    public class Grabbable : MonoBehaviour
    {
        [SerializeField]
        private float maxThrowSpeed = 6f;

        private Rigidbody body;
        private Camera mainCamera;
        private float dragDepth;
        private Vector3 dragVelocity;

        // Screen position of the mouse.
        public event Action<Vector2> Dragged;
        public event Action<Vector2> Released;

        public bool IsDragging { get; private set; }

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            mainCamera = Camera.main;
        }

        public void Grab()
        {
            IsDragging = true;
            body.isKinematic = true;
            dragVelocity = Vector3.zero;
            dragDepth = mainCamera.WorldToScreenPoint(transform.position).z;
        }

        private void Update()
        {
            var mouse = Mouse.current;
            if (mouse == null)
            {
                return;
            }

            var screenPosition = mouse.position.ReadValue();

            if (!IsDragging)
            {
                if (mouse.leftButton.wasPressedThisFrame && IsUnder(screenPosition))
                {
                    Grab();
                }

                return;
            }

            // Keep the same distance from the camera so the object doesn't jump in depth.
            var target = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, dragDepth));
            if (Time.deltaTime > 0f)
            {
                dragVelocity = (target - transform.position) / Time.deltaTime;
            }

            transform.position = target;
            Dragged?.Invoke(screenPosition);

            if (mouse.leftButton.wasReleasedThisFrame)
            {
                IsDragging = false;
                body.isKinematic = false;

                // Carry the mouse movement so a quick flick tosses the object.
                body.linearVelocity = Vector3.ClampMagnitude(dragVelocity, maxThrowSpeed);
                Released?.Invoke(screenPosition);
            }
        }

        private bool IsUnder(Vector2 screenPosition)
        {
            var ray = mainCamera.ScreenPointToRay(screenPosition);
            return Physics.Raycast(ray, out var hit) && hit.transform.IsChildOf(transform);
        }
    }
}
