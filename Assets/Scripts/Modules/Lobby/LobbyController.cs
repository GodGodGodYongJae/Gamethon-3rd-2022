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
    #region server
    private void Awake()
    {
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
