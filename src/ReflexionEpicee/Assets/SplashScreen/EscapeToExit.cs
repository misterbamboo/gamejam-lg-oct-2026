using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Escape in game goes back to the splash screen; Escape on the splash screen quits.
public class EscapeToExit : MonoBehaviour
{
    private const string SplashScene = "SplashScreenScene";

    // Created automatically, so nothing has to be added to any scene.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Create()
    {
        var exit = new GameObject(nameof(EscapeToExit));
        exit.AddComponent<EscapeToExit>();
        DontDestroyOnLoad(exit);
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null || !keyboard.escapeKey.wasPressedThisFrame)
        {
            return;
        }

        if (SceneManager.GetActiveScene().name != SplashScene)
        {
            SceneManager.LoadScene(SplashScene);
            return;
        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
