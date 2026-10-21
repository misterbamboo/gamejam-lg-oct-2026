using Assets.Shared;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.CookingPotDomain.Scripts
{
    // A scoop of chili: dragged out of the pot onto a customer, or dropped on the table.
    public class Ladle : MonoBehaviour
    {
        [SerializeField]
        private float gravity = 20f;

        // The table surface the ladle falls onto when released over nobody.
        [SerializeField]
        private float tableY = 0f;

        private CookingPot cookingPot;
        private Camera mainCamera;
        private bool isDragging;
        private float dragDepth;
        private float fallSpeed;
        private bool isReleasing;
        private bool isServed;

        // Heat of the chili when it was scooped.
        public float Shu { get; private set; }

        // Starts held, so the click that created it keeps dragging it.
        public void Init(CookingPot pot, Camera camera, float shu)
        {
            cookingPot = pot;
            mainCamera = camera;
            Shu = shu;
            StartDragging();
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
                Fall();

                if (mouse.leftButton.wasPressedThisFrame && IsUnder(screenPosition))
                {
                    StartDragging();
                }

                return;
            }

            // Keep the same distance from the camera so the ladle doesn't jump in depth.
            transform.position = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, dragDepth));
            EventBus.Publish(GameEvents.LadleDragged, screenPosition);

            if (mouse.leftButton.wasReleasedThisFrame)
            {
                isDragging = false;
                fallSpeed = 0f;

                // A customer under the mouse takes it; else the pot gets it back; else it falls on the table.
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

        private void StartDragging()
        {
            isDragging = true;
            dragDepth = mainCamera.WorldToScreenPoint(transform.position).z;
        }

        private void Fall()
        {
            // Rest with the bottom of the sphere touching the table.
            var restY = tableY + transform.lossyScale.y * 0.5f;
            var position = transform.position;
            if (position.y <= restY)
            {
                // Also lifts it back up if it was dropped below the table.
                position.y = restY;
                transform.position = position;
                return;
            }

            fallSpeed += gravity * Time.deltaTime;
            position.y = Mathf.Max(restY, position.y - fallSpeed * Time.deltaTime);
            transform.position = position;
        }

        private bool IsUnder(Vector2 screenPosition)
        {
            var ray = mainCamera.ScreenPointToRay(screenPosition);
            return Physics.Raycast(ray, out var hit) && hit.transform == transform;
        }
    }
}
