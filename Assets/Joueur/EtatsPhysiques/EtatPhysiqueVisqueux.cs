namespace EtatsPhysiques
{
    using UnityEngine;

    public class EtatPhysiqueVisqueux : EtatPhysiqueState
    {
        [SerializeField] private GameObject joueurVisqueux;

        private void Awake()
        {
            joueurVisqueux.GetComponent<JoueurVisqueux>().Setup();
        }

        public Vector2 Center { get
            {
                var joueur = joueurVisqueux.GetComponent<JoueurVisqueux>();
                return joueur.Center;
            }
            private set
            {
                Vector2 blobPosition = Center;
                var joueur = joueurVisqueux.GetComponent<JoueurVisqueux>();

                joueur.Center = value;
            }
        }

        protected override void enter(EtatPhysiqueState from)
        {

            Player.SetActive(false);
            Vector2 playerPosition = Player.transform.position;


            Player = joueurVisqueux;

            JoueurVisqueux joueurVisqueux_Component = Player.GetComponent<JoueurVisqueux>();

            //joueurVisqueux_Component.ResetBonesPositions();
            joueurVisqueux_Component.Center = playerPosition;

            Player.SetActive(true);

            //Debug.Log("entering : " + Player.name);
        }

        protected override void exit(EtatPhysiqueState from)
        {
            //Debug.Log("exiting : " + Player.name);
            JoueurVisqueux joueurVisqueux = Player.GetComponent<JoueurVisqueux>();
            joueurVisqueux.UnStick(0);
        }
    }
}