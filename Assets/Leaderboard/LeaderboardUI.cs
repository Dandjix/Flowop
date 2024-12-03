using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LeaderboardUI : MonoBehaviour
{
    public Transform scoreListContent; // Parent où les scores seront affichés
    public GameObject scoreItemPrefab; // Prefab pour un score
    public string apiUrl = "https://flowop.codeky.fr/leaderboard"; // URL de votre API

    void Start()
    {
        StartCoroutine(FetchScores());
    }

    IEnumerator FetchScores()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Erreur de connexion : {request.error}");
        }
        else
        {
            // Parse la réponse JSON
            string json = request.downloadHandler.text;
            List<ScoreData> scores = JsonHelper.FromJson<ScoreData>(json);

            Debug.Log("TEST");
            Debug.Log(scores.Count);
            // Affiche les scores
            foreach (var score in scores)
            {
                GameObject scoreItem = Instantiate(scoreItemPrefab, scoreListContent);
                TMP_Text text = scoreItem.GetComponentInChildren<TMP_Text>();
                text.text = $"{score.player} - {score.n_coins} pièces - {score.time_ms / 1000f:0.00}s";
            }
        }
    }

    [System.Serializable]
    public class ScoreData
    {
        public string player;
        public int time_ms;
        public int n_coins;
    }

    public static class JsonHelper
    {
        public static List<T> FromJson<T>(string json)
        {
            string newJson = "{ \"Items\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.Items;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public List<T> Items;
        }
    }
}
