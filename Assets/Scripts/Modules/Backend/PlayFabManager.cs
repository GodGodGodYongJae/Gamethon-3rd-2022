using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using GooglePlayGames;
using TMPro;


public class PlayFabManager : Singleton<PlayFabManager>
{
    public TextMeshProUGUI LogText;
    private bool isLogin;

    protected override void Awake()
    {
        base.Awake();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        GoogleLogin();
    }

    #region Login
    public void GoogleLogin()
    {
        Social.localUser.Authenticate((success) =>
        {
            if (success) { LogText.text = "Push To Start!"; PlayFabLogin(); isLogin = true;  GetStats(); }
            else { LogText.text = "Faild Login!";  }
            
        });
    }

    public void GoogleLogout()
    {
        LogText.text = "구글 로그아웃";
    }

    public void PlayFabLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = Social.localUser.id + "@rand.com", Password = Social.localUser.id };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => LogText.text = "Wait Please." + Social.localUser.userName, (error) => PlayFabRegister());
    }

    public void PlayFabRegister()
    {
        var request = new RegisterPlayFabUserRequest { Email = Social.localUser.id + "@rand.com", Password = Social.localUser.id, Username = Social.localUser.userName };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => { LogText.text = "Wait Please.."; PlayFabLogin(); }, (error) => LogText.text = "Push To Start!");
    }

    public void OnTouchStart()
    {
        if (isLogin)
            ScenesManager.Instance.OnLobbyScene();
    }
    #endregion

    public int playerHp;
    public int playerAtk;
    public int playerDef;
    public int playerGas;
    public int Diamond;
    #region PlayerStats
    //public void SetStats()
    //{
    //    PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
    //    {
    //        Statistics = new List<StatisticUpdate> { 
    //        new StatisticUpdate{ StatisticName = "playerHp", Value = playerHp},
    //        new StatisticUpdate{ StatisticName = "playerAtk", Value = playerAtk},
    //        new StatisticUpdate{ StatisticName = "playerDef", Value = playerDef},
    //        new StatisticUpdate{ StatisticName = "playerGas", Value = playerGas},
    //        }

    //    }, result=> { Debug.Log("user Statistics Update"); },
    //    error=> { Debug.LogError(error.GenerateErrorReport()); });
    //}

    public void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStats,
            error=> Debug.LogError(error.GenerateErrorReport())
            );
    }

    void OnGetStats(GetPlayerStatisticsResult result)
    {
        Debug.Log(result.Statistics.Count);
        if(result.Statistics.Count <= 0)
        {
            playerHp = 100;
            playerAtk = 80;
            playerDef = 10;
            playerGas = 0;
            StartCloudUpdatePlayerState();
            GetStats();
            //SetStats();
        }
        else
        {
            foreach (var eachStat in result.Statistics)
            {
                Debug.Log("Statistic (" + eachStat.StatisticName + "):" + eachStat.Value);
                switch (eachStat.StatisticName)
                {
                    case "hp":
                        playerHp = eachStat.Value;
                        break;
                    case "atk":
                        playerAtk = eachStat.Value;
                        break;
                    case "def":
                        playerDef = eachStat.Value;
                        break;
                    case "gas":
                        playerGas = eachStat.Value;
                        break;
                    default:
                        break;
                }
            }
        }
       
    }


    public  void StartCloudUpdatePlayerState()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {  
            FunctionName = "UpdatePlayerState",
            FunctionParameter = new { hp = playerHp, atk = playerAtk,def = playerDef,gas = playerGas},
            GeneratePlayStreamEvent = true
        }
        ,OnCloudUpdate,OnErrorShared);

    }

    private static void OnCloudUpdate(ExecuteCloudScriptResult result)
    {
        //JsonObject jsonResult = (JsonObject)result.FunctionResult;
        //jsonResult.TryGetValue("messageValue", out object messageValue);
        //Debug.Log((string)messageValue);
    }
    private static void OnErrorShared(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
    #endregion
}
