using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    private string apiUrl = "http://localhost:3000/leaderboard"; // Adresse de l'API

    public IEnumerator RecupererLeaderboard()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        // Envoyer la requête et attendre la réponse
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Leaderboard récupéré avec succès !");

            // Convertir la réponse JSON en liste de scores
            string jsonResponse = request.downloadHandler.text;
            LeaderboardEntry[] leaderboard = JsonHelper.FromJson<LeaderboardEntry>(jsonResponse);

            foreach (var entry in leaderboard)
            {
                Debug.Log($"Joueur : {entry.player}, Temps : {entry.time_ms}ms, Pièces : {entry.n_coins}");
            }
        }
        else
        {
            Debug.LogError($"Erreur : {request.error}");
        }
    }
}

// Classe pour stocker les données du leaderboard
[System.Serializable]
public class LeaderboardEntry
{
    public string player;
    public int time_ms;
    public int n_coins;
}

// Classe d'aide pour convertir le JSON en tableau
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{\"Items\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
