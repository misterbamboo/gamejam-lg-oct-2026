using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public class PimentsManager : MonoBehaviour
{
    [SerializeField]
    GameObject pimentPrefab;

    List<Piment> piments = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        piments = GetPiments();
        CreatePiment(new Piment(0, "piment", "Materials/Jaune"), new Vector3(0,0,0));
    }

    public GameObject CreatePiment(Piment piment, Vector3 pos)
    {
        GameObject newPiment = Instantiate(pimentPrefab, pos, Quaternion.identity);
        newPiment.GetComponent<Renderer>().material = Resources.Load(piment.MatDir, typeof(Material)) as Material;

        return newPiment;
    }

    private List<Piment> GetPiments()
    {
        using (StreamReader r = new StreamReader("Assets/Piments/JSON/Piment.json"))
        {
            string json = r.ReadToEnd();
            List<Piment> piments = JsonConvert.DeserializeObject<List<Piment>>(json);
            return piments;
        }
    }
}
