using UnityEngine;

public class PrendrePiece : MonoBehaviour
{
    public int compteurDePieces
    {
        get; private set;
    } = 0;
    void Start()
    {
        // Initialisation du compteur de pi�ces
        compteurDePieces = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // V�rifier si l'objet avec lequel on entre en collision a le tag "Piece Jaune"
        if (collision.CompareTag("Piece Jaune"))
        {
            // Incr�menter le compteur de pi�ces
            compteurDePieces++;

            // D�truire la pi�ce (retirer l'objet du jeu)
            Destroy(collision.gameObject);

            // Afficher le nombre de pi�ces collect�es (optionnel)
            Debug.Log("Nombre de pi�ces collect�es : " + compteurDePieces);
        }
    }
}
