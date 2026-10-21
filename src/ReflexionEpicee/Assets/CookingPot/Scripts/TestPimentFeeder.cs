using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.CookingPotDomain.Scripts
{
    // Temporary: fills the pot at start and adds a random pepper when P is pressed.
    public class TestPimentFeeder : MonoBehaviour
    {
        [SerializeField]
        private CookingPot cookingPot;

        [SerializeField]
        private int pimentsOnStart = 10;

        private void Start()
        {
            for (int i = 0; i < pimentsOnStart; i++)
            {
                cookingPot.AddPiment(TestPiment.Random());
            }
        }

        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
            {
                cookingPot.AddPiment(TestPiment.Random());
            }
        }
    }
}
