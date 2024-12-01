using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisappearingPlatform : MonoBehaviour
{
    private Tilemap tilemap; // Référence à la Tilemap
    public float delay = 5f; // Temps avant la disparition

    void Start()
    {
        // Récupère la Tilemap associée
        tilemap = GetComponent<Tilemap>();

        // Lance la coroutine pour la disparition
        StartCoroutine(DisappearAfterDelay());
    }

    IEnumerator DisappearAfterDelay()
    {
        // Attend le délai
        yield return new WaitForSeconds(delay);

        // Désactiver la collision et rendre la Tilemap invisible
        if (tilemap != null)
        {
            tilemap.ClearAllTiles(); // Supprime toutes les tuiles
        }
    }
}
