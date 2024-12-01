namespace EtatsPhysiques
{
    using UnityEngine;

    public class EtatPhysiqueGazeux : EtatPhysiqueState
    {
        [SerializeField] private GameObject joueurGazeux;
        [SerializeField] private float vitesseMaxVerticale;
        [SerializeField] private float accelerationVerticale;
        [SerializeField] private float vitesseMaxHorizontale;
        [SerializeField] private float accelerationHorizontale;
        [SerializeField] private float amortissementLineaire;


        private Rigidbody2D joueurRigidbody;
        protected override void enter(EtatPhysiqueState from)
        {
            Player.SetActive(false);
            Vector2 playerPosition = Player.transform.position;

            Player = joueurGazeux;
            Player.transform.position = playerPosition;
            Player.SetActive(true);

            joueurRigidbody = Player.GetComponent<Rigidbody2D>();
            if (joueurRigidbody == null)
            {
                Debug.LogError("Le joueur gazeux n'a pas de Rigidbody2D");
            }
            GazSuivi gaz = Player.GetComponent<GazSuivi>();
            if (gaz != null)
            {
                gaz.CreerParticules(StateMachine.etatPhysiqueStore.LinearVelocity);
            }

            joueurRigidbody.linearVelocity = StateMachine.etatPhysiqueStore.LinearVelocity;
        }

        protected override void exit(EtatPhysiqueState to)
        {
            GazSuivi gaz = Player.GetComponent<GazSuivi>();
            if (gaz != null)
            {
                gaz.DetruireParticules();
            }

            StateMachine.etatPhysiqueStore.LinearVelocity = joueurRigidbody.linearVelocity; 
        }

        private void Update()
        {
            if (Player != null && joueurRigidbody != null)
            {

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    joueurRigidbody.linearVelocity = new Vector2(joueurRigidbody.linearVelocityX, Mathf.Min(joueurRigidbody.linearVelocityY + accelerationVerticale, vitesseMaxVerticale));

                }

                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        joueurRigidbody.linearVelocity = new Vector2(Mathf.Max(joueurRigidbody.linearVelocityX - accelerationHorizontale, -vitesseMaxHorizontale), joueurRigidbody.linearVelocityY);

                    }

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        joueurRigidbody.linearVelocity = new Vector2(Mathf.Min(joueurRigidbody.linearVelocityX + accelerationHorizontale, vitesseMaxHorizontale), joueurRigidbody.linearVelocityY);

                    }
                } else
                {
                    joueurRigidbody.linearVelocityX = Mathf.MoveTowards(joueurRigidbody.linearVelocityX, 0, amortissementLineaire * Time.deltaTime);                                     

                }



            }
        }
    }

}

