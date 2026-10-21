using Assets.CustomerQueueDomain.Entities;
using Assets.CustomerQueueDomain.Scripts;
using Assets.Shared;
using System.Collections.Generic;
using System.Linq;
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

    // What a customer pays for a chili they like.
    [SerializeField]
    private int chiliPrice = 5;

    private readonly List<CustomerPopupUI> usedPopups = new List<CustomerPopupUI>();
    private readonly Stack<CustomerPopupUI> unusedPopups = new Stack<CustomerPopupUI>();

    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    private void OnEnable()
    {
        customerQueue.CustomerAdded += OnCustomerAdded;
        customerQueue.CustomerRemoved += OnCustomerRemoved;
        EventBus.Subscribe<Vector2>(GameEvents.LadleDragged, OnLadleDragged);
        EventBus.Subscribe<LadleDrop>(GameEvents.LadleReleased, OnLadleReleased);
    }

    private void OnDisable()
    {
        customerQueue.CustomerAdded -= OnCustomerAdded;
        customerQueue.CustomerRemoved -= OnCustomerRemoved;
        EventBus.Unsubscribe<Vector2>(GameEvents.LadleDragged, OnLadleDragged);
        EventBus.Unsubscribe<LadleDrop>(GameEvents.LadleReleased, OnLadleReleased);
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

    private void OnLadleDragged(Vector2 screenPosition)
    {
        var hovered = PopupAt(screenPosition);
        foreach (var popup in usedPopups)
        {
            popup.SetHighlighted(popup == hovered);
        }
    }

    private void OnLadleReleased(LadleDrop drop)
    {
        var popup = PopupAt(drop.ScreenPosition);
        foreach (var used in usedPopups)
        {
            used.SetHighlighted(false);
        }

        if (popup == null)
        {
            return;
        }

        var customer = popup.Customer;
        var taste = customer.Preference.TasteOf(drop.Shu);
        var payment = customer.Likes(drop.Shu) ? chiliPrice : 0;

        // The card shows the reaction for a moment, then shrinks away when the customer leaves the queue.
        popup.React(customer.ReactionTo(drop.Shu));
        EventBus.Publish(GameEvents.ChiliServed, new ChiliServing(customer.Name, drop.Shu, taste, payment));
        customerQueue.Remove(customer);
    }

    // The 3D ladle and the UI cards share the mouse's screen position, so a rect test finds the card under it.
    private CustomerPopupUI PopupAt(Vector2 screenPosition)
    {
        var eventCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        // Cards still shrinking away belong to customers already removed from the queue.
        return usedPopups.Find(p =>
            customerQueue.All.Contains(p.Customer) &&
            RectTransformUtility.RectangleContainsScreenPoint((RectTransform)p.transform, screenPosition, eventCamera));
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
