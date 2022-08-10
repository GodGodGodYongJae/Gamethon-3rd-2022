using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using GooglePlayGames;
using TMPro;

public class PlayFabManager : MonoBehaviour
{
    public TextMeshProUGUI LogText;
    public GameObject AcountInfo;
    private bool isLogin;

    void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        GoogleLogin();
    }

    public void GoogleLogin()
    {
        Social.localUser.Authenticate((success) =>
        {
            if (success) { LogText.text = "Push To Start!"; PlayFabLogin(); isLogin = true; }
            else { LogText.text = "Faild Login!"; isLogin = true; AcountInfo.SetActive(true); OnTouchStart(); }
            
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
        if(isLogin)
        ScenesManager.Instance.OnLobbyScene();
    }
}
