using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHOP_GAS : MonoBehaviour
{
    public int ST;
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
                PlayFabData.Instance.AddAccountData("ST", ST);
                Invoke("SyncData", 1.5f);
                LobbyController.Instance.WaitPannel.SetActive(false);
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
