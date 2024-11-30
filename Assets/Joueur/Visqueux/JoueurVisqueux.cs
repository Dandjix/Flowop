using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.XR;

[RequireComponent(typeof (SpriteSkin))]
public class JoueurVisqueux : MonoBehaviour
{
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
    [Range(0,1)][SerializeField] private float damping_adjacent;
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

    public List<Transform> GetSortedBones()
    {
        List<Transform> sortedBones = new List<Transform>(transform.childCount);
        foreach (Transform child in transform)
        {
            if(!child.name.StartsWith("bone_"))
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

    public void generateSprings()
    {
        //while(transform.childCount>0)
        //{
        //    Destroy(transform.GetChild(0));
        //}

        //spriteSkin.gene


        var bones = GetSortedBones();

        foreach (var bone in bones)
        {
            bone.AddComponent<Rigidbody2D>();
            bone.AddComponent<CircleCollider2D>();
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

            visqueuxBone.Attach(bones[indexOfBoneToTheRight], bones[indexOfOpposingBone]);

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
    }

    private void Awake()
    {
        spriteSkin = GetComponent<SpriteSkin>();

        generateSprings();
    }
}
