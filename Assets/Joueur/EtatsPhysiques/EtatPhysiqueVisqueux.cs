namespace EtatsPhysiques
{
    using UnityEngine;

    public class EtatPhysiqueVisqueux : EtatPhysiqueState
    {
        private GameObject playerPositionDummy;

        [SerializeField] private GameObject joueurVisqueux_Prefab;

        private void Awake()
        {
            playerPositionDummy = new GameObject("Player position dummy from visqueux");
        }

        //public Vector2 Center { get
        //    {
        //        var joueur = joueurVisqueux_Prefab.GetComponent<JoueurVisqueux>();
        //        return joueur.Center;
        //    }
        //    private set
        //    {
        //        Vector2 blobPosition = Center;
        //        var joueur = joueurVisqueux_Prefab.GetComponent<JoueurVisqueux>();

        //        joueur.Center = value;
        //    }
        //}

        protected override void enter(EtatPhysiqueState from)
        {
            Player.SetActive(false);
            Vector2 playerPosition = Player.transform.position;


            Player = Instantiate(joueurVisqueux_Prefab);

            JoueurVisqueux joueurVisqueux_Component = Player.GetComponent<JoueurVisqueux>();

            Player.transform.position = playerPosition;

            joueurVisqueux_Component.GenerateSprings(StateMachine);

            Player.SetActive(true);



            foreach (var bone in joueurVisqueux_Component.GetSortedBones())
            {
                bone.GetComponent<Rigidbody2D>().linearVelocity = StateMachine.etatPhysiqueStore.linearVelocity;
            }

            //Debug.Log("entering : " + Player.name);
        }

        /// <summary>
        /// sors de l'état, renvoyant la position du joueur.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        protected override void exit(EtatPhysiqueState from)
        {
            JoueurVisqueux joueurVisqueux = Player.GetComponent<JoueurVisqueux>();
            Vector2 pos = joueurVisqueux.Center;

            var bones = joueurVisqueux.GetSortedBones();
            Vector2 oldVelocity = Vector2.zero;
            foreach (var bone in bones) {
                oldVelocity += bone.GetComponent<Rigidbody2D>().linearVelocity;
            }
            oldVelocity /= bones.Count;

            Destroy(Player);

            playerPositionDummy.transform.position = pos;

            Player = playerPositionDummy;

            StateMachine.etatPhysiqueStore.linearVelocity = oldVelocity;
        }


    }
}