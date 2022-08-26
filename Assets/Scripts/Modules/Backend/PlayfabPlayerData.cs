using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabPlayerData : Singleton<PlayfabPlayerData>
{
    string MyPlayfabID;
    int serverStage;
    int PlayerHP;

    [SerializeField]
    Player player;

    protected override void Awake()
    {
        base.Awake();
        GetAccountInfo();
        Time.timeScale = 0;
    }

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
            PlayerHP = int.Parse(result.Data["hp"].Value);
            if (result.Data.ContainsKey("LastStage"))
            {
                serverStage = int.Parse(result.Data["LastStage"].Value);
               
                Time.timeScale = 1;
                if(serverStage == 0)
                {
                    UIManager.Instance.StartTutorial();
                    //First Start TODO

                }
                else if(serverStage < 10000)
                {
                    // Stage가 1만 보다 낮으면 현재 App 버전이 22 버전 아래이므로 초기화 시켜줘야 한다.
                    // Stage에서 현재 챕터, 01 현재 스테이지 01 현재 웨이브를 의미한다.
                    // 차후 디바이스를 껐을 때 마지막 저장 웨이브로 이동하게끔 할 수 있을 것 같다.
                    // 다만 Level 정보, 스킬 정보 등을 어떻게 저장하느냐도 문제가 될 것 같다.
                    SetUserLastStageUpdate(10101);
                }
            }
            player.RequestPlayerData(PlayerHP);
                
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });

    }

    public void SetUserLastStageUpdate(int stage)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {

            {"LastStage",stage.ToString()},
        }
        },
        result => { Debug.Log("Successfully updated user data"); PlayFabData.instance.GetUserData(); },
        error => {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
