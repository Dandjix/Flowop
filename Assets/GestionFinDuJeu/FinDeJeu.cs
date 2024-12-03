using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinDeJeu : MonoBehaviour
{
    public TMP_Text texteTemps; // R�f�rence au texte pour le temps
    public TMP_Text textePieces; // R�f�rence au texte pour le nombre de pi�ces
    public TMP_InputField inputPseudo; // R�f�rence au champ d'entr�e pour le pseudo
    public TMP_Text messageFeedback; // R�f�rence pour afficher des messages (optionnel)
    public APIManager apiManager; // R�f�rence au gestionnaire API

    private float tempsEcoule; // Temps �coul� � afficher
    private int nombreDePieces; // Nombre de pi�ces � afficher

    void Start()
    {
        // Lire les donn�es � partir de PlayerPrefs
        tempsEcoule = PlayerPrefs.GetFloat("TempsEcoule", 0f); // Valeur par d�faut de 0f
        nombreDePieces = PlayerPrefs.GetInt("NombreDePieces", 0); // Valeur par d�faut de 0

        // Affichage des donn�es
        AfficherResultats();
    }

    private void AfficherResultats()
    {
        // Affichage du temps �coul�
        int minutes = Mathf.FloorToInt(tempsEcoule / 60);
        int secondes = Mathf.FloorToInt(tempsEcoule % 60);
        int millisecondes = Mathf.FloorToInt((tempsEcoule * 100) % 100);

        texteTemps.text = $"{minutes:00}:{secondes:00}:{millisecondes:00}";
        textePieces.text = $"Pi�ces : {nombreDePieces}";
    }

    public void EnvoyerScore()
    {
        // R�cup�rer le pseudo entr�
        string pseudo = inputPseudo.text;

        // V�rification : Pseudo non vide
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
        // Appeler la m�thode APIManager.EnvoyerScore et attendre la r�ponse
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
        // Revenir � la sc�ne 0 (par exemple le menu principal)
        SceneManager.LoadScene("Main Menu");
    }
}
