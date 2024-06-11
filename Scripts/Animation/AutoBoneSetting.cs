using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoBoneSetting : MonoBehaviour
{
    public List<Transform> bones;

    public Transform rootBone;

    private void Start()
    {
        var skin = GetComponentInChildren<SkinnedMeshRenderer>();
        bones = skin.bones.ToList();
        var newBones = new Transform[skin.bones.Length];
        for (int i = 0; i < bones.Count; i++)
        {
            var target = FindChild(bones[i].name, rootBone);
            newBones[i] = target;
        }
        skin.bones = newBones;
        skin.rootBone = rootBone;
        bones = skin.bones.ToList();
    }

    public Transform FindChild(string name, Transform target)
    {
        if (target.name == name) return target;
        else
        {
            for (int i = 0; i < target.childCount; i++)
            {
                var child = target.GetChild(i);
                var newTarget = FindChild(name, child);
                if (newTarget != null) return newTarget;
            }
        }
        return null;
    }
}