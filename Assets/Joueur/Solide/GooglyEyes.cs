using UnityEngine;

public class GooglyEyes : MonoBehaviour
{
    public Transform pupil; // La pupille qui doit bouger
    public float maxDistance = 0.1f; // Distance maximale que la pupille peut parcourir
    public float smoothTime = 0.1f; // Temps de lissage pour le mouvement
    private Rigidbody2D parentRigidbody; // Le Rigidbody2D du parent
    private Vector3 velocity = Vector3.zero; // Vitesse interne pour SmoothDamp

    void Start()
    {
        // Vérifier que la pupille est assignée
        if (pupil == null)
        {
            Debug.LogError("La pupille (pupil) n'est pas assignée dans l'inspecteur !");
            return;
        }

        // Récupérer le Rigidbody2D du parent automatiquement
        if (transform.parent != null)
        {
            parentRigidbody = transform.parent.GetComponent<Rigidbody2D>();
            //if (parentRigidbody == null)
            //{
            //    Debug.LogError("Aucun Rigidbody2D trouvé sur le parent !");
            //}
        }
        else
        {
            Debug.LogError("Aucun parent trouvé pour les yeux ! Assure-toi que l'œil est enfant d'un GameObject avec un Rigidbody2D.");
        }
    }

    void Update()
{
    // Si aucun Rigidbody2D ou pupille n'est trouvé, ne rien faire
    if (parentRigidbody == null || pupil == null) return;

    // Obtenir la direction de mouvement du parent
    Vector2 movementDirection = parentRigidbody.linearVelocity.normalized;

    // Obtenir la direction basée sur la rotation du parent
    Vector3 rotationDirection = transform.parent.up.normalized; // Utilise transform.parent.up (ou right selon ton setup)

    // Convertir movementDirection (Vector2) en Vector3 pour l'addition
    Vector3 movementDirection3D = new Vector3(movementDirection.x, movementDirection.y, 0);

    // Combiner mouvement et rotation
    Vector3 combinedDirection = movementDirection3D + rotationDirection;

    // Limiter la distance de la pupille
    Vector3 targetPosition = Vector3.ClampMagnitude(combinedDirection, maxDistance);

    // Déplacer la pupille de manière fluide vers la position cible
    pupil.localPosition = Vector3.SmoothDamp(pupil.localPosition, targetPosition, ref velocity, smoothTime);
}

}
