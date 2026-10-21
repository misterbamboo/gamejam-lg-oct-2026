using UnityEngine;
using System.Collections;

public class Piment : MonoBehaviour
{
    // Niveau d'épice 0 à 10
    [SerializeField]
    int shu;

    public int Shu
    {
        get { return shu; }
        private set
        {
            if (value >= 0 && value <= 10)
                shu = value;
            else
            {
                Debug.Log("Piment créer avec une mauvaise valeur de shu.");
                shu = 0;
            }
        }
    }

    [SerializeField]
    string nom;

    public string Nom
    {
        get { return nom; }
        private set { nom = value; }
    }

    [SerializeField]
    string pays;

    public string Pays
    {
        get { return pays; }
        private set { pays = value; }
    }

    [SerializeField]
    string matDir;

    public string MatDir
    {
        get { return matDir; }
        private set { matDir = value; }
    }

    public Piment(int shu, string nom, string pays, string matDir)
    {
        Shu = shu;
        Nom = nom;
        Pays = pays;
        MatDir = matDir;
    }

    public void CopyVals(Piment p)
    {
        Shu = p.Shu;
        Nom = p.Nom;
        Pays = p.Pays;
        MatDir = p.MatDir;
    }
    
    public override string ToString()
    {
        return nom + " epice: " + shu + " mat: " + MatDir;
    }
}
