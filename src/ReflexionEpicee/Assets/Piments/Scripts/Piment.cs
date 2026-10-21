using UnityEngine;
using System.Collections;

public class Piment
{
    // Niveau d'épice 0 à 10
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

    string nom;

    public string Nom
    {
        get { return nom; }
        private set { nom = value; }
    }

    public Piment(int shu, string nom)
    {
        Shu = shu;
        Nom = nom;
    }
}
