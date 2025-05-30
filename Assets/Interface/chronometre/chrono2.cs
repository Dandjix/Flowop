using UnityEngine;
using TMPro;

public class Chronometre : MonoBehaviour
{
    public static Chronometre Instance; // Singleton pour accéder facilement à l'instance
    public TextMeshProUGUI chronometreText; // Associez votre TextMeshPro ici
    private float tempsEcoule = 0f; // Temps écoulé
    private bool enCours = false; // Indique si le chrono tourne

    public float TempsEcoule
    {
        get { return tempsEcoule; }
    }

    void Start()
    {
        Instance = this;
        DemarrerChronometre();
    }

    // Démarre le chronomètre
    public void DemarrerChronometre()
    {
        tempsEcoule = 0f;
        enCours = true;
    }

    // Arrête le chronomètre
    public void ArreterChronometre()
    {
        enCours = false;
    }

    // Redémarre le chronomètre
    public void ResetChronometre()
    {
        tempsEcoule = 0f;
        AfficherTemps();
    }

    private void Update()
    {
        if (enCours)
        {
            tempsEcoule += Time.deltaTime; // Incrémentation du temps
            AfficherTemps();
        }
    }

    // Met à jour l'affichage
    private void AfficherTemps()
    {
        int minutes = Mathf.FloorToInt(tempsEcoule / 60); // Minutes
        int secondes = Mathf.FloorToInt(tempsEcoule % 60); // Secondes
        int millisecondes = Mathf.FloorToInt((tempsEcoule * 100) % 100); // Millisecondes

        if (chronometreText != null)
        {

            chronometreText.text = $"{minutes:00}:{secondes:00}:{millisecondes:00}";
        }
    }
}
