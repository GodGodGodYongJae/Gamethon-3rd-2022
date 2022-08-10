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
            if (success) { LogText.text = "���� �α��� ����"; PlayFabLogin(); }
            else LogText.text = "���� �α��� ����";
        });
    }

    public void GoogleLogout()
    {
        LogText.text = "���� �α׾ƿ�";
    }

    public void PlayFabLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = Social.localUser.id + "@rand.com", Password = Social.localUser.id };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => LogText.text = "�÷����� �α��� ����\n" + Social.localUser.userName, (error) => PlayFabRegister());
    }

    public void PlayFabRegister()
    {
        var request = new RegisterPlayFabUserRequest { Email = Social.localUser.id + "@rand.com", Password = Social.localUser.id, Username = Social.localUser.userName };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => { LogText.text = "�÷����� ȸ������ ����"; PlayFabLogin(); }, (error) => LogText.text = "�÷����� ȸ������ ����");
    }
}
