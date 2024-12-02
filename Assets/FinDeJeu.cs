using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinDeJeu : MonoBehaviour
{
    public TMP_Text texteTemps; // R�f�rence au texte pour le temps
    public TMP_Text textePieces; // R�f�rence au texte pour le nombre de pi�ces

    void Start()
    {
        // Lire les donn�es stock�es dans PlayerPrefs
        float tempsEcoule = PlayerPrefs.GetFloat("TempsEcoule");
        int nombreDePieces = PlayerPrefs.GetInt("NombreDePieces");

        // Affichage du temps �coul�
        int minutes = Mathf.FloorToInt(tempsEcoule / 60);
        int secondes = Mathf.FloorToInt(tempsEcoule % 60);
        int millisecondes = Mathf.FloorToInt((tempsEcoule * 100) % 100);

        texteTemps.text = $"{minutes:00}:{secondes:00}:{millisecondes:00}";
        textePieces.text = $"Pi�ces : {nombreDePieces}";
    }

    public void RecommencerJeu()
    {
        // Revenir � la sc�ne 0 (par exemple le menu principal)
        SceneManager.LoadScene(0);
    }
}
