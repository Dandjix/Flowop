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

        public EtatPhysiqueStore etatPhysiqueStore { get; private set; }

        public void setState(EtatsPhysiques etat)
        {
            if (etat == currentStateEnum)
            {
                return;
            }

            //Debug.Log("changing to state : " + etat);

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

        public EtatsPhysiques currentStateEnum
        {
            get
            {
                if(currentState is EtatPhysiqueVisqueux)
                    return EtatsPhysiques.Visqueux;
                else if(currentState is EtatPhysiqueGazeux)
                    return EtatsPhysiques.Gazeux;
                else
                    return EtatsPhysiques.Solide;
            }
        }

        private void setState(EtatPhysiqueState etat)
        {
            if(currentState != null)
            {
                currentState.Sm_Exit(etat);
            }
            var oldState = currentState;

            currentState = etat;
            etat.Sm_Enter(oldState);
        }

        private void Start()
        {
            etatPhysiqueStore = GetComponent<EtatPhysiqueStore>();

            physiqueGazeux = GetComponent<EtatPhysiqueGazeux>();
            physiqueSolide = GetComponent<EtatPhysiqueSolide>();
            physiqueVisqueux = GetComponent <EtatPhysiqueVisqueux>();

            physiqueGazeux.Sm_Setup(this);
            physiqueSolide.Sm_Setup(this);
            physiqueVisqueux.Sm_Setup(this);

            setState(EtatsPhysiques.Solide);
        }

        public Vector2 getPlayerPosition()
        {
            if(currentState is EtatPhysiqueVisqueux)
            {
                var visqueuxPlayer = Player.GetComponent<JoueurVisqueux>();

                return visqueuxPlayer.Center;
            }
            return Player.transform.position;
        }
    }

    public enum EtatsPhysiques
    {
        Solide,
        Gazeux,
        Visqueux
    }
}

