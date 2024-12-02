using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinDeJeu : MonoBehaviour
{
    public TMP_Text texteTemps; // Référence au texte pour le temps
    public TMP_Text textePieces; // Référence au texte pour le nombre de pièces

    void Start()
    {
        // Lire les données stockées dans PlayerPrefs
        float tempsEcoule = PlayerPrefs.GetFloat("TempsEcoule");
        int nombreDePieces = PlayerPrefs.GetInt("NombreDePieces");

        // Affichage du temps écoulé
        int minutes = Mathf.FloorToInt(tempsEcoule / 60);
        int secondes = Mathf.FloorToInt(tempsEcoule % 60);
        int millisecondes = Mathf.FloorToInt((tempsEcoule * 100) % 100);

        texteTemps.text = $"{minutes:00}:{secondes:00}:{millisecondes:00}";
        textePieces.text = $"Pièces : {nombreDePieces}";
    }

    public void RecommencerJeu()
    {
        // Revenir à la scène 0 (par exemple le menu principal)
        SceneManager.LoadScene(0);
    }
}
