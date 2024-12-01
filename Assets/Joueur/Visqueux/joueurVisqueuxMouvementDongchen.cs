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
    [SerializeField] private float dragDeSautInit, dragDeSaut, dragDecrease;
    [SerializeField] private int unstickingFrames;
    [SerializeField] private float stickyFactor;
    private float smoothness;
    [SerializeField] private ContactFilter2D filterBas, filterRock;

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

    private bool touchingRock
    {
        get
        {
            foreach (var rigidbody in rigidbodies)
            {
                if (rigidbody.IsTouching(filterRock))
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
        smoothness = touchingRock ? stickyFactor : 1;
        playerControl();
    }

    void playerControl()
    {
        // Mouvement horizontal
        float hInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(hInput) > 0.1)
        {
            joueurVisqueux.UnStick(0);
            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.linearVelocityX = vitesse * hInput * smoothness;
            }
        }

        // Grimper les rocks
        float vInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(vInput) > 0.1 && touchingRock)
        {
            joueurVisqueux.UnStick(0);
            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.linearVelocityY = vitesse * vInput * smoothness;
            }
        }

        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && (surSol || touchingRock))
        {
            joueurVisqueux.UnStick(unstickingFrames);
            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.AddForce(new Vector2(0, forceDeSaut * smoothness), ForceMode2D.Impulse);
                dragDeSaut = dragDeSautInit * smoothness;
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
