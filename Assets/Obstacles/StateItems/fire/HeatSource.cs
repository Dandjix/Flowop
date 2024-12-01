using UnityEngine;

[DisallowMultipleComponent]
public class HeatSource : MonoBehaviour
{
    [SerializeField] private bool hot;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var stateMRef = collision.GetComponent<StateMachineReference>();

        if (stateMRef != null && stateMRef.ToString().StartsWith("JoueurGazeux"))
        {
            Debug.Log("state ref : " + stateMRef);
        }


        if (stateMRef == null)
            return;

        var temperatureManager = stateMRef.StateMachine.GetComponent<TemperatureManager>();
        if (temperatureManager != null && temperatureManager.ToString().StartsWith("JoueurGazeux"))
        {
            Debug.Log("temp manager : " + temperatureManager);
        }
        if (temperatureManager == null)
            return ;


        if (hot)
        {
            temperatureManager.heating = true;
        }
        else
        {
            temperatureManager.cooling = true;
        }
    }
}


