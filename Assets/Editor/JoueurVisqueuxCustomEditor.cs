using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JoueurVisqueux))]
public class JoueurVisqueuxCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("calculer distances"))
        {
            JoueurVisqueux j = (JoueurVisqueux)target;

            
            
            var bones = j.GetSortedBones();

            var first = bones[0];

            var adjacent = bones[1];

            var opposite = bones[bones.Count / 2];

            Debug.Log("Adjacent distance : " + Vector2.Distance(first.position, adjacent.position));
            Debug.Log("Opposite distance : " + Vector2.Distance(first.position, opposite.position));

            Debug.Log("Adjacent distance scaled : " + Vector2.Distance(first.position, adjacent.position)*j.transform.localScale.x);
            Debug.Log("Opposite distance scaled : " + Vector2.Distance(first.position, opposite.position)*j.transform.localScale.x);

        }
    }
}
