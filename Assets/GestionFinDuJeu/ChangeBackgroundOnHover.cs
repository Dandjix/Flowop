using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonHoverEffect : MonoBehaviour
{
    public Image canvasBackground;      // L'image de fond existante
    public Sprite newBackground;        // La nouvelle image de fond à afficher lors du hover
    public float fadeDuration = 0.5f;   // Durée de la transition

    private Sprite defaultBackground;   // L'image de fond par défaut
    private GameObject overlayImageObj; // Image superposée pour le fade-in/out
    private Image overlayImage;         // Composant Image de l'overlay

    void Start()
    {
        if (canvasBackground == null)
        {
            Debug.LogError("Canvas background non défini !");
            return;
        }

        // Stocker l'image par défaut
        defaultBackground = canvasBackground.sprite;

        // Créer une image superposée pour les transitions
        overlayImageObj = new GameObject("OverlayImage");
        overlayImageObj.transform.SetParent(canvasBackground.transform, false);
        overlayImage = overlayImageObj.AddComponent<Image>();

        // Configurer l'overlay pour qu'il prenne la taille du canvas background
        overlayImage.rectTransform.anchorMin = Vector2.zero;
        overlayImage.rectTransform.anchorMax = Vector2.one;
        overlayImage.rectTransform.offsetMin = Vector2.zero;
        overlayImage.rectTransform.offsetMax = Vector2.zero;

        // Initialement invisible
        overlayImage.color = new Color(1f, 1f, 1f, 0f);
        overlayImage.raycastTarget = false;
    }

    public void OnHoverEnter()
    {
        if (overlayImage != null && newBackground != null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeInNewImage(newBackground));
        }
    }

    public void OnHoverExit()
    {
        if (overlayImage != null && defaultBackground != null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeInNewImage(defaultBackground));
        }
    }

    private IEnumerator FadeInNewImage(Sprite targetImage)
    {
        if (overlayImage == null) yield break;

        // Configurer l'overlay avec la nouvelle image
        overlayImage.sprite = targetImage;

        // Faire un fade-in de l'overlay
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Color color = overlayImage.color;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            overlayImage.color = color;
            yield return null;
        }

        // Une fois le fade-in terminé, définir l'image de fond principale sur la nouvelle image
        canvasBackground.sprite = targetImage;

        // Réinitialiser l'overlay pour qu'il devienne transparent
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Color color = overlayImage.color;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            overlayImage.color = color;
            yield return null;
        }
    }
}
