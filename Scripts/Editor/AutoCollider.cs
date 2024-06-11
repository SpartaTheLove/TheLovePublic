using UnityEngine;
using UnityEditor;


public class AutoCollider : MonoBehaviour
{
    [MenuItem("Tool/AutoCollider")]
    public static void AdjustBounds()
    {
        GameObject selectedGameObject = Selection.activeGameObject;
        if (selectedGameObject != null)
        {
            Renderer mr = selectedGameObject.GetComponentInChildren<MeshRenderer>();
            if (mr == null) mr = selectedGameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            var box = selectedGameObject.GetComponentInChildren<BoxCollider>();
            if (box == null)
            {
                box = selectedGameObject.AddComponent<BoxCollider>();
            }
            box.center = mr.bounds.center - selectedGameObject.transform.position;
            box.size = mr.bounds.size;
        }
    }
}