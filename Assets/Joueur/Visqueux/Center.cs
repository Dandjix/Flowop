using UnityEngine;

public class Center : MonoBehaviour
{
    public JoueurVisqueux joueur;
    public Vector2 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = joueur.Center - offset*joueur.transform.localScale.x;
        
    }
}
