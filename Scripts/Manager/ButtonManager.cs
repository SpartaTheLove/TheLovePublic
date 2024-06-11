using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : Singleton<ButtonManager>
{
    public void OnClickOpen(GameObject go)
    {
        SoundManager.Instance.Play(4, 0.2f);
        go.SetActive(true);    
    }
    public void OnClickCancel(GameObject go)
    {
        SoundManager.Instance.Play(4, 0.2f);
        go.SetActive(false);
    }

    public void Toggle(GameObject go)
    {
        SoundManager.Instance.Play(4, 0.2f);
        go.SetActive(!IsOpen(go));
    }

    public bool IsOpen(GameObject go)
    {
        SoundManager.Instance.Play(4, 0.2f);
        return go.activeInHierarchy;
    }

    public void RestartGameBtn()
    {
        SoundManager.Instance.Play(4, 0.2f);
        SceneManager.LoadScene("MainScene");
    }
}
