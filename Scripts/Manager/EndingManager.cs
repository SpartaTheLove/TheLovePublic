using UnityEngine;

public enum EndingType
{
    KillBoss,
    PlayerDie,
    Hidden
}

public class EndingManager : Singleton<EndingManager>
{
    [SerializeField] private GameObject[] _endingImages;

    public void CheckEnding(EndingType endingType)
    {
        switch(endingType)
        {
            case EndingType.KillBoss:
                // TODO :: 플레이어 구현시 연동
                //if(CharacterManager.Instance.Player.Inventory.curEquip == "정화의화살") EnableEndingImage(0);
                //else  EnableEndingImage(2);
                EnableEndingImage(0);
                SoundManager.Instance.Play(3, 0.2f, SoundType.Bgm);
                break;
            case EndingType.PlayerDie:
                EnableEndingImage(1);
                SoundManager.Instance.Play(2, 0.2f, SoundType.Bgm);
                break;
            case EndingType.Hidden:
                EnableEndingImage(4);
                SoundManager.Instance.Play(3, 0.2f, SoundType.Bgm);
                break;
        }
    }

    void EnableEndingImage(int idx)
    {
        GameManager.Instance.IsPlay = false;
        _endingImages[idx].SetActive(true);
    }
}
