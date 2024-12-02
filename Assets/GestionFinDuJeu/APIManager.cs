using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIManager : MonoBehaviour
{
    private string apiUrl = "http://localhost:3000/leaderboard"; // Adresse de l'API

    // M�thode pour envoyer un score
    public IEnumerator EnvoyerScore(string player, int timeMs, int nCoins)
    {
        // Cr�ation des donn�es JSON
        ScoreData scoreData = new ScoreData
        {
            player = player,
            time_ms = timeMs,
            n_coins = nCoins
        };
        string jsonData = JsonUtility.ToJson(scoreData);

        // Pr�paration de la requ�te POST
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Envoyer la requ�te et attendre la r�ponse
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score envoy� avec succ�s !");
        }
        else
        {
            Debug.LogError($"Erreur : {request.error}");
        }
    }
}

// Classe pour formater les donn�es � envoyer
[System.Serializable]
public class ScoreData
{
    public string player;
    public int time_ms;
    public int n_coins;
}
