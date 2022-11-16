using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class illiustbookPage : MonoBehaviour
{

    public SerializableDictionary<int, bookPage> serial;
    private QuestSystem questSystem;

    private void Awake()
    {
        questSystem = QuestSystem.Instance;
    }
    public void OnClickillustPage()
    {
        foreach (var item in serial)
        {
            bookPage ibook = item.Value;
            Quest iquest = ibook.m_quest;
            Debug.Log(questSystem.ContainsInCompleteQuests(iquest));
            if (questSystem.ContainsInCompleteQuests(iquest).Equals(true))
            {
                //iquest = questSystem.SerchQuest(iquest);
                    ibook.gameObject.SetActive(true);
            }
        }
        //foreach (var item in serial)
        //{
        //    bookPage ibook = item.Value;

        //    if(ibook.m_quest.IsComplete)
        //    {
        //        Debug.Log("Quest Check. " + item.Key);
        //        ibook.gameObject.SetActive(true);
        //    }
        //}
    }
}
