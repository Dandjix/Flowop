using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class joueurSolideMouvement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    [SerializeField] private float vitesse;
    [SerializeField] private float forceDeSaut;
    [SerializeField] private float dragDeSaut, dragDecrease;
    [SerializeField] private ContactFilter2D filterBas;
    [SerializeField] private float verreDetruitDelai = 10f;
    private bool surSol => rigidbody.IsTouching(filterBas);
    private bool sauter;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sauter = false;
        dragDeSaut = -1;
    }

    // Update is called once per frame
    void Update()
    {
        playerControl();
    }

    void playerControl()
    {
        // Mouvement horizontal
        float hInupt = Input.GetAxis("Horizontal");
        if(Mathf.Abs(hInupt) > 0.1)
        {
            rigidbody.linearVelocityX = vitesse * hInupt;
        }

        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && surSol)
        {
            rigidbody.AddForce(new Vector2(0, forceDeSaut), ForceMode2D.Impulse);
            dragDeSaut = forceDeSaut;
            sauter = true;
        }
        if (sauter)
        {
            if (Input.GetKey(KeyCode.Space) && dragDeSaut >= 0)
            {
                rigidbody.AddForce(new Vector2(0, dragDeSaut), ForceMode2D.Force);
                dragDeSaut -= dragDecrease * Time.fixedDeltaTime;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                sauter = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Verre":
                if(rigidbody.IsTouching(collision.collider, filterBas))
                {
                    StartCoroutine(detruitVerre(collision.gameObject));
                }
                break;
        }
    }

    IEnumerator detruitVerre(GameObject verre)
    {
        yield return new WaitForSeconds(verreDetruitDelai);
        Vector2 vPos = verre.transform.position;
        Vector2 vSize = verre.GetComponent<BoxCollider2D>().size;
        verre.SetActive(false);
        StartCoroutine(resetVerre(verre, vPos, vSize));
    }

    IEnumerator resetVerre(GameObject verre, Vector2 vPos, Vector2 vSize)
    {
        yield return new WaitForSeconds(verreDetruitDelai*3);
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
