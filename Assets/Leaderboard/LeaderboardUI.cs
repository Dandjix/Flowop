using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardUI : MonoBehaviour
{
    public Transform scoreListContent; // Parent où les scores seront affichés
    public GameObject scoreItemPrefab; // Prefab pour un score
    public string apiUrl; // URL de votre API

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

            // Trier les scores par temps (du meilleur au pire)
            scores.Sort((x, y) => x.time_ms.CompareTo(y.time_ms));

            // Affiche les scores avec leur rang
            for (int i = 0; i < scores.Count; i++)
            {
                GameObject scoreItem = Instantiate(scoreItemPrefab, scoreListContent);
                TMP_Text text = scoreItem.GetComponentInChildren<TMP_Text>();

                // Ajouter le rang devant le nom du joueur et afficher les informations
                text.text = $"{i + 1}. {scores[i].player} - {scores[i].n_coins} pièces - {scores[i].time_ms / 1000f:0.00}s";
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
