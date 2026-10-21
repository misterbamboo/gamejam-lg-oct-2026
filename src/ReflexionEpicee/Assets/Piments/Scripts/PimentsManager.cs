using UnityEngine;

public class PimentsManager : MonoBehaviour
{
    [SerializeField]
    GameObject pepperPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreatePepper(string nom, Vector3 pos)
    {
        GameObject pepper = Instantiate(pepperPrefab, pos, Quaternion.identity);
        return pepper;
    }
}
