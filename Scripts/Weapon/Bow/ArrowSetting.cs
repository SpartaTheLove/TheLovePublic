using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSetting : MonoBehaviour
{
    public Transform target;


    void Update()
    {
        this.transform.LookAt(target);    
    }
}
