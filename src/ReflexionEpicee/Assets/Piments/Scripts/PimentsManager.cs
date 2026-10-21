using Assets.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
public class PimentsManager : MonoBehaviour
{
    [SerializeField]
    GameObject pimentPrefab;

    [SerializeField]
    Vector3 pos;

    List<PimentData> piments = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        piments = GetPiments();
    }

    public void CreatePiment(PimentRequested pimentRequested)
    {
        int indicePiment = UnityEngine.Random.Range(0, piments.Count);
        GameObject newPiment = Instantiate(pimentPrefab, pos, Quaternion.identity);

        // Link Piment class
        newPiment.GetComponent<Piment>().CopyVals(piments[indicePiment]);

        // Load material
        newPiment.GetComponent<Renderer>().material = Resources.Load(piments[indicePiment].MatDir, typeof(Material)) as Material;
    }

    private static List<PimentData> GetPiments()
    {
        using (StreamReader r = new StreamReader("Assets/Piments/JSON/Piment.json"))
        {
            string json = r.ReadToEnd();
            List<PimentData> piments = JsonConvert.DeserializeObject<List<PimentData>>(json);
            return piments;
        }
    }
    private void OnEnable()
    {
        EventBus.Subscribe<PimentRequested>(GameEvents.PimentRequested, CreatePiment);
    }
}
