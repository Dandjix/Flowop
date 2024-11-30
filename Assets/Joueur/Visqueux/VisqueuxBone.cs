using UnityEngine;
using UnityEngine.XR;

public class VisqueuxBone : MonoBehaviour
{
    public float frequency_adjacent
    {
        get
        {
            return springJointAdjacent.frequency;
        }
        set
        {
            springJointAdjacent.frequency = value;
        }
    }

    public float dampingRatio_adjacent
    {
        get
        {
            return springJointAdjacent.dampingRatio;
        }
        set
        {
            springJointAdjacent.dampingRatio = value;
        }
    }

    public float frequency_opposite
    {
        get
        {
            return springJointOpposite.frequency;
        }
        set
        {
            springJointOpposite.frequency = value;
        }
    }

    public float dampingRatio_opposite
    {
        get
        {
            return springJointOpposite.dampingRatio;
        }
        set
        {
            springJointOpposite.dampingRatio = value;
        }
    }

    SpringJoint2D springJointAdjacent, springJointOpposite;

    public void Attach(Transform boneAdjacent, Transform boneOpposite)
    {
        springJointAdjacent = gameObject.AddComponent<SpringJoint2D>();
        springJointAdjacent.connectedBody = boneAdjacent.GetComponent<Rigidbody2D>();
        springJointAdjacent.autoConfigureConnectedAnchor = true;

        springJointOpposite = gameObject.AddComponent<SpringJoint2D>();
        springJointOpposite.connectedBody = boneOpposite.GetComponent<Rigidbody2D>();
        springJointOpposite.autoConfigureConnectedAnchor = true;
    }

}
