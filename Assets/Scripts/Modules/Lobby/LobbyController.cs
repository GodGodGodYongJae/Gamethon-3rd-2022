using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : Singleton<LobbyController>
{
    [SerializeField]
    GameObject[] LobbyObj;

    [SerializeField]
    public GameObject WaitPannel;


    protected override void Awake()
    {
        base.Awake();
        int Las = PlayFabData.instance.PlayerStatus[PlayFabData.Stat.LastStage];
        if (Las == 0)
            LobbyObj[0].SetActive(true);
        else
            LobbyObj[1].SetActive(true);
    }

    #region daily Reward
    const string VC_DR = "DR";


    public void OnReward(DailyRewardBtnEvent DR)
    {

        if (DR.Daily == PlayFabData.instance.PlayerStatus[PlayFabData.Stat.dailyReward] 
            && PlayFabData.instance.PlayerDailyCount > 0)
        {
            WaitPannel.SetActive(true);
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "SubVirtualCurrency",
                FunctionParameter = new { Amount = 1, type = VC_DR },
                GeneratePlayStreamEvent = true

            },
            cloudResult => { SetUserDR(DR); },
            error => { Debug.Log(error.GenerateErrorReport()); });
        }
        else
        {
            WaitPannel.SetActive(false);
        }
    }
 

    void SetUserDR(DailyRewardBtnEvent DR)
    {
        PlayFabData.instance.PlayerStatus[PlayFabData.Stat.dailyReward] += 1;
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"dailyReward",PlayFabData.instance.PlayerStatus[PlayFabData.Stat.dailyReward].ToString() }
        }
        },
        result =>
        {
            Debug.Log("Successfully updated user data");
            if (DR.Gold > 0)
                RewardAdd("DM", DR.Gold);
            if (DR.Gas > 0)
                RewardAdd("ST", DR.Gas);
            if (DR.Ruby > 0)
                RewardAdd("RU", DR.Ruby);

            DR.GetComponent<Image>().color = Color.yellow;
            DR.transform.Find("Complate").gameObject.SetActive(true);
            PlayFabData.instance.PlayerDailyCount = 0;
        },
        error =>
        {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void RewardAdd(string Type, int ammount)
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "AddVirtualCurrency",
            FunctionParameter = new { Amount = ammount, type = Type },
            GeneratePlayStreamEvent = true
            
        },
        cloudResult => {  PlayFabData.instance.GetAccountData(); WaitPannel.SetActive(false); },
        error => { }
        );

    }
    #endregion
}

