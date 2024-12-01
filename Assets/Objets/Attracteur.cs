using UnityEngine;
using UnityEngine.SceneManagement;

public class AttracteurSimple : MonoBehaviour
{
    [SerializeField] private float rayonAttraction = 5f;    // Rayon d'attraction
    [SerializeField] private float forceAttraction = 10f;   // Intensité de la force d'attraction
    [SerializeField] private GameObject positionTeleportationObject; // Point de téléportation
    [SerializeField] private float rayonTeleportation = 0.5f; // Rayon pour déclencher la téléportation

    private Transform positionTeleportation;

    private void Start()
    {
        // Vérifie et stocke le Transform du point de téléportation
        if (positionTeleportationObject != null)
        {
            positionTeleportation = positionTeleportationObject.transform;
        }
        else
        {
            Debug.LogWarning("Aucun objet de position de téléportation défini !");
        }
    }

    private void FixedUpdate()
    {
        // Trouver tous les objets tagués "Joueur"
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

            // Téléporter si dans le rayon de téléportation
            if (distance <= rayonTeleportation && positionTeleportation != null)
            {
                TeleporterJoueur(joueur);
            }
        }
    }

    private void TeleporterJoueur(GameObject joueur)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void OnDrawGizmosSelected()
    {
        // Visualiser le rayon d'attraction
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rayonAttraction);

        // Visualiser le rayon de téléportation
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rayonTeleportation);
    }
}
