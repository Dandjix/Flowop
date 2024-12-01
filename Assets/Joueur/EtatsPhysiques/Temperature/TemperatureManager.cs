using UnityEngine;

public class TemperatureManager : MonoBehaviour
{
    private EtatsPhysiques.EtatPhysiquesStateMachine EtatStateMachine;

    [SerializeField] private float temperature = 0f;
    public float Temperature { get => temperature; private set => temperature = value; }

    [Header("Transition Timings")]
    [SerializeField] private float timeSolidToVisqueux = 6f;
    [SerializeField] private float timeVisqueuxToSolid = 1f;
    [SerializeField] private float timeGazToVisqueux = 2f;
    [SerializeField] private float timeVisqueuxToGaz = 1f;

    [Header("Temperature Thresholds")]
    [SerializeField] private float threshVisqueuxToSolid = -0.5f;
    [SerializeField] private float threshSolidToVisqueux = -0.1f;
    [SerializeField] private float threshVisqueuxToGaz = 0.5f;
    [SerializeField] private float threshGazToVisqueux = 0.1f;

    [Header("External Control Flags")]
    public bool heating = false;
    public bool cooling = false;

    private void Start()
    {
        EtatStateMachine = GetComponent<EtatsPhysiques.EtatPhysiquesStateMachine>();
        if (EtatStateMachine == null)
        {
            Debug.LogError("EtatPhysiquesStateMachine is missing on the GameObject.");
        }
    }

    private void FixedUpdate()
    {
        if (heating || cooling)
        {
            ChangeTemperature();
        }
        else
        {
            AdjustTowardsAmbient();
        }

        CheckStateChange();

        // Reset heating and cooling flags
        heating = false;
        cooling = false;
    }

    private void AdjustTowardsAmbient()
    {
        float adjustmentSpeed = (Temperature > 0) ? timeGazToVisqueux : timeSolidToVisqueux;
        float adjustment = Time.fixedDeltaTime / adjustmentSpeed;

        // Move temperature towards 0
        if (Temperature > 0)
        {
            Temperature = Mathf.Max(0, Temperature - adjustment);
        }
        else
        {
            Temperature = Mathf.Min(0, Temperature + adjustment);
        }
    }

    private void ChangeTemperature()
    {
        float adjustmentSpeed = heating ? timeVisqueuxToGaz : timeVisqueuxToSolid;
        float adjustment =  Time.fixedDeltaTime / adjustmentSpeed ;

        if (heating)
        {
            Temperature += adjustment;
            Temperature = Mathf.Min(1, Temperature);
        }
        else if (cooling)
        {
            Temperature -= adjustment;
            Temperature = Mathf.Max(-1, Temperature);
        }
    }

    private void CheckStateChange()
    {
        if (EtatStateMachine == null) return;

        switch (EtatStateMachine.currentStateEnum)
        {
            case EtatsPhysiques.EtatsPhysiques.Solide:
                if (Temperature > threshSolidToVisqueux)
                {
                    EtatStateMachine.setState(EtatsPhysiques.EtatsPhysiques.Visqueux);
                }
                break;

            case EtatsPhysiques.EtatsPhysiques.Gazeux:
                if (Temperature < threshGazToVisqueux)
                {
                    EtatStateMachine.setState(EtatsPhysiques.EtatsPhysiques.Visqueux);
                }
                break;

            case EtatsPhysiques.EtatsPhysiques.Visqueux:
                if (Temperature > threshVisqueuxToGaz)
                {
                    EtatStateMachine.setState(EtatsPhysiques.EtatsPhysiques.Gazeux);
                }
                else if (Temperature < threshVisqueuxToSolid)
                {
                    EtatStateMachine.setState(EtatsPhysiques.EtatsPhysiques.Solide);
                }
                break;
        }
    }
}
