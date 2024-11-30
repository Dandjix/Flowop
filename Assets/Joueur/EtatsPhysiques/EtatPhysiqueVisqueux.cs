namespace EtatsPhysiques
{
    using UnityEngine;

    public class EtatPhysiqueVisqueux : EtatPhysiqueState
    {
        [SerializeField] private GameObject joueurVisqueux;

        private Vector2 Center { get
            {
                var joueur = joueurVisqueux.GetComponent<JoueurVisqueux>();
                var bones = joueur.GetSortedBones();

                Vector2 posSum = Vector2.zero;

                foreach (var bone in bones)
                {
                    posSum = posSum + (Vector2)bone.position;
                }

                var posAvg = posSum/bones.Count;

                return posAvg;
            }
            set
            {
                Vector2 blobPosition = Center;

                var joueur = joueurVisqueux.GetComponent<JoueurVisqueux>();
                var bones = joueur.GetSortedBones();

                foreach (var bone in bones)
                {
                    Vector2 localPos = blobPosition - (Vector2)bone.transform.position;
                    bone.position = value + localPos;
                }
            }
        }

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