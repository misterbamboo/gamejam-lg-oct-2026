using Assets.CustomerQueueDomain.Entities;
using Assets.CustomerQueueDomain.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerPopupUIManager : MonoBehaviour
{
    [SerializeField]
    public CustomerPopupUI customerPopupUIPrefab;

    [SerializeField]
    public CustomerQueue customerQueue;

    [SerializeField]
    private float margin = 10f;

    [SerializeField]
    private float moveSpeed = 12f;

    private readonly List<CustomerPopupUI> usedPopups = new List<CustomerPopupUI>();
    private readonly Stack<CustomerPopupUI> unusedPopups = new Stack<CustomerPopupUI>();

    private void OnEnable()
    {
        customerQueue.CustomerAdded += OnCustomerAdded;
        customerQueue.CustomerRemoved += OnCustomerRemoved;
    }

    private void OnDisable()
    {
        customerQueue.CustomerAdded -= OnCustomerAdded;
        customerQueue.CustomerRemoved -= OnCustomerRemoved;
    }

    // Slides every used popup towards its slot, so remaining cards close the gap smoothly.
    private void Update()
    {
        var t = 1f - Mathf.Exp(-moveSpeed * Time.deltaTime);
        float offsetX = margin;
        foreach (var popup in usedPopups)
        {
            var rect = (RectTransform)popup.transform;
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, SlotPosition(rect, offsetX), t);
            offsetX += rect.rect.width + margin;
        }
    }

    private void OnCustomerAdded(Customer customer)
    {
        float offsetX = margin;
        foreach (var used in usedPopups)
        {
            offsetX += ((RectTransform)used.transform).rect.width + margin;
        }

        var popup = Get();
        popup.Render(customer);

        // Popups are anchored top-right; rebuild first so content-driven sizes are up to date.
        var rect = (RectTransform)popup.transform;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        rect.anchoredPosition = SlotPosition(rect, offsetX);

        usedPopups.Add(popup);
        popup.PlayAppear();
    }

    private void OnCustomerRemoved(Customer customer)
    {
        var popup = usedPopups.Find(p => p.Customer == customer);
        if (popup == null)
        {
            return;
        }

        // Keep the slot while shrinking, then free it so the remaining cards slide over.
        popup.PlayDisappear(() =>
        {
            usedPopups.Remove(popup);
            Release(popup);
        });
    }

    // Stack popups from the top-right corner towards the left, first customer rightmost.
    private Vector2 SlotPosition(RectTransform rect, float offsetX)
    {
        var size = rect.rect.size;
        return new Vector2(
            -offsetX - size.x * (1f - rect.pivot.x),
            -margin - size.y * (1f - rect.pivot.y));
    }

    private CustomerPopupUI Get()
    {
        CustomerPopupUI popup;
        if (unusedPopups.Count > 0)
        {
            popup = unusedPopups.Pop();
        }
        else
        {
            popup = Instantiate(customerPopupUIPrefab, transform);

            // Centered pivot so appear/disappear scaling happens from the middle of the card.
            ((RectTransform)popup.transform).pivot = new Vector2(0.5f, 0.5f);
        }

        popup.ResetVisual();
        return popup;
    }

    private void Release(CustomerPopupUI popup)
    {
        popup.gameObject.SetActive(false);
        unusedPopups.Push(popup);
    }
}
