using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsPlay = false;
    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (IsPlay)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    public void StartGame(GameObject go)
    {
        ButtonManager.Instance.OnClickCancel(go);
        IsPlay = true;
        SoundManager.Instance.Play(1, 0.2f, SoundType.Bgm);
    }
}