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

            playerPosition = Player.transform.position;

            Player = joueurSolide;
            Player.transform.position = playerPosition;

            Player.SetActive(true);

            joueurSolide.GetComponent<Rigidbody2D>().linearVelocity = StateMachine.etatPhysiqueStore.linearVelocity;
        }

        protected override void exit(EtatPhysiqueState from)
        {
            Vector2 velocity = Player.GetComponent<Rigidbody2D>().linearVelocity;
        }
    }

}

