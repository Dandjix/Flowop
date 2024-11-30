namespace EtatsPhysiques
{
    using UnityEngine;

    public class EtatPhysiqueVisqueux : EtatPhysiqueState
    {
        [SerializeField] private GameObject joueurVisqueux;
        [SerializeField] private float forceDeSaut = 5f;

        private Rigidbody2D joueurRigidbody;

        protected override void enter(EtatPhysiqueState from)
        {

            Player.SetActive(false);
            Vector2 playerPosition = Player.transform.position;


            Player = joueurVisqueux;
            Player.transform.position = playerPosition;
            Player.SetActive(true);

            joueurRigidbody = Player.GetComponent<Rigidbody2D>();
            if (joueurRigidbody == null)
            {
                Debug.LogError("Le joueur gazeux n'a pas de Rigidbody2D");
            }
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