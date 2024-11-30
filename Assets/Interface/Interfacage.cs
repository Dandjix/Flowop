using UnityEngine;
using TMPro;
using JetBrains.Annotations; // Nécessaire pour TextMeshPro

using System.Collections.Generic;

public class Interfacage : MonoBehaviour
{
    List<GameObject> listePiecesRamassees = new List<GameObject>();

    public TMP_Text textePieces; // Référence au texte TextMeshPro dans le Canvas
    private int nombreDePieces;
    private int NombreDePieces
    {
        get { return nombreDePieces; }
        set { 
            nombreDePieces = value; 
            MettreAJourUI();
        }

    }

    

    public static Interfacage Instance;

    public void RamasserPiece(GameObject piece)
    {
        if (listePiecesRamassees.Contains(piece))
        {
            return;
        }

        NombreDePieces++;
        listePiecesRamassees.Add(piece);
    }
    void Start()
    {
        Instance = this;
        MettreAJourUI();

    }

    void Update()
    {

    }

    private void MettreAJourUI()
    {
        textePieces.text = nombreDePieces.ToString();
    }
}
