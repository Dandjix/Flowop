using UnityEngine;

public class PrendrePiece : MonoBehaviour
{
    public int compteurDePieces
    {
        get; private set;
    } = 0;
    void Start()
    {
        // Initialisation du compteur de pièces
        compteurDePieces = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifier si l'objet avec lequel on entre en collision a le tag "Piece Jaune"
        if (collision.CompareTag("Piece Jaune"))
        {
            // Incrémenter le compteur de pièces
            compteurDePieces++;

            // Détruire la pièce (retirer l'objet du jeu)
            Destroy(collision.gameObject);

            // Afficher le nombre de pièces collectées (optionnel)
            Debug.Log("Nombre de pièces collectées : " + compteurDePieces);
        }
    }
}
