using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Quest/Quest/SaveRegist",fileName ="QuestNo_")]
public abstract class QuestSystemSaveAndRegist : ScriptableObject
{
    [SerializeField]
    private Quest quest;
    [SerializeField]
    private Category category;
    [SerializeField]
    private TaskTarget target;


    public void Setup()
    {
        var questSystem = QuestSystem.Instance;


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
    }

    public void RecevieReport()
    {
        QuestSystem.Instance.ReceiveReport(category, target, 1);
    }
}