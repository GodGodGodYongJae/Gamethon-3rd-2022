using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabPlayerData : Singleton<PlayfabPlayerData>
{
    string MyPlayfabID;
    int serverStage;
    protected override void Awake()
    {
        base.Awake();
        GetAccountInfo();
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
            if (result.Data.ContainsKey("LastStage"))
            {
                serverStage = int.Parse(result.Data["LastStage"].Value);
                if(serverStage == 0)
                {
                    UIManager.Instance.StartTutorial();
                    //First Start TODO

                }
            }
                
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
        result => { Debug.Log("Successfully updated user data"); GetUserData(); },
        error => {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
