using EtatsPhysiques;
using UnityEngine;

public abstract class StateItem : MonoBehaviour
{
    [SerializeField] GameObject joueurManager;
    static protected EtatPhysiquesStateMachine stateMachine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joueurManager = GameObject.Find("JoueurManagerEtatPhysique");
        stateMachine = joueurManager.GetComponent<EtatPhysiquesStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

