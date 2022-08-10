using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class AcountPlayFab : MonoBehaviour
{
    public InputField EmailInput, PasswordInput, UsernameInput;


    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId)) PlayFabSettings.TitleId = "144";

        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }


    void OnLoginSuccess(LoginResult result)
    {
        print("로그인 성공");
    }


    void OnLoginFailure(PlayFabError error)
    {
        print("로그인 실패");
    }

}
