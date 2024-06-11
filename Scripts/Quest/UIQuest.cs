using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIQuest : MonoBehaviour
{
    [Header("QuestList")]
    [SerializeField] private List<GameObject> _questListUI = new List<GameObject>();

    [Header("QuestData")]
    [SerializeField] private TextMeshProUGUI _questName;
    [SerializeField] private TextMeshProUGUI _questDescription;
    [SerializeField] private TextMeshProUGUI _questRewards;
    [SerializeField] private GameObject _questAcceptBtn;
    [SerializeField] private GameObject _questProgressUI;
    [SerializeField] private GameObject _questClearBtn;
    [SerializeField] private GameObject _questCompletionUI;

    private int _nowQuestIdx;

    private void Awake()
    {
        SetQuestList();
        gameObject.SetActive(false);
    }

    void SetQuestList()
    {
        for (int i = 0; i < _questListUI.Count; i++)
        {
            _questListUI[i].GetComponentInChildren<TextMeshProUGUI>().text = $"{QuestManager.Instance.QuestData[i].QuestID}. {QuestManager.Instance.QuestData[i].QuestName}";
        }
    }

    public void UpdateQuestDataUI(int idx)
    {
        _nowQuestIdx = idx;

        QuestData data = QuestManager.Instance.QuestData[idx];

        _questName.text = data.QuestName;
        _questDescription.text = data.QuestDescription;

        string _rewardList = "";
        foreach(GameObject obj in data.Rewards)
        {
            _rewardList += $"º¸»ó : {obj.GetComponent<Item>().data.itemName}\n";
        }
        _questRewards.text = _rewardList;

        DisableAllBtn();
        if (data.GetRewards) _questCompletionUI.SetActive(true);

        if (!data.IsProcessing && !data.IsClear)
        {
            _questAcceptBtn.SetActive(true);
        }
        else if (data.IsProcessing && !data.IsClear)
        {
            _questProgressUI.SetActive(true);
        }
        else if (data.IsProcessing && data.IsClear && !data.GetRewards)
        {
            _questClearBtn.SetActive(true);
        }

    }

    void DisableAllBtn()
    {
        _questAcceptBtn.SetActive(false);
        _questProgressUI.SetActive(false);
        _questClearBtn.SetActive(false);
        _questCompletionUI.SetActive(false);
    }

    public void OnClickAcceptBtn()
    {
        SoundManager.Instance.Play(4, 0.2f);
        QuestManager.Instance.AcceptQuest(QuestManager.Instance.QuestData[_nowQuestIdx]);
        UpdateQuestDataUI(_nowQuestIdx);
    }

    public void OnClickClearBtn()
    {
        SoundManager.Instance.Play(4, 0.2f);
        QuestManager.Instance.GetRewards(QuestManager.Instance.QuestData[_nowQuestIdx]);
        UpdateQuestDataUI(_nowQuestIdx);
        _questClearBtn.SetActive(false);
    }
}
