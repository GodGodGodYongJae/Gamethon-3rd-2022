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
    // Start is called before the first frame update
    string MyPlayfabID;
    int serverStage;

    int DailyCoin;
    public int CurrentDaily;


    [SerializeField]
    PlayFabDM DMController;
    [SerializeField]
    GameObject WaitPannel;

    protected override void Awake()
    {
        base.Awake();
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            GetAccountInfo();
            GetDR();
        }
    }
    #region daily Reward
    const string VC_DR = "DR";


    public void OnReward(DailyRewardBtnEvent DR)
    {

        if(DR.Daily == CurrentDaily && DailyCoin > 0)
        {
            WaitPannel.SetActive(true);
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "SubVirtualCurrency",
                FunctionParameter = new { Amount = 1, type = VC_DR },
                GeneratePlayStreamEvent = true

            },
            cloudResult => {  SetUserDR(DR);  },
            error => { Debug.Log(error.GenerateErrorReport()); });
        }
        else
        {
            WaitPannel.SetActive(false);
        }
    }
    void GetDR()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest()
        , (result) =>
        {

            result.VirtualCurrency.TryGetValue(VC_DR, out int _DailyReward);
            DailyCoin = _DailyReward;

        }, (error) =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
 
    }

    void SetUserDR(DailyRewardBtnEvent DR)
    {
        CurrentDaily += 1;
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"dailyReward",CurrentDaily.ToString() }
        }
        },
        result => { Debug.Log("Successfully updated user data");
            if (DR.Gold > 0)
                RewardAdd("DM", DR.Gold);
            if (DR.Gas > 0)
                RewardAdd("ST", DR.Gas);
            if (DR.Ruby > 0)
                RewardAdd("RU", DR.Ruby);

            DR.GetComponent<Image>().color = Color.yellow;
            DailyCoin = 0;
        },
        error => {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void RewardAdd(string Type,int ammount)
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "AddVirtualCurrency",
            FunctionParameter = new { Amount = ammount, type = Type },
            GeneratePlayStreamEvent = true
        },
        cloudResult => { Debug.Log("저장성공!"); DMController.GetDR(); WaitPannel.SetActive(false); },
        error => { }
        ); ; ;
    }

    #endregion

    #region GetData
    void GetAccountInfo()
    {
        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, Successs, fail);
    }

    void Successs(GetAccountInfoResult result)
    {

        MyPlayfabID = result.AccountInfo.PlayFabId;
        GetUserData();

    }


    void fail(PlayFabError error)
    {

        Debug.LogError(error.GenerateErrorReport());
    }

    void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = MyPlayfabID,
            Keys = null
        }, result => {
            Debug.Log("Got user data:");
            if (result.Data.ContainsKey("LastStage"))
            {
                CurrentDaily = int.Parse(result.Data["dailyReward"].Value);
                serverStage = int.Parse(result.Data["LastStage"].Value);
                if (serverStage == 0)
                {
                    LobbyObj[0].SetActive(true);

                    //First Start TODO

                }
                else
                    LobbyObj[1].SetActive(true);
            }

        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });

    }
    #endregion

 

}
