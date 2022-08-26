using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChapterSelectBtn : MonoBehaviour
{
    [SerializeField]
    GameObject[] ChapterBtn;
    // Start is called before the first frame update
    private void OnEnable()
    {
        for (int i = 0; i < PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.ClearChapter]; i++)
        {
            ChapterBtn[i].SetActive(true);
        }
    }
    public void OnSelectChpater(int select)
    {
        ScenesManager.Instance.StartChpater = (sbyte)select;
    }
}
