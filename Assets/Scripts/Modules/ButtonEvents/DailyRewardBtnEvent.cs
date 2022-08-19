using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardBtnEvent : MonoBehaviour
{
    public int Daily;
    public int Gold;
    public int Gas;
    public int Ruby;

    private void Awake()
    {
       if(PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.dailyReward] > Daily)
        {
            this.gameObject.GetComponent<Image>().color = Color.yellow;
        }
    }
}