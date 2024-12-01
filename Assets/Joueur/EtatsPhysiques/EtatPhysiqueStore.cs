using UnityEngine;

public class EtatPhysiqueStore : MonoBehaviour
{
    private Vector2 linearVelocity = Vector2.zero;
    public Vector2 LinearVelocity
    {
        get
        {
            //Debug.Log("linear velocity read !");
            return linearVelocity;
        }
        set
        {
            linearVelocity = value;
            //Debug.Log("linear velocity set !");
        }
    }
}
