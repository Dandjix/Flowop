using UnityEngine;

public class rebondsTrampoline : MonoBehaviour
{
    [SerializeField][Min(0)] private float reflectionFactor = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var trampoline = collision.gameObject;

        Debug.Log("tag : "+trampoline.tag);

        if (trampoline.tag != "Trampoline")
            return;

        GetComponent<Rigidbody2D>().linearVelocity = collision.relativeVelocity;
    }
}