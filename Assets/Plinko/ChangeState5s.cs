using System.Collections;
using UnityEngine;

public class ChangeState5s : MonoBehaviour
{   
    private EtatsPhysiques.EtatPhysiquesStateMachine stateMachine;
    public float delay = 5f; // Temps avant la disparition

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine.setState(EtatsPhysiques.EtatsPhysiques.Visqueux);
        stateMachine = GetComponent<EtatsPhysiques.EtatPhysiquesStateMachine>();
        StartCoroutine(DisappearAfterDelay());
    }

    IEnumerator DisappearAfterDelay()
    {
        // Attend le délai
        yield return new WaitForSeconds(delay);
        stateMachine.setState(EtatsPhysiques.EtatsPhysiques.Solide);
    }

    
}
