using UnityEngine;

public class VisqueuxCollant : MonoBehaviour
{
    private JoueurVisqueux joueurVisqueux;

    private void Awake()
    {
        joueurVisqueux = GetComponent<JoueurVisqueux>();
    }

    float gracePeriodLeft = 0f;

    public void UnStick(float gracePeriod)
    {
        gracePeriodLeft = gracePeriod;
    }

    private void Update()
    {
        if (gracePeriodLeft > 0f)
        {
            gracePeriodLeft -= Time.deltaTime;
        }
    }
}
