using Assets.CookingPotDomain.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

// Debug only: press F1 to show the values of the last pepper clicked, top-left of the screen.
public class PimentDebugPanel : MonoBehaviour
{
    private const float Width = 320f;

    private bool isVisible;
    private Piment selected;
    private GUIStyle labelStyle;

    // Created automatically in the editor and development builds, so nothing has to be added to a scene.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Create()
    {
        if (!Debug.isDebugBuild)
        {
            return;
        }

        var panel = new GameObject(nameof(PimentDebugPanel));
        panel.AddComponent<PimentDebugPanel>();
        DontDestroyOnLoad(panel);
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        var mouse = Mouse.current;

        // F1 rather than D: W/A/S/D turn the pepper (ThreeDView).
        if (keyboard != null && keyboard.f1Key.wasPressedThisFrame)
        {
            isVisible = !isVisible;
        }

        var camera = Camera.main;
        if (mouse == null || camera == null || !mouse.leftButton.wasPressedThisFrame)
        {
            return;
        }

        var ray = camera.ScreenPointToRay(mouse.position.ReadValue());
        if (Physics.Raycast(ray, out var hit))
        {
            var piment = hit.transform.GetComponentInParent<Piment>();
            if (piment != null)
            {
                selected = piment;
            }
        }
    }

    private void OnGUI()
    {
        if (!isVisible)
        {
            return;
        }

        if (labelStyle == null)
        {
            labelStyle = new GUIStyle(GUI.skin.label) { fontSize = 15, richText = true };
        }

        GUILayout.BeginArea(new Rect(10f, 10f, Width, Screen.height - 20f));
        GUILayout.BeginVertical(GUI.skin.box);
        Line("<b>DEBUG PIMENT</b>  (F1 pour fermer)");

        // A pepper dropped in the pot is destroyed; Unity's == null catches that.
        if (selected == null)
        {
            Line("Cliquez sur un piment.");
        }
        else
        {
            Line($"<b>{selected.Nom}</b>");
            Line($"Pays: {selected.Pays}");
            Line($"SHU (recette): <b>{selected.Shu:0.##}</b>");
            Line(" ");
            Line($"Volume: {selected.Volume:0.##}    Dryness: {selected.Dryness:0.##}");
            Line($"Bouncy: {selected.Bouncy:0.##}    Slimy: {selected.Slimy:0.##}");
            Line($"Rough: {selected.Rough:0.##}    Soft: {selected.Soft:0.##}");
            Line($"Spiky: {selected.Spiky:0.##}    Tint: {selected.Tint:0.##}");
            Line($"Reflection: {selected.Reflection:0.##}    Refraction: {selected.Refraction:0.##}");
            Line(" ");
            Line($"Surface: {PimentFormula.Surface(selected):0.##}    Dilution: {PimentFormula.Dilution(selected):0.##}");
            Line($"Texture: {PimentFormula.Texture(selected):0.##}");
            Line($"Heat (formule): {PimentFormula.Heat(selected):0.##}");
            Line($"Color (formule): {PimentFormula.Color(selected):0.##}");
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void Line(string text)
    {
        GUILayout.Label(text, labelStyle);
    }
}
