using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
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


    #region hp
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
    public bool ChangeExpBar(ref int curExp, ref int level)
    {
        int maxExp = 150;

        if(curExp >= maxExp)
        {
            for (int i = 1; i < 16; i++)
            {
                UIList[(int)UI.ExpBar].transform.GetChild(i-1).GetComponent<Image>().sprite = expSprite[0];
               
            }
            curExp = 0;
            level++;
            string leveltext = "LV :" + level.ToString();
            TextChange(UI.LevelText, leveltext);
            return true;
        }
        else
        {
            for (int i = 1; i < 16; i++)
            {
                Debug.Log(curExp + "," + i * 10);
                if (curExp >= i * 10) UIList[(int)UI.ExpBar].transform.GetChild(i - 1).GetComponent<Image>().sprite = expSprite[1];
                else break;
            }
            return false;
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
    #endregion

    #region respawn
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
    #endregion

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
        TutorialObj.transform.GetChild(1).GetComponent<Image>().sprite = TutorialSprite[t_currentPage];
        t_button[0].SetActive(true);
        PlayfabPlayerData.instance.SetUserLastStageUpdate(10101);
    }
    public void OnNextTutorial()
    {
        t_currentPage++;
        TutorialObj.transform.GetChild(1).GetComponent<Image>().sprite = TutorialSprite[t_currentPage];
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
        PlayfabPlayerData.instance.SetUserLastStageUpdate(10101);
    }

    #endregion

    [SerializeField]
    Image SkillCoolDownImage;
    public void SkilCoolDown(float amount)
    {
        SkillCoolDownImage.fillAmount = amount;
    }


    #region 100다이아 부활
    public void OnRespawnBtn()
    {
        if (PlayFabData.Instance.PlayerDiamond >= 100)
        {
            CharaterRespwan();
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "SubVirtualCurrency",
                FunctionParameter = new { Amount = 100, type = "DM" },
                GeneratePlayStreamEvent = true

            },
           cloudResult =>
           {
               PlayFabData.Instance.GetAccountData();
           },
           error =>
           {
               Debug.Log("Got error setting user data Ancestor to Arthur");
               Debug.Log(error.GenerateErrorReport());
           });
        }
    }
    #endregion
}
