namespace EtatsPhysiques
{
    using UnityEngine;

    public abstract class EtatPhysiqueState : MonoBehaviour
    {
        public EtatPhysiquesStateMachine StateMachine { get; private set; }

        public GameObject Player { get 
            {
                return StateMachine.Player; 
            } set => StateMachine.Player = value; }

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
            //Sm_Exit(null); //causes bugs at initialization
        }

        protected abstract void enter(EtatPhysiqueState from);
        protected abstract void exit(EtatPhysiqueState from);

    }
}

