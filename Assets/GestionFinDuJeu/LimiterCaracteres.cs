using TMPro;
using UnityEngine;

public class LimitInputField : MonoBehaviour
{
    public TMP_InputField inputField; // Référence à votre TMP_InputField
    public int maxCharacters = 10; // Limite du nombre de caractères

    void Start()
    {
        // Limiter le nombre de caractères
        inputField.characterLimit = maxCharacters;
    }
}
