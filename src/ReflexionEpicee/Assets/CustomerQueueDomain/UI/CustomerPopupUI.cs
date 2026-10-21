using Assets.CustomerQueueDomain.Entities;
using TMPro;
using UnityEngine;

public class CustomerPopupUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text preferenceText;

    public void Render(Customer customer)
    {
        var preference = customer.Preference;
        preferenceText.text = $"Mmmm ... je préfère les piments d'une force de {preference.Min} à {preference.Max}";
    }
}
