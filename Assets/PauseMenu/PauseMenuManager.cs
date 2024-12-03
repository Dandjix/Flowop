using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuCanvas; // Référence au Canvas du menu pause
    private bool isPaused = false; // Indique si le jeu est en pause
    private bool isLeaderboardOpen = false; // Indique si la scène Leaderboard est affichée

    void Update()
    {
        // Détecter l'appui sur Échap
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isLeaderboardOpen)
            {
                CloseLeaderboard();
            }
            else if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuCanvas.SetActive(true); // Afficher le menu pause
        Time.timeScale = 0f; // Arrêter le temps du jeu
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuCanvas.SetActive(false); // Cacher le menu pause
        Time.timeScale = 1f; // Reprendre le temps du jeu
    }

    public void OpenLeaderboard()
    {
        Time.timeScale = 0f;

        // Charger le leaderboard en mode additive
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Additive);
        isLeaderboardOpen = true; // Indiquer que le leaderboard est ouvert

        // Masquer le menu pause (optionnel, selon vos préférences)
        pauseMenuCanvas.SetActive(false);
    }

    public void CloseLeaderboard()
    {
        // Fermer la scène Leaderboard
        SceneManager.UnloadSceneAsync("Leaderboard");
        isLeaderboardOpen = false;

        // Réafficher le menu pause
        pauseMenuCanvas.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Quitter le jeu !");
        Application.Quit(); // Quitte l'application (ne fonctionne pas dans l'éditeur Unity)
    }
}
