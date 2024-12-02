using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIManager : MonoBehaviour
{
    private string apiUrl = "http://localhost:3000/leaderboard"; // Adresse de l'API

    // Méthode pour envoyer un score
    public IEnumerator EnvoyerScore(string player, int timeMs, int nCoins)
    {
        // Création des données JSON
        ScoreData scoreData = new ScoreData
        {
            player = player,
            time_ms = timeMs,
            n_coins = nCoins
        };
        string jsonData = JsonUtility.ToJson(scoreData);

        // Préparation de la requête POST
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Envoyer la requête et attendre la réponse
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score envoyé avec succès !");
        }
        else
        {
            Debug.LogError($"Erreur : {request.error}");
        }
    }
}

// Classe pour formater les données à envoyer
[System.Serializable]
public class ScoreData
{
    public string player;
    public int time_ms;
    public int n_coins;
}
