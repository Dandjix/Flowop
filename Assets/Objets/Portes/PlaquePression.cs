using System.Collections;
using UnityEngine;

public class PlaquePression : MonoBehaviour
{
    [SerializeField] private Porte porte;

    [SerializeField] private float duree = 5;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var reference = collision.transform.GetComponent<StateMachineReference>();

        if (reference == null || reference.StateMachine == null)
            return;

        if(reference.StateMachine.currentStateEnum != EtatsPhysiques.EtatsPhysiques.Gazeux )
        {
            Press();
        }
    }

    private void Press()
    {
        GetComponent<Renderer>().enabled = false;
        porte.Open();
        dureeValue = duree;
    }
    private void Depress()
    {
        GetComponent<Renderer>().enabled = true;
        porte.Close();
    }

    private float dureeValue = 0;

    private void Update()
    {
        if (dureeValue == 0)
            return;

        dureeValue -= Time.deltaTime;

        if(dureeValue <= 0)
        {
            Depress();
        }
    }

}
