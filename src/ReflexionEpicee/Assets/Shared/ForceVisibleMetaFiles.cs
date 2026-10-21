#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Assets.Shared
{
    // We use Git outside of Unity: undo the Unity Version Control plugin switching the mode back on.
    [InitializeOnLoad]
    internal static class ForceVisibleMetaFiles
    {
        private const string Mode = "Visible Meta Files";
        private const double CheckInterval = 5d;

        private static double nextCheck;

        static ForceVisibleMetaFiles()
        {
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            if (EditorApplication.timeSinceStartup < nextCheck)
            {
                return;
            }

            nextCheck = EditorApplication.timeSinceStartup + CheckInterval;
            if (VersionControlSettings.mode == Mode)
            {
                return;
            }

            Debug.Log($"Version control was '{VersionControlSettings.mode}', switched back to '{Mode}' (we use Git).");
            VersionControlSettings.mode = Mode;
            AssetDatabase.SaveAssets();
        }
    }
}
#endif
