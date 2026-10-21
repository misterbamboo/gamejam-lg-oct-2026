using Assets.Shared;
using UnityEngine;

public class PimentsManager : MonoBehaviour
{
    [SerializeField]
    GameObject pimentPrefab;

    [SerializeField]
    Vector3 pos;

    // How many peppers are generated each time the batch runs out.
    [SerializeField]
    int batchSize = 10;

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
        var data = piments.Take();
        Debug.Log($"{data} ({piments.Remaining} left in batch)");

        GameObject newPiment = Instantiate(pimentPrefab, pos, Quaternion.identity);

        // Link Piment class
        newPiment.GetComponent<Piment>().CopyVals(data);

        // Load material
        newPiment.GetComponent<Renderer>().material = Resources.Load(data.MatDir, typeof(Material)) as Material;
    }

    private void OnEnable()
    {
        EventBus.Subscribe<PimentRequested>(GameEvents.PimentRequested, CreatePiment);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PimentRequested>(GameEvents.PimentRequested, CreatePiment);
    }
}
