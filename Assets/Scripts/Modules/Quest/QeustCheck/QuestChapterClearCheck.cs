using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChapterClearCheck : MonoBehaviour
{
    [SerializeField]
    private Quest quest;
    [SerializeField]
    private Category category;
    [SerializeField]
    private TaskTarget target;

    void Start()
    {
        var questSystem = QuestSystem.Instance;
        int LastStage = PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.LastStage];

        if (
            questSystem.ContainsInCompleteQuests(quest).Equals(false)
            && questSystem.ContainsInActiveQuests(quest).Equals(false)
          )
        {
            Debug.Log("Register");
            var newQuest = questSystem.Register(quest);
            QuestSystem.Instance.Save();
        }
        else
        {
            questSystem.onQuestCompleted += (quest) =>
            {
                Debug.Log("Complated");
                QuestSystem.Instance.Save();

            };
        }

        if (questSystem.ContainsInActiveQuests(quest).Equals(true))
        {
            string Values = target.Value as string;
            if(int.Parse(Values) <= LastStage)
            {
                QuestSystem.Instance.ReceiveReport(category, target, 1);
            }
        }
    }


}
