using UnityEngine;

public class ChangeStateControl : MonoBehaviour
{
    private EtatsPhysiques.EtatPhysiquesStateMachine stateMachine;
    private void Start()
    {
        stateMachine = GetComponent<EtatsPhysiques.EtatPhysiquesStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            stateMachine.setState(EtatsPhysiques.EtatsPhysiques.Solide);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            stateMachine.setState(EtatsPhysiques.EtatsPhysiques.Visqueux);
        }
    }
}
