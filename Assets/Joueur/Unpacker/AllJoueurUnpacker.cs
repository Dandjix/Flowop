using System.Collections.Generic;
using UnityEngine;

public class AllJoueurUnpacker : MonoBehaviour
{
    private void Awake()
    {
        List<Transform> children = new List<Transform>();

        foreach(Transform childTransform in transform)
        {
            children.Add(childTransform);
        }

        foreach(Transform childTransform in children)
        {
            childTransform.parent = null;
        }


        Destroy(gameObject);
    }
}
