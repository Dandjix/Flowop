using EtatsPhysiques;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private EtatPhysiquesStateMachine stateMachine;

    [SerializeField] private float lerpFactor;

    [SerializeField] private Vector2 offset = Vector2.zero;
    // Update is called once per frame
    void Update()
    {
        Vector2 to = stateMachine.getPlayerPosition();

        Vector2 position = Vector2.Lerp((Vector2)transform.position + offset, to + offset, lerpFactor * Time.deltaTime);

        transform.position = new Vector3(position.x,position.y,transform.position.z);
    }
}
