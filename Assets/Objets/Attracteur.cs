using UnityEngine;
using UnityEngine.SceneManagement;

public class AttracteurSimple : MonoBehaviour
{
    [SerializeField] private float rayonAttraction = 5f;    // Rayon d'attraction
    [SerializeField] private float forceAttraction = 10f;   // Intensit� de la force d'attraction
    [SerializeField] private float rayonTeleportation = 0.5f; // Rayon pour d�clencher la t�l�portation

    private void FixedUpdate()
    {
        // Trouver tous les objets tagu�s "Joueur"
        GameObject[] joueurs = GameObject.FindGameObjectsWithTag("Joueur");

        foreach (var joueur in joueurs)
        {
            // Calculer la distance entre le vortex et le joueur
            float distance = Vector2.Distance(transform.position, joueur.transform.position);

            // Appliquer l'attraction si dans le rayon
            if (distance <= rayonAttraction)
            {
                Rigidbody2D joueurRb = joueur.GetComponent<Rigidbody2D>();
                if (joueurRb != null)
                {
                    // Calculer la direction vers le vortex
                    Vector2 direction = (transform.position - joueur.transform.position).normalized;

                    // Appliquer une force proportionnelle
                    joueurRb.AddForce(direction * forceAttraction);
                }
            }

            // Recharger la sc�ne si dans le rayon de t�l�portation
            if (distance <= rayonTeleportation)
            {
                // Sauvegarder les informations dans PlayerPrefs
                PlayerPrefs.SetFloat("TempsEcoule", Chronometre.Instance.TempsEcoule);
                PlayerPrefs.SetInt("NombreDePieces", Interfacage.Instance.NombreDePieces);

                // Charger la sc�ne de fin
                SceneManager.LoadScene("Winner");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualiser le rayon d'attraction
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rayonAttraction);

        // Visualiser le rayon de t�l�portation
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rayonTeleportation);
    }
}
