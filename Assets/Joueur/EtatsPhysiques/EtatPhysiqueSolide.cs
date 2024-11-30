namespace EtatsPhysiques
{
    using UnityEngine;

    public class EtatPhysiqueSolide : EtatPhysiqueState
    {
        [SerializeField] private GameObject joueurSolide;

        protected override void enter(EtatPhysiqueState fromState)
        {

            Player.SetActive(false);

            Vector2 playerPosition;

            if (fromState is EtatPhysiqueVisqueux )
            {
                var stateVisqueux = fromState as EtatPhysiqueVisqueux;
                playerPosition = stateVisqueux.Center;
            }
            else
            {
                playerPosition = Player.transform.position;
            }


            Player = joueurSolide;
            Player.transform.position = playerPosition;
            Player.SetActive(true);
        }

        protected override void exit(EtatPhysiqueState from)
        {

        }
    }

}

