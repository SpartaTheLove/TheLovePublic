using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using static UnityEngine.UI.Button;
using UnityEngine;

public class AnimEventListener : MonoBehaviour
{
    [SerializeField] private ButtonClickedEvent PullEvent;
    [SerializeField] private ButtonClickedEvent EnableEvnet;

    public void Pull()
    {
        PullEvent.Invoke();
    }

    public void EnableArrow()
    {
        EnableEvnet.Invoke();
    }
}
