namespace EtatsPhysiques
{
    using UnityEngine;

    public abstract class EtatPhysiqueState : MonoBehaviour
    {
        public EtatPhysiquesStateMachine StateMachine { get; private set; }

        public GameObject Player { get => StateMachine.player; }

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

        public void Sm_Setup(EtatPhysiquesStateMachine stateMachine)
        {
            StateMachine = stateMachine;
            Sm_Exit(null);
        }

        protected abstract void enter(EtatPhysiqueState from);
        protected abstract void exit(EtatPhysiqueState from);

    }
}

