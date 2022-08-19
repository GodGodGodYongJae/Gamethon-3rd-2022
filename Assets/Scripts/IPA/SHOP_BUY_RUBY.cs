using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHOP_BUY_RUBY : MonoBehaviour
{
    public int RUBY;
    public int COSTDIA;
    [SerializeField]
    PlayfabStemina displaySt;

    public void OnBuyClick()
    {
        LobbyController.Instance.WaitPannel.SetActive(true);
        if (PlayFabData.Instance.PlayerDiamond >= COSTDIA)
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "SubVirtualCurrency",
                FunctionParameter = new { Amount = COSTDIA, type = "DM" },
                GeneratePlayStreamEvent = true

            },
            cloudResult =>
            {
                PlayFabData.Instance.AddAccountData("RU", RUBY);
                Invoke("SyncData", 1.5f);

            },
            error =>
            {
                Debug.Log("Got error setting user data Ancestor to Arthur");
                Debug.Log(error.GenerateErrorReport());
            });
        }
        else
        {
            LobbyController.Instance.WaitPannel.SetActive(false);
        }
    }

    void SyncData()
    {
        PlayFabData.Instance.GetAccountData();
        displaySt.GetInventory();
        LobbyController.Instance.WaitPannel.SetActive(false);
    }

}
