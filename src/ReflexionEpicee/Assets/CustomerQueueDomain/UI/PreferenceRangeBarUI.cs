using Assets.CustomerQueueDomain.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreferenceRangeBarUI : MonoBehaviour
{
    private static readonly Color[] HeatColors =
    {
        Color.green,
        Color.yellow,
        new Color(1f, 0.5f, 0f),
        Color.red,
    };

    [SerializeField]
    private RectTransform rangeFill;

    [SerializeField]
    private TMP_Text minLabel;

    [SerializeField]
    private TMP_Text maxLabel;

    [SerializeField]
    private float labelSpacing = 2f;

    public void Render(PreferenceRange preference)
    {
        var minRatio = Mathf.Clamp01(preference.Min / PreferenceRange.MaxHeat);
        var maxRatio = Mathf.Clamp01(preference.Max / PreferenceRange.MaxHeat);

        // The fill stretches between min and max as a fraction of the background width.
        rangeFill.anchorMin = new Vector2(minRatio, 0f);
        rangeFill.anchorMax = new Vector2(maxRatio, 1f);
        rangeFill.offsetMin = Vector2.zero;
        rangeFill.offsetMax = Vector2.zero;
        rangeFill.GetComponent<Graphic>().color = HeatColor(preference.Mid / PreferenceRange.MaxHeat);

        PlaceLabel(minLabel, minRatio, preference.Min);
        PlaceLabel(maxLabel, maxRatio, preference.Max);
    }

    // Labels sit just under the bar, centered on the edges of the fill.
    private void PlaceLabel(TMP_Text label, float ratio, float value)
    {
        label.text = value.ToString();

        var rect = label.rectTransform;
        rect.anchorMin = new Vector2(ratio, 0f);
        rect.anchorMax = new Vector2(ratio, 0f);
        rect.pivot = new Vector2(0.5f, 1f);
        rect.anchoredPosition = new Vector2(0f, -labelSpacing);
    }

    // Lerps through green, yellow, orange, red.
    private static Color HeatColor(float ratio)
    {
        var scaled = Mathf.Clamp01(ratio) * (HeatColors.Length - 1);
        var index = Mathf.Min((int)scaled, HeatColors.Length - 2);
        return Color.Lerp(HeatColors[index], HeatColors[index + 1], scaled - index);
    }
}
