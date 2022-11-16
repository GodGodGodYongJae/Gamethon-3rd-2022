using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameQuestController : Singleton<InGameQuestController>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void OnReportQuest(InGameTargetQuest target)
    {
        if (target.category != null && target.taskTarget != null)
            QuestSystem.Instance.ReceiveReport(target.category, target.taskTarget, 1);
    }
}
