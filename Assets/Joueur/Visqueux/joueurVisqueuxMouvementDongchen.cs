using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class joueurVisqueuxMouvementDongchen : MonoBehaviour
{
    private JoueurVisqueux joueurVisqueux;
    private List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>(12);
    [SerializeField] private float vitesse;
    [SerializeField] private float forceDeSaut;
    [SerializeField] private float dragDeSaut, dragDecrease;
    [SerializeField] private ContactFilter2D filterBas;

    private bool surSol
    {
        get 
        {
            foreach (var rigidbody in rigidbodies) {
                if (rigidbody.IsTouching(filterBas))
                    return true;
            }
            return false;
        }
    }

    private bool sauter;
    // Start is called before the first frame update
    void Start()
    {
        joueurVisqueux = GetComponent<JoueurVisqueux>();
        foreach (var bone in joueurVisqueux.GetSortedBones())
        {
            rigidbodies.Add(bone.GetComponent<Rigidbody2D>());
        }
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

        if (Mathf.Abs(hInupt) > 0.1)
        {
            foreach (var rigidbody in rigidbodies)
            {
                float newVelocity = rigidbody.linearVelocityX + vitesse * hInupt;

                rigidbody.linearVelocityX = newVelocity;
            }
        }


        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && surSol)
        {
            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.AddForce(new Vector2(0, forceDeSaut), ForceMode2D.Impulse);
                dragDeSaut = forceDeSaut;
                sauter = true;
            }
        }
        if (sauter)
        {
            if (Input.GetKey(KeyCode.Space) && dragDeSaut >= 0)
            {
                foreach (var rigidbody in rigidbodies)
                {
                    rigidbody.AddForce(new Vector2(0, dragDeSaut), ForceMode2D.Force);
                    dragDeSaut -= dragDecrease * Time.fixedDeltaTime;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                sauter = false;
            }
        }
    }
}
