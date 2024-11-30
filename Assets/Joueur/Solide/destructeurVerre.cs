using System.Collections;
using UnityEngine;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI;

public class destructeurVerre : MonoBehaviour
{
    [SerializeField] private float verreDelaiDestruction = 0.0f;
    [SerializeField] private float verrreDelaiRespawn = 4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Verre":
                { 
                 var rb = GetComponent<Rigidbody2D>();
                    if (rb.IsTouching(collision.collider))
                    {
                        StartCoroutine(detruitVerre(collision.gameObject));
                    }
                }
                break;

        }
    }

    IEnumerator detruitVerre(GameObject verre)
    {
        if (verreDelaiDestruction > 0.0f)
        {
            yield return new WaitForSeconds(verreDelaiDestruction);
        }

        Vector2 vPos = verre.transform.position;
        Vector2 vSize = verre.GetComponent<BoxCollider2D>().size;
        verre.SetActive(false);
        StartCoroutine(resetVerre(verre, vPos, vSize));
    }

    IEnumerator resetVerre(GameObject verre, Vector2 vPos, Vector2 vSize)
    {
        yield return new WaitForSeconds(verrreDelaiRespawn);
        if (Physics2D.BoxCast(vPos, vSize, 0, new Vector2(0, 0)))
        {
            StartCoroutine(resetVerre(verre, vPos, vSize));
        }
        else
        {
            verre.SetActive(true);
        }
    }
}
