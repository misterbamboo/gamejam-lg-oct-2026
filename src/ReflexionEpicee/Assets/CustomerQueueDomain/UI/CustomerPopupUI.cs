using Assets.CustomerQueueDomain.Entities;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CustomerPopupUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private TMP_Text preferenceText;

    [SerializeField]
    private PreferenceRangeBarUI preferenceRangeBar;

    [SerializeField]
    private float scaleDuration = 0.25f;

    [SerializeField]
    private float highlightScale = 1.06f;

    [SerializeField]
    private float highlightSpeed = 15f;

    private bool isAnimating;
    private bool isHighlighted;

    public Customer Customer { get; private set; }

    // Grows slightly while a ladle hovers over this card, unless appear/disappear is playing.
    private void Update()
    {
        if (isAnimating)
        {
            return;
        }

        var t = 1f - Mathf.Exp(-highlightSpeed * Time.deltaTime);
        var target = Vector3.one * (isHighlighted ? highlightScale : 1f);
        transform.localScale = Vector3.Lerp(transform.localScale, target, t);
    }

    public void SetHighlighted(bool highlighted)
    {
        isHighlighted = highlighted;
    }

    public void Render(Customer customer)
    {
        Customer = customer;
        nameText.text = customer.Name;
        preferenceText.text = customer.Reflexion;
        preferenceRangeBar.Render(customer.Preference);
    }

    public void ResetVisual()
    {
        StopAllCoroutines();
        isAnimating = false;
        isHighlighted = false;
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
    }

    public void PlayAppear()
    {
        StopAllCoroutines();
        StartCoroutine(AnimateScale(0f, 1f, null));
    }

    public void PlayDisappear(Action onComplete)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateScale(transform.localScale.x, 0f, onComplete));
    }

    private IEnumerator AnimateScale(float from, float to, Action onComplete)
    {
        isAnimating = true;
        for (float elapsed = 0f; elapsed < scaleDuration; elapsed += Time.deltaTime)
        {
            transform.localScale = Vector3.one * Mathf.SmoothStep(from, to, elapsed / scaleDuration);
            yield return null;
        }

        transform.localScale = Vector3.one * to;
        isAnimating = false;
        onComplete?.Invoke();
    }
}
