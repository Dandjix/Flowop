using System.Collections;
using UnityEngine;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI;

public class destructeurVerre : MonoBehaviour
{
    [SerializeField] private float verreDelaiDestruction = 0.0f;
    [SerializeField] private float verreDelaiRespawn = 4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    //si on attache les coroutines au joueur, elles s'arrêtent quand on change d'état

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Verre":
                { 
                 var rb = GetComponent<Rigidbody2D>();
                    if (rb.IsTouching(collision.collider))
                    {
                        var verre = collision.gameObject;
                        if (verre == null)
                        {
                            return;
                        }
                        if (verreDelaiDestruction > 0.0f)
                        {
                            if(verre.GetComponent<verreRespawn>() != null)
                            {
                                return;
                            }
                            var coroutineManager = verre.AddComponent<verreRespawn>();
                            coroutineManager.StartCoroutine(detruitVerreCoroutine(verre, verreDelaiDestruction));
                        }
                        else
                        {
                            var velocity = collision.relativeVelocity;
                            Debug.Log("collision at velocity : "+velocity.magnitude);
                            detruitVerre(collision.gameObject);
                        }
                    }
                }
                break;

        }
    }

    IEnumerator detruitVerreCoroutine(GameObject verre, float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);

        detruitVerre(verre);
    }

    private void detruitVerre(GameObject verre)
    {
        Vector2 vPos = verre.transform.position;
        Vector2 vSize = verre.GetComponent<BoxCollider2D>().size;
        setVerreVisible(verre,false);
        var coroutineManager = verre.GetComponent<verreRespawn>();
        if(coroutineManager == null)
        {
            coroutineManager = verre.AddComponent<verreRespawn>();
        }

        coroutineManager.StartCoroutine(resetVerre(verre, vPos, vSize, verreDelaiRespawn));
    }

    private void setVerreVisible(GameObject verre, bool visible)
    {
        verre.GetComponent<Renderer>().enabled = visible;
        verre.GetComponent<Collider2D>().enabled = visible;
    }

    IEnumerator resetVerre(GameObject verre, Vector2 vPos, Vector2 vSize, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        bool objectInTheWay = true;

        while (objectInTheWay)
        {
            yield return 0; //wait one frame
            var result = Physics2D.BoxCast(vPos, vSize, 0, new Vector2(0, 0));
            objectInTheWay = result.collider != null;
        }

        setVerreVisible(verre, true);
        Destroy(verre.GetComponent<verreRespawn>());
    }
}
