using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad = "Version1";

    // Hooked to the Play button's OnClick.
    public void Play()
    {
        // A scene can only be loaded by name once it is in the Build Profiles scene list.
        if (!Application.CanStreamedLevelBeLoaded(sceneToLoad))
        {
            Debug.LogError($"Scene '{sceneToLoad}' is not in the Build Profiles scene list (File > Build Profiles > Scene List).");
            return;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
