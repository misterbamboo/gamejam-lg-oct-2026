using Assets.CookingPotDomain.Entities;
using Assets.Shared;
using TMPro;
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

        [SerializeField]
        private float colorSpeed = 3f;

        // Optional: a TextMeshPro (3D) placed by hand; one is created above the pot otherwise.
        [SerializeField]
        private TMP_Text shuLabel;

        [SerializeField]
        private float labelHeight = 0.3f;

        // Pushes the label behind the pot (away from the camera, horizontally) so a tilted camera doesn't see it overlap the pot.
        [SerializeField]
        private float labelDepth = 1f;

        [SerializeField]
        private float labelFontSize = 3f;

        // Same dark blue as the CustomerPopup texts.
        [SerializeField]
        private Color labelColor = new Color(0f, 0.043f, 0.361f);

        private readonly ChiliPeppersMix chili = new ChiliPeppersMix();
        private Renderer chiliRenderer;
        private Camera mainCamera;
        private Color displayedColor;

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
            // Before hiding the chili: a disabled renderer has empty bounds.
            if (shuLabel == null)
            {
                shuLabel = CreateShuLabel();
            }

            SetChiliY(TargetY());
            chiliRenderer.enabled = !chili.IsEmpty;
            RenderChili();
        }

        private TMP_Text CreateShuLabel()
        {
            // Float just above the highest part of the pot.
            var bounds = chiliRenderer.bounds;
            foreach (var part in GetComponentsInChildren<Renderer>())
            {
                bounds.Encapsulate(part.bounds);
            }

            var label = new GameObject("ShuLabel").AddComponent<TextMeshPro>();
            label.transform.SetParent(transform, true);
            var awayFromCamera = Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up).normalized;
            label.transform.position = new Vector3(bounds.center.x, bounds.max.y + labelHeight, bounds.center.z) + awayFromCamera * labelDepth;
            label.rectTransform.sizeDelta = new Vector2(3f, 1f);
            label.alignment = TextAlignmentOptions.Center;
            label.fontSize = labelFontSize;
            label.fontStyle = FontStyles.Bold;
            label.color = labelColor;
            label.outlineWidth = 0.2f;
            label.outlineColor = Color.white;
            return label;
        }

        // Billboard: the label always faces the camera.
        private void LateUpdate()
        {
            shuLabel.transform.rotation = mainCamera.transform.rotation;
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

            // Shift gradually towards the new mix after each pepper.
            if (!chili.IsEmpty)
            {
                displayedColor = Color.Lerp(displayedColor, chili.Color, 1f - Mathf.Exp(-colorSpeed * Time.deltaTime));
                chiliRenderer.material.color = displayedColor;
            }

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
            chili.TryPourBack(returned.Shu, returned.Color);
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
            var color = chili.Color;
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
            sphereRenderer.material.color = color;

            sphere.AddComponent<Ladle>().Init(this, shu, color);
        }

        private void RenderChili()
        {
            shuLabel.text = chili.IsEmpty ? "Vide" : $"{chili.Shu:0.#} SHU";

            // The first pepper in an empty pot sets the color directly instead of fading from the old one.
            if (chili.Portions == 1)
            {
                displayedColor = chili.Color;
            }
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
