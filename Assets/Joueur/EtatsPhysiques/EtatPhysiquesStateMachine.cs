namespace EtatsPhysiques
{

    using UnityEngine;

    [RequireComponent(typeof(EtatPhysiqueGazeux))]
    [RequireComponent(typeof(EtatPhysiqueSolide))]
    [RequireComponent(typeof (EtatPhysiqueVisqueux))]
    public class EtatPhysiquesStateMachine : MonoBehaviour
    {
        private EtatPhysiqueGazeux physiqueGazeux;
        private EtatPhysiqueSolide physiqueSolide;
        private EtatPhysiqueVisqueux physiqueVisqueux;

        public GameObject Player;

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
            physiqueGazeux = GetComponent<EtatPhysiqueGazeux>();
            physiqueSolide = GetComponent<EtatPhysiqueSolide>();
            physiqueVisqueux = GetComponent <EtatPhysiqueVisqueux>();

            physiqueGazeux.Sm_Setup(null);
            physiqueSolide.Sm_Setup(null);
            physiqueVisqueux.Sm_Setup(null);

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

