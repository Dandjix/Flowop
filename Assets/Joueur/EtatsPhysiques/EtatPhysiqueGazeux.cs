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

        private void FixedUpdate()
        {
            if (Player == null || joueurRigidbody == null)
                return;

            Vector2 velocity = joueurRigidbody.linearVelocity;

            // Handle vertical movement (Up Arrow)
            if (Input.GetKey(KeyCode.UpArrow))
            {
                velocity.y = Mathf.Min(velocity.y + accelerationVerticale, vitesseMaxVerticale);
            }

            // Handle horizontal movement (Left and Right Arrows)
            if(Input.GetKey(KeyCode.LeftArrow) ^ Input.GetKey(KeyCode.RightArrow))
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    velocity.x = Mathf.Max(velocity.x - accelerationHorizontale, -vitesseMaxHorizontale);
                }
                else
                {
                    velocity.x = Mathf.Min(velocity.x + accelerationHorizontale, vitesseMaxHorizontale);
                }
            }
            else
            {
                // Apply damping when no horizontal input
                velocity.x = Mathf.MoveTowards(velocity.x, 0, amortissementLineaire);
            }

            joueurRigidbody.linearVelocity = velocity;

            //debugText.text = "vmv : " + vitesseMaxVerticale +
            //"\n accvert : " + accelerationVerticale +
            //"\n vmh : " + vitesseMaxHorizontale +
            //"\n acch : " + accelerationHorizontale +
            //"\n amm lineaire : " + amortissementLineaire +
            //"\n velocite = "+velocity
            //;
        }
    }

}

