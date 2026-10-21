using Assets.Shared;
using TMPro;
using UnityEngine;

public class PimentsManager : MonoBehaviour
{
    public int Bal = 10;

    [SerializeField]
    TextMeshProUGUI money;

    [SerializeField]
    GameObject pimentPrefab;

    [SerializeField]
    Vector3 pos;

    // How many peppers are generated each time the batch runs out.
    [SerializeField]
    int batchSize = 10;

    [SerializeField]
    GameObject drapeau;

    GameObject currPiment;

    PimentBatch piments;

    void Awake()
    {
        piments = new PimentBatch(batchSize);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreatePiment(new PimentRequested());
    }

    public void CreatePiment(PimentRequested pimentRequested)
    {
        Destroy(currPiment);

        var data = piments.Take();
        Debug.Log($"{data} ({piments.Remaining} left in batch)");

        GameObject newPiment = Instantiate(pimentPrefab, pos, Quaternion.identity);

        // Link Piment class
        newPiment.GetComponent<Piment>().CopyVals(data);

        // Load material
        newPiment.GetComponent<Renderer>().material = Resources.Load(data.MatDir, typeof(Material)) as Material;

        // Load drapeau: the flags in Resources/Drapeau are images, so they go on the material's texture
        var flag = Resources.Load<Texture2D>("Drapeau/" + data.Pays);
        if (flag == null)
        {
            Debug.LogWarning($"No flag image at Resources/Drapeau/{data.Pays}");
        }
        else if (drapeau != null)
        {
            drapeau.GetComponent<Renderer>().material.mainTexture = flag;
        }

        currPiment = newPiment;

        // Each pepper costs 1 $.
        Bal--;
        RefreshMoney();
    }

    // Customers pay for the chili they liked.
    private void OnChiliServed(ChiliServing serving)
    {
        Bal += serving.Payment;
        RefreshMoney();
    }

    private void RefreshMoney()
    {
        if (money != null)
        {
            money.text = "Argent : " + Bal + " $";
        }
    }

    private void OnEnable()
    {
        EventBus.Subscribe<PimentRequested>(GameEvents.PimentRequested, CreatePiment);
        EventBus.Subscribe<ChiliServing>(GameEvents.ChiliServed, OnChiliServed);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PimentRequested>(GameEvents.PimentRequested, CreatePiment);
        EventBus.Unsubscribe<ChiliServing>(GameEvents.ChiliServed, OnChiliServed);
    }
}
