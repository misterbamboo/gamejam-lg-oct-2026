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

    private readonly List<CustomerPopupUI> usedPopups = new List<CustomerPopupUI>();
    private readonly Stack<CustomerPopupUI> unusedPopups = new Stack<CustomerPopupUI>();
    private int lastQueueSize = -1;

    private void Update()
    {
        int queueSize = customerQueue.Size;
        if (queueSize == lastQueueSize)
        {
            return;
        }

        lastQueueSize = queueSize;
        Render();
    }

    private void Render()
    {
        foreach (var popup in usedPopups)
        {
            Release(popup);
        }
        usedPopups.Clear();

        // Stack popups from the top-right corner towards the left, first customer rightmost.
        float offsetX = margin;
        foreach (var customer in customerQueue.All)
        {
            var popup = Get();
            popup.Render(customer);

            // Popups are anchored top-right; rebuild first so content-driven sizes are up to date.
            var rect = (RectTransform)popup.transform;
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            var size = rect.rect.size;
            rect.anchoredPosition = new Vector2(
                -offsetX - size.x * (1f - rect.pivot.x),
                -margin - size.y * (1f - rect.pivot.y));
            offsetX += size.x + margin;

            usedPopups.Add(popup);
        }
    }

    private CustomerPopupUI Get()
    {
        var popup = unusedPopups.Count > 0
            ? unusedPopups.Pop()
            : Instantiate(customerPopupUIPrefab, transform);
        popup.gameObject.SetActive(true);
        return popup;
    }

    private void Release(CustomerPopupUI popup)
    {
        popup.gameObject.SetActive(false);
        unusedPopups.Push(popup);
    }
}
