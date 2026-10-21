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

    public Customer Customer { get; private set; }

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
        for (float elapsed = 0f; elapsed < scaleDuration; elapsed += Time.deltaTime)
        {
            transform.localScale = Vector3.one * Mathf.SmoothStep(from, to, elapsed / scaleDuration);
            yield return null;
        }

        transform.localScale = Vector3.one * to;
        onComplete?.Invoke();
    }
}
