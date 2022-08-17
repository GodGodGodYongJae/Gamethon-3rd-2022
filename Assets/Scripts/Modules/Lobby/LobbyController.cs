using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    [SerializeField]
    GameObject[] LobbyObj;
    // Start is called before the first frame update
    string MyPlayfabID;
    int serverStage;

    int DailyCoin;
    int CurrentDaily;
    int CurrentRward;

    DailyRewardBtnEvent SelectDR;

    #region server
    private void Awake()
    {
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
        print("1");
        if(DR.Daily == CurrentRward && DailyCoin > 0)
        {
            print("2");
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "SubVirtualCurrency",
                FunctionParameter = new { Amount = 1, type = VC_DR },
                GeneratePlayStreamEvent = true

            },
            cloudResult => { print("3"); SetUserDR(DR);  },
            error => { Debug.Log(error.GenerateErrorReport()); });
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
        CurrentRward += 1;
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"dailyReward",CurrentRward.ToString() }
        }
        },
        result => { Debug.Log("Successfully updated user data");
            if (DR.Gold > 0)
                RewardAdd("DM", DR.Gold);
            if (DR.Gas > 0)
                RewardAdd("ST", DR.Gas);
            if (DR.Ruby > 0)
                RewardAdd("RU", DR.Ruby);
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
        cloudResult => { Debug.Log("저장성공!"); },
        error => { }
        ); ; ;
    }

    #endregion

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
                CurrentRward = int.Parse(result.Data["dailyReward"].Value);
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
