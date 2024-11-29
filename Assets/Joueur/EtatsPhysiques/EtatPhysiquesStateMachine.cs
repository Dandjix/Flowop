namespace EtatsPhysiques
{
    using UnityEngine;

    public class EtatPhysiquesStateMachine : MonoBehaviour
    {
        [SerializeField] private EtatPhysiqueGazeux physiqueGazeux;
        [SerializeField] private EtatPhysiqueSolide physiqueSolide;
        [SerializeField] private EtatPhysiqueVisqueux physiqueVisqueux;


        public void setState(EtatsPhysiques etat)
        {
            switch (etat)
            {
                case EtatsPhysiques.Solide:
                    setState(physiqueSolide);
                    break;
                case EtatsPhysiques.Gazeux:
                    setState(physiqueGazeux);
                    break;
                case EtatsPhysiques.Visqueux:
                    setState(physiqueVisqueux);
                    break;
            }
        }

        public EtatPhysiqueState currentState { get; private set; }

        private void setState(EtatPhysiqueState etat)
        {
            if(currentState != null)
            {
                currentState.Sm_Exit(etat);
            }
            var oldState = currentState;

            currentState = etat;
            etat.Sm_Enter(currentState);
        }

        private void Start()
        {
            physiqueGazeux.Sm_Exit(null);
            physiqueSolide.Sm_Exit(null);
            physiqueVisqueux.Sm_Exit(null);

            setState(EtatsPhysiques.Solide);
        }
    }

    public enum EtatsPhysiques
    {
        Solide,
        Gazeux,
        Visqueux
    }
}

