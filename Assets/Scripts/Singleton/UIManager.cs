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
            //for (int i = 14; i < -1; i++)
            //{
            //    if(curExp < maxExp * (i*15))
            //}
        }
    
    }
    public void ChangeHpBar(UI UiState, int curhp, int maxHp)
    {
        for (int i = 9; i > -1; i--)
        {
            //Debug.Log(maxHp * (i * 0.1f));
            if(curhp < maxHp * (i*0.1f) && curhp != maxHp * (i * 0.1f))
            {
                Debug.Log(curhp + "," + maxHp * (i * 0.1f));
                UIList[(int)UiState].transform.GetChild(i).GetComponent<Image>().sprite = playerHpSprite[0]; 
            }
            else if(curhp >= maxHp * (i  * 0.1f))
            {
                UIList[(int)UiState].transform.GetChild(i).GetComponent<Image>().sprite = playerHpSprite[1];
            }
        }
        //UIList[(int)UiState].
    }
}
