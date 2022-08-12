using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public enum UI {
    ChapterName,
    StageNum,
    HpBar,
    HPText,
    ExpBar,
    LevelText
    }

    [SerializeField]
    private Sprite[] playerHpSprite;
    [SerializeField]
    private Sprite[] expSprite;
    public GameObject[] UIList;

    public void TextChange(UI UiState, string text)
    {
        int UINum = (int)UiState;
        UIList[UINum].GetComponent<Text>().text = text;
    }
    public void ChangeExpBar(int curExp,int level)
    {
        int maxExp = 150;
        if(curExp >= 150)
        {
            foreach (Transform item in UIList[(int)UI.ExpBar].transform)
            {
                item.GetComponent<Image>().sprite = expSprite[0];
            }
        }
        else
        {
            for (int i = 15; i < -1; i++)
            {
                //if (curExp < maxExp * (i * 15))
            }
        }
    
    }
    public void ChangeHpBar(UI UiState, int curhp, int maxHp)
    {
        for (int i = 9; i > -1; i--)
        {
            //Debug.Log(maxHp * (i * 0.1f));
            if(curhp < maxHp * (i*0.1f) && curhp != maxHp * (i * 0.1f))
            {
                //Debug.Log(curhp + "," + maxHp * (i * 0.1f));
                UIList[(int)UiState].transform.GetChild(i).GetComponent<Image>().sprite = playerHpSprite[0]; 
            }
            else if(curhp >= maxHp * (i  * 0.1f))
            {
                UIList[(int)UiState].transform.GetChild(i).GetComponent<Image>().sprite = playerHpSprite[1];
            }
        }
        //UIList[(int)UiState].
    }

    [SerializeField]
    GameObject RespawnButton;

    [SerializeField]
    GameObject RespawnObj;

    [SerializeField]
    GameObject DeathCinemachine;

    [SerializeField]
    Player Player;
    public void CharaterRespwan()
    {
        DeathCinemachine.SetActive(false);
        RespawnButton.SetActive(false);
        RespawnObj.SetActive(false);
        Player.Respawn();
    }

    #region Tutorial UI
    [SerializeField]
    private GameObject TutorialObj;
    [SerializeField]
    private GameObject[] t_button;
    [SerializeField]
    private Sprite[] TutorialSprite;
    private short t_currentPage;
    public void StartTutorial()
    {
        // Game Stop
        Time.timeScale = 0;
        t_currentPage = 0;
        TutorialObj.SetActive(true);
       TutorialObj.GetComponent<Image>().sprite = TutorialSprite[t_currentPage];
        t_button[0].SetActive(true);
    }
    public void OnNextTutorial()
    {
        t_currentPage++;
        TutorialObj.GetComponent<Image>().sprite = TutorialSprite[t_currentPage];
        Debug.Log((t_currentPage + 1) + "," + TutorialSprite.Length);
        if(t_currentPage+1 >= TutorialSprite.Length)
        {
            t_button[0].SetActive(false);
            t_button[1].SetActive(true);
        }
    }

    public void OnComplateTutorial()
    {
        Time.timeScale = 1;
        t_currentPage = 0;
        TutorialObj.SetActive(false);
        foreach (var item in t_button)
        {
            item.SetActive(false);
        }
        PlayfabPlayerData.instance.SetUserLastStageUpdate(111);
    }

    #endregion
}
