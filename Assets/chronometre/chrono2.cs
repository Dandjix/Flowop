using UnityEngine;
using TMPro; // Importez ce namespace

public class WinnerScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // Assurez-vous d'utiliser TextMeshProUGUI

    void Start()
    {
        // Vérifiez que l'objet est bien assigné
        if (timerText != null)
        {
            timerText.text = "Temps écoulé : " + GameManager.timeElapsed.ToString("F2") + " secondes";
        }
        else
        {
            Debug.LogError("Le champ timerText n'est pas assigné !");
        }
    }
}
