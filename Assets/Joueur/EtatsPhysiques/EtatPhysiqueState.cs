using UnityEngine;

public abstract class EtatPhysiqueState : MonoBehaviour {

    public void Sm_Enter(EtatPhysiqueState from)
    {
        enter(from);
        enabled = true;
    }

    public void Sm_Exit(EtatPhysiqueState from)
    {
        exit(from);
        enabled = false;
    }

    protected abstract void enter(EtatPhysiqueState from);
    protected abstract void exit(EtatPhysiqueState from);

}
