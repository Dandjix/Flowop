using EtatsPhysiques;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.XR;

[RequireComponent(typeof(SpriteSkin))]
public class JoueurVisqueux : MonoBehaviour
{
    //[SerializeField] private EtatPhysiqueVisqueux etatPhysiqueVisqueux;

    private SpriteSkin spriteSkin;

    private static int getBoneNumber(Transform bone)
    {
        return int.Parse(bone.name.Substring(5));
    }

    [SerializeField] private float frequency_adjacent;
    public float Frequency_Adjacent { get
        {
            return frequency_adjacent;
        }
        set
        {
            frequency_adjacent = value;
            ApplyValues();
        }
    }
    [Range(0, 1)][SerializeField] private float damping_adjacent;
    public float Damping_Adjacent
    {
        get
        {
            return damping_adjacent;
        }
        set
        {
            damping_adjacent = value;
            ApplyValues();
        }
    }

    [SerializeField] private float frequency_opposite;
    [Range(0, 1)][SerializeField] private float damping_opposite;

    [SerializeField] private float originalGravity = 1;

    public List<Transform> GetSortedBones()
    {
        List<Transform> sortedBones = new List<Transform>(transform.childCount);
        foreach (Transform child in transform)
        {
            if (!child.name.StartsWith("bone_"))
            {
                continue;
            }
            sortedBones.Add(child);
        }


        sortedBones.Sort((a, b) =>
        {
            int boneNumberA = getBoneNumber(a);
            int boneNumberB = getBoneNumber(b);
            return boneNumberA - boneNumberB;
        });

        return sortedBones;
    }

    public void GenerateSprings(EtatPhysiquesStateMachine stateMachine)
    {
        var bones = GetSortedBones();

        foreach (var bone in bones)
        {
            var rb = bone.gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = originalGravity;

            bone.gameObject.AddComponent<CircleCollider2D>();

            bone.gameObject.AddComponent<StateMachineReference>().StateMachine = stateMachine;

            var prendrePiece = bone.gameObject.AddComponent<PrendrePiece>();
            prendrePiece.typePiece = PrendrePiece.TypePieceRamassee.PieceVerte;
        }


        for (int i = 0; i < bones.Count; i++)
        {
            int indexOfBoneToTheRight = (i + 1) % bones.Count;
            int indexOfBoneToTheLeft = (i - 1) % bones.Count;
            if (indexOfBoneToTheLeft < 0)
                indexOfBoneToTheLeft += bones.Count;

            int indexOfOpposingBone = (i + bones.Count / 2) % bones.Count;

            var bone = bones[i].gameObject;

            var visqueuxBone = bone.AddComponent<VisqueuxBone>();

            visqueuxBone.Attach(bones[indexOfBoneToTheRight], bones[indexOfOpposingBone], this);

        }
    }

    [HideInInspector] public int FixedFramesTillNextStick = 0;

    private float gracePeriod = 0f;

    public bool AllowSticking => gracePeriod > 0f;

    public void UnStick(float gracePeriodTotal)
    {
        gracePeriod = gracePeriodTotal;
        foreach (var bone in GetSortedBones())
        {
            var visqueuxBone = bone.GetComponent<VisqueuxBone>();
            visqueuxBone.Sticking = false;
        }
    }
    public void UnStick(int frames)
    {
        FixedFramesTillNextStick = frames;
        foreach (var bone in GetSortedBones())
        {
            var visqueuxBone = bone.GetComponent<VisqueuxBone>();
            visqueuxBone.Sticking = false;
        }
    }

    private void ApplyValues()
    {
        var bones = GetSortedBones();
        foreach (var bone in bones)
        {
            var visqueuxBone = bone.GetComponent<VisqueuxBone>();

            visqueuxBone.frequency_adjacent = frequency_adjacent;
            visqueuxBone.dampingRatio_adjacent = damping_adjacent;

            visqueuxBone.frequency_opposite = frequency_opposite;
            visqueuxBone.dampingRatio_opposite = damping_opposite;
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        ApplyValues();
#endif

        gracePeriod = Mathf.Max(0, gracePeriod - Time.deltaTime);
    }

    private void FixedUpdate()
    {
        FixedFramesTillNextStick = Mathf.Max(0, FixedFramesTillNextStick - 1);
    }

    //public void Setup()
    //{
    //    spriteSkin = GetComponent<SpriteSkin>();

    //    GenerateSprings();
    //}

    public Vector2 Center 
    {
        get
        {
            var bones = GetSortedBones();

            Vector2 posSum = Vector2.zero;

            foreach (var bone in bones)
            {
                posSum = posSum + (Vector2)bone.position;
            }

            var posAvg = posSum / bones.Count;

            return posAvg;
        }
        set //TODO: FIx this flipping the bones
        {
            Vector2 blobPosition = Center;

            Vector2 diff = value - blobPosition;

            transform.position = (Vector2) transform.position + diff;

            //var bones = GetSortedBones();

            //foreach (var bone in bones)
            //{
            //    Vector2 localPos = (Vector2)bone.transform.position - blobPosition;
            //    Vector2 outputPos = value + localPos;
            //    bone.GetComponent<VisqueuxBone>().SetPosition(outputPos);
            //    bone.position = value + localPos;
            //}
        }
    }

    public void ResetBonesPositions()
    {
        Vector2 center = Center;
        foreach(var bone in GetSortedBones())
        {
            bone.position = center + bone.GetComponent<VisqueuxBone>().originalLocalPos;
            bone.rotation = bone.GetComponent<VisqueuxBone>().originalRotation;
        }
    }
}
