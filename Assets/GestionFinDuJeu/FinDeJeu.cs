using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinDeJeu : MonoBehaviour
{
    public TMP_Text texteTemps; // Référence au texte pour le temps
    public TMP_Text textePieces; // Référence au texte pour le nombre de pièces
    public TMP_InputField inputPseudo; // Référence au champ d'entrée pour le pseudo
    public TMP_Text messageFeedback; // Référence pour afficher des messages (optionnel)
    public APIManager apiManager; // Référence au gestionnaire API

    private float tempsEcoule; // Temps écoulé à afficher
    private int nombreDePieces; // Nombre de pièces à afficher

    void Start()
    {
        // Lire les données à partir de PlayerPrefs
        tempsEcoule = PlayerPrefs.GetFloat("TempsEcoule", 0f); // Valeur par défaut de 0f
        nombreDePieces = PlayerPrefs.GetInt("NombreDePieces", 0); // Valeur par défaut de 0

        // Affichage des données
        AfficherResultats();
    }

    private void AfficherResultats()
    {
        // Affichage du temps écoulé
        int minutes = Mathf.FloorToInt(tempsEcoule / 60);
        int secondes = Mathf.FloorToInt(tempsEcoule % 60);
        int millisecondes = Mathf.FloorToInt((tempsEcoule * 100) % 100);

        texteTemps.text = $"{minutes:00}:{secondes:00}:{millisecondes:00}";
        textePieces.text = $"Pièces : {nombreDePieces}";
    }

    public void EnvoyerScore()
    {
        // Récupérer le pseudo entré
        string pseudo = inputPseudo.text;

        // Vérification : Pseudo non vide
        if (string.IsNullOrWhiteSpace(pseudo))
        {
            AfficherMessageFeedback("Veuillez entrer un pseudo valide.");
            return;
        }

        // Envoyer le score au leaderboard via l'APIManager
        int tempsEnMillisecondes = Mathf.FloorToInt(tempsEcoule * 1000);
        StartCoroutine(EnvoyerScoreCoroutine(pseudo, tempsEnMillisecondes, nombreDePieces));
    }

    private IEnumerator EnvoyerScoreCoroutine(string pseudo, int tempsMs, int pieces)
    {
        // Appeler la méthode APIManager.EnvoyerScore et attendre la réponse
        yield return StartCoroutine(apiManager.EnvoyerScore(pseudo, tempsMs, pieces));

    }

    private void AfficherMessageFeedback(string message)
    {
        if (messageFeedback != null)
        {
            messageFeedback.text = message;
        }
    }

    public void RecommencerJeu()
    {
        // Revenir à la scène 0 (par exemple le menu principal)
        SceneManager.LoadScene(0);
    }
}
