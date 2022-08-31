using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystemSaveTest : MonoBehaviour
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

        //questSystem.Load();

        //Debug.Log(questSystem.ContainsInCompleteQuests(quest));
        //Debug.Log(questSystem.ContainsInActiveQuests(quest));
        //Debug.Log(questSystem.ActiveQuests.Count);
        //Debug.Log(questSystem.CompletedQuests.Count);
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
                 //PlayerPrefs.DeleteAll();
                 //PlayerPrefs.Save();
             };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            QuestSystem.Instance.ReceiveReport(category, target, 1);
    }
}
