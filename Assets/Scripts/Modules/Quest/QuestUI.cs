using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField]
    private Quest quest;
    GameObject RewardBtn;
    GameObject ComplateImg;
    QuestSystem questSystem;

    [System.Obsolete]
    void Awake()
    {
        questSystem = QuestSystem.Instance;
        RewardBtn = transform.FindChild("RewardBtn").gameObject;
        ComplateImg = transform.FindChild("ComplateImg").gameObject;

    }

    private void OnEnable()
    {
 
        if (questSystem.ContainsInActiveQuests(quest).Equals(true))
        {
            quest = questSystem.SerchQuest(quest);
            if (quest == null)
                return;
            if (quest.State == QuestState.WaitingForCompletion)
            {
                RewardBtn.SetActive(true);
            }
        }
        else if (questSystem.ContainsInCompleteQuests(quest).Equals(true))
        {
            ComplateImg.SetActive(true);
        }
    }

    public void OnClickRewardBtn()
    {
        quest.Complete();
        RewardBtn.SetActive(false);
        ComplateImg.SetActive(true);
    }
}
