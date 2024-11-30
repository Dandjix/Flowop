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

    FixedJoint2D fixedJoint;

    private JoueurVisqueux joueurVisqueux;

    public void Attach(Transform boneAdjacent, Transform boneOpposite, JoueurVisqueux joueurVisqueux)
    {
        this.joueurVisqueux = joueurVisqueux;

        springJointAdjacent = gameObject.AddComponent<SpringJoint2D>();
        springJointAdjacent.connectedBody = boneAdjacent.GetComponent<Rigidbody2D>();
        springJointAdjacent.autoConfigureConnectedAnchor = true;

        springJointOpposite = gameObject.AddComponent<SpringJoint2D>();
        springJointOpposite.connectedBody = boneOpposite.GetComponent<Rigidbody2D>();
        springJointOpposite.autoConfigureConnectedAnchor = true;

        fixedJoint = gameObject.AddComponent<FixedJoint2D>();
        fixedJoint.enabled = false;
    }

    // sticky --

    const int fixedFramesBetweenStick = 10;

    private new Rigidbody2D rigidbody;

    public Vector2 originalLocalPos { get; private set; }
    public Quaternion originalRotation { get; private set; }

    private void Awake()
    {
        originalLocalPos = transform.localPosition;
        originalRotation = transform.rotation;
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private bool allowSticking
    {
        get
        {
            return false;
        }
    }

    //private bool sticking = false;
    public bool Sticking
    {
        get => fixedJoint.enabled;
        set
        {
            //if (value == sticking)
            //    return;

            //rigidbody.useFullKinematicContacts = value;
            //stickPosition = transform.position;
            //sticking = value;

            fixedJoint.enabled = value;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(joueurVisqueux.FixedFramesTillNextStick <= 0 
            && collision.collider.gameObject.layer == LayerMask.NameToLayer("Rocks")
            && !springJointOpposite.gameObject.GetComponent<VisqueuxBone>().Sticking)
        {
            Sticking = true;
            joueurVisqueux.FixedFramesTillNextStick = fixedFramesBetweenStick;
        }
        //if (joueurVisqueux.AllowSticking && LayerMask.LayerToName(collision.collider.gameObject.layer)=="")
        //{

        //}
    }
}
