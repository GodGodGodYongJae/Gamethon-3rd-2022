using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using GooglePlayGames;


public class PlayFabManager : MonoBehaviour
{
    public Text LogText;


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
            if (success) { LogText.text = "구글 로그인 성공"; PlayFabLogin(); }
            else LogText.text = "구글 로그인 실패";
        });
    }

    public void GoogleLogout()
    {
        LogText.text = "구글 로그아웃";
    }

    public void PlayFabLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = Social.localUser.id + "@rand.com", Password = Social.localUser.id };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => LogText.text = "플레이팹 로그인 성공\n" + Social.localUser.userName, (error) => PlayFabRegister());
    }

    public void PlayFabRegister()
    {
        var request = new RegisterPlayFabUserRequest { Email = Social.localUser.id + "@rand.com", Password = Social.localUser.id, Username = Social.localUser.userName };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => { LogText.text = "플레이팹 회원가입 성공"; PlayFabLogin(); }, (error) => LogText.text = "플레이팹 회원가입 실패");
    }
}
