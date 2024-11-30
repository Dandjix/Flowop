using System.Collections.Generic;
using UnityEngine;

public class GazSuivi : MonoBehaviour
{
    [SerializeField] private GameObject particulePrefab;       // Le prefab de la particule
    [SerializeField] private int nombreDeParticules = 15;      // Nombre de particules dans l'essaim
    [SerializeField] private float rayonFormation = 2f;        // Rayon autour du centre dans lequel les particules se déplacent
    [SerializeField] private float vitesseLerp = 5f;           // Vitesse de lerp (mouvement vers le centre)
    [SerializeField] private float separationMinimale = 0.5f;  // Distance minimale entre les particules avant séparation
    [SerializeField] private float forceSeparation = 1f;       // Force appliquée lorsque les particules sont trop proches
    [SerializeField] private float facteurMouvementAleatoire = 0.1f; // Facteur de variation aléatoire du mouvement des particules
    [SerializeField] private float distanceMaximale = 3f;      // Distance maximale de la corde

    private List<Rigidbody2D> particulesRigidbodies;           // Liste des rigidbodies des particules
    private Rigidbody2D rbPrincipal;                          // Rigidbody de l'objet principal

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 forceSurObjetPrincipal = Vector2.zero;

        foreach (var rb in particulesRigidbodies)
        {
            // Appliquer un mouvement aléatoire léger
            Vector2 directionAleatoire = new Vector2(Random.Range(-facteurMouvementAleatoire, facteurMouvementAleatoire),
                                                      Random.Range(-facteurMouvementAleatoire, facteurMouvementAleatoire));

            // Calculer la direction vers le centre de l'objet
            Vector2 directionVersCentre = (transform.position - rb.transform.position).normalized;

            // Calculer la nouvelle vélocité avec Lerp et ajouter le facteur aléatoire
            Vector2 nouvelleVitesse = Vector2.Lerp(rb.linearVelocity, directionVersCentre * vitesseLerp + directionAleatoire, Time.deltaTime);

            // Appliquer la nouvelle vélocité à la particule
            rb.linearVelocity = nouvelleVitesse;

            // Vérification et écartement des particules entre elles pour éviter la superposition excessive
            EviterSuperposition(rb);

            // Calculer si la particule dépasse la distance maximale
            float distance = Vector2.Distance(transform.position, rb.position);
            if (distance > distanceMaximale)
            {
                // Appliquer une force sur l'objet principal pour le ramener vers la particule
                Vector2 directionTension = (rb.position - (Vector2)transform.position).normalized;
                float forceTension = (distance - distanceMaximale); // Force proportionnelle à l'excès de distance
                forceSurObjetPrincipal += directionTension * forceTension;
            }
        }

        // Appliquer la somme des forces sur l'objet principal
        rbPrincipal.linearVelocity += forceSurObjetPrincipal * Time.deltaTime;
    }

    public void CreerParticules()
    {
        Debug.Log("TEST");
        particulesRigidbodies = new List<Rigidbody2D>();
        rbPrincipal = GetComponent<Rigidbody2D>();

        for (int i = 0; i < nombreDeParticules; i++)
        {
            GameObject particule = Instantiate(particulePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = particule.GetComponent<Rigidbody2D>();
            particulesRigidbodies.Add(rb);

            // Position initiale autour du centre
            float angle = i * Mathf.PI * 2f / nombreDeParticules;
            Vector3 positionInitiale = transform.position;
            particule.transform.position = positionInitiale;

            // Initialisation de la vélocité des particules
            rb.linearVelocity = Vector2.zero; // On commence avec une vélocité nulle
        }
    }
    public void DetruireParticules()
    {
        foreach (var rb in particulesRigidbodies)
        {
            if (rb != null)
            {
                Destroy(rb.gameObject); 
                Debug.Log("test");
            }
        }

        // Vider la liste après destruction
        particulesRigidbodies.Clear();
    }

    // Méthode pour empêcher la superposition excessive des particules
    private void EviterSuperposition(Rigidbody2D rb)
    {
        foreach (var autreRb in particulesRigidbodies)
        {
            if (rb != autreRb)
            {
                float distance = Vector2.Distance(rb.position, autreRb.position);

                // Si les particules sont trop proches, les séparer doucement
                if (distance < separationMinimale)
                {
                    // Calculer la direction pour écarter les particules
                    Vector2 directionDeSeparation = (rb.position - autreRb.position).normalized;

                    // Appliquer une force douce pour les éloigner en fonction de la distance
                    float force = forceSeparation * (separationMinimale - distance) / separationMinimale;

                    // Appliquer cette force de séparation
                    rb.linearVelocity += directionDeSeparation * force * Time.deltaTime;
                }
            }
        }
    }
}
