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
    private float radius;
    private bool sauter = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        radius = GetComponent<CircleCollider2D>().radius;
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
        rigidbody.linearVelocityX = vitesse * hInupt;

        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && surSol)
        {
            rigidbody.AddForce(new Vector2(0, forceDeSaut), ForceMode2D.Impulse);
            dragDeSaut = forceDeSaut;
            sauter = true;
        }
        if (Input.GetKey(KeyCode.Space) && sauter)
        {
            if (dragDeSaut >= 0)
            {
                rigidbody.AddForce(new Vector2(0, dragDeSaut), ForceMode2D.Force);
                dragDeSaut -= dragDecrease * Time.deltaTime;
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
                    Destroy(collision.gameObject, verreDetruitDelai);
                }
                break;
        }
    }

}
