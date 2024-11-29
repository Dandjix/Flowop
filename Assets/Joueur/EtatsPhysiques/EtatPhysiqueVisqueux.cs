namespace EtatsPhysiques
{
    using UnityEngine;

    public class EtatPhysiqueVisqueux : EtatPhysiqueState
    {
        [SerializeField] private GameObject joueurVisqueux;

        protected override void enter(EtatPhysiqueState from)
        {

            Player.SetActive(false);
            Vector2 playerPosition = Player.transform.position;


            Player = joueurVisqueux;
            Player.transform.position = playerPosition;
            Player.SetActive(true);
        }

        protected override void exit(EtatPhysiqueState from)
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}