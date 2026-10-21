using Assets.CookingPotDomain.Entities;
using Assets.Shared;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.CookingPotDomain.Scripts
{
    public class CookingPot : MonoBehaviour
    {
        [SerializeField]
        private Transform chiliTransform;

        // Local Y of the chili when the pot is empty and when it is full.
        [SerializeField]
        private float emptyY = -0.226f;

        [SerializeField]
        private float fullY = 0.589f;

        [SerializeField]
        private float levelSpeed = 6f;

        [SerializeField]
        private float ladleSize = 0.3f;

        private readonly ChiliPeppersMix chili = new ChiliPeppersMix();
        private Renderer chiliRenderer;
        private Camera mainCamera;

        public float Shu => chili.Shu;

        // Where a new ladle appears: on top of the chili surface.
        public Vector3 LadleHome
        {
            get
            {
                var bounds = chiliRenderer.bounds;
                return new Vector3(bounds.center.x, bounds.max.y, bounds.center.z);
            }
        }

        private void Awake()
        {
            chiliRenderer = chiliTransform.GetComponent<Renderer>();
            mainCamera = Camera.main;
            SetChiliY(TargetY());
            chiliRenderer.enabled = !chili.IsEmpty;
            RenderChili();
        }

        private void OnEnable()
        {
            EventBus.Subscribe<ChiliServing>(GameEvents.ChiliServed, OnChiliServed);
            EventBus.Subscribe<PimentDrop>(GameEvents.PimentDropped, OnPimentDropped);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<ChiliServing>(GameEvents.ChiliServed, OnChiliServed);
            EventBus.Unsubscribe<PimentDrop>(GameEvents.PimentDropped, OnPimentDropped);
        }

        // A pepper dropped over the pot goes in, unless the pot is full.
        private void OnPimentDropped(PimentDrop drop)
        {
            if (IsUnder(drop.ScreenPosition) && AddPiment(drop.Piment))
            {
                drop.Take();
            }
        }

        private void Update()
        {
            var t = 1f - Mathf.Exp(-levelSpeed * Time.deltaTime);
            SetChiliY(Mathf.Lerp(chiliTransform.localPosition.y, TargetY(), t));

            // Visible while there is chili, and hidden only once the last scoop has finished sinking.
            chiliRenderer.enabled = !chili.IsEmpty || chiliTransform.localPosition.y - emptyY > 0.01f;

            // Any number of ladles can exist: dropped ones lie on the table until picked up again.
            if (!chili.IsEmpty && WasClicked())
            {
                SpawnLadle();
            }
        }

        public bool AddPiment(IPiment piment)
        {
            if (!chili.TryAdd(piment))
            {
                return false;
            }

            RenderChili();
            EventBus.Publish(GameEvents.PimentAdded, piment);
            return true;
        }

        // A ladle dropped back into the pot pours its scoop back in.
        public void PourBack(Ladle returned)
        {
            chili.TryPourBack(returned.Shu);
            RenderChili();
            Destroy(returned.gameObject);
        }

        public bool IsUnder(Vector2 screenPosition)
        {
            var ray = mainCamera.ScreenPointToRay(screenPosition);
            foreach (var hit in Physics.RaycastAll(ray))
            {
                if (hit.transform.IsChildOf(transform))
                {
                    return true;
                }
            }

            return false;
        }

        private void OnChiliServed(ChiliServing serving)
        {
            Debug.Log($"{serving.CustomerName} served chili at {serving.Shu:0.##} SHU, taste {serving.Taste:0.##}/10");
        }

        private bool WasClicked()
        {
            var mouse = Mouse.current;
            if (mouse == null || !mouse.leftButton.wasPressedThisFrame)
            {
                return false;
            }

            var ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue());
            return Physics.Raycast(ray, out var hit) && hit.transform.IsChildOf(transform);
        }

        // Taking a ladle scoops one portion out of the pot right away.
        private void SpawnLadle()
        {
            var shu = chili.Shu;
            if (!chili.TryScoop())
            {
                return;
            }

            RenderChili();

            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = "Ladle";
            sphere.transform.localScale = Vector3.one * ladleSize;
            sphere.transform.position = LadleHome;

            // Its own copy of the chili material, so its color stays that of its scoop.
            var sphereRenderer = sphere.GetComponent<Renderer>();
            sphereRenderer.material = chiliRenderer.material;
            sphereRenderer.material.color = HeatGradient.Evaluate(shu / ChiliPeppersMix.MaxShu);

            sphere.AddComponent<Ladle>().Init(this, mainCamera, shu);
        }

        private void RenderChili()
        {
            chiliRenderer.material.color = HeatGradient.Evaluate(chili.Shu / ChiliPeppersMix.MaxShu);
        }

        private float TargetY() => Mathf.Lerp(emptyY, fullY, chili.Level);

        private void SetChiliY(float y)
        {
            var position = chiliTransform.localPosition;
            position.y = y;
            chiliTransform.localPosition = position;
        }
    }
}
