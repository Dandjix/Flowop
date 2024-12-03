using TMPro;
using UnityEngine;

public class LimitInputField : MonoBehaviour
{
    public TMP_InputField inputField; // R�f�rence � votre TMP_InputField
    public int maxCharacters = 10; // Limite du nombre de caract�res

    void Start()
    {
        // Limiter le nombre de caract�res
        inputField.characterLimit = maxCharacters;
    }
}
