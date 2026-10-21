using System;
using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public class PimentsManager : MonoBehaviour
{
    [SerializeField]
    GameObject pimentPrefab;

    List<PimentData> piments = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        piments = GetPiments();
        Debug.Log(piments[0]);
        CreatePiment(0, new Vector3(0,0,0));
    }

    public GameObject CreatePiment(int indice, Vector3 pos)
    {
        GameObject newPiment = Instantiate(pimentPrefab, pos, Quaternion.identity);

        // Link Piment class
        newPiment.GetComponent<Piment>().CopyVals(piments[indice]);

        // Load material
        newPiment.GetComponent<Renderer>().material = Resources.Load(piments[indice].MatDir, typeof(Material)) as Material;

        return newPiment;
    }

    private List<PimentData> GetPiments()
    {
        using (StreamReader r = new StreamReader("Assets/Piments/JSON/Piment.json"))
        {
            string json = r.ReadToEnd();
            List<PimentData> piments = JsonConvert.DeserializeObject<List<PimentData>>(json);
            return piments;
        }
    }
}
