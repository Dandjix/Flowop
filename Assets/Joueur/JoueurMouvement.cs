using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joueurMouvement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.linearVelocity = Vector2.left;
        }

    }
}
