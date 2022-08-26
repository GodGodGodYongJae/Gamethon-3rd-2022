using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabController : MonoBehaviour
{

    [SerializeField]  GetPlayerCombinedInfoRequestParams InfoRequestParams;
    // Start is called before the first frame update
    void Start()
    {

        InfoRequestParams.GetUserData = true; 
        InfoRequestParams.GetTitleData = true; 
        InfoRequestParams.GetCharacterList = true; 


    }
    

    public void OnAccount(bool google)
    {
        Authtypes auth = (google)?Authtypes.Google:Authtypes.Silent;
        PlayFabAuthService.Instance.InfoRequestParams = InfoRequestParams;
        PlayFabAuthService.OnLoginSuccess += PlayFabLogin_OnLoginSuccess;
        PlayFabAuthService.Instance.Authenticate(auth);



    }

    // Update is called once per frame
    //void OnEnable()
    //{
    //    PlayFabAuthService.OnLoginSuccess += PlayFabLogin_OnLoginSuccess;
    //}
    [HideInInspector] public string PlayerName { get; private set; }
    private string myPlayFabId;
    void PlayFabLogin_OnLoginSuccess(LoginResult result)
    {
        myPlayFabId = result.PlayFabId;
        GetUserData();
        //PlayerName = result.InfoResultPayload.UserData["name"].Value;
        Debug.Log("Login Success" + myPlayFabId);
        PlayFabData.Instance.myPlayFabId = myPlayFabId;
        ScenesManager.Instance.OnLobbyScene();
    }

    private void OnDisable()
    {
        PlayFabAuthService.OnLoginSuccess -= PlayFabLogin_OnLoginSuccess;
    }


    void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabId,
            Keys = null
        }, result => {
            Debug.Log("Got user data:");
            if (result.Data == null || !result.Data.ContainsKey("hp")) 
                SetUserData();
            else
            { PlayFabData.Instance.GetUserData(); 
                PlayFabData.Instance.GetAccountData(); }
         
        
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });

    }


    void SetUserData()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"hp", "500"},
            {"cri","0" },
            {"atk", "100"},
            {"def", "10"},
            {"LastStage","0"},
            {"dailyReward","1" },
            {"upgradeAtkLv","1"},
             {"upgradeDefLv","1"}


        }
        },
        result => { Debug.Log("Successfully updated user data"); GetUserData(); },
        error => {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
