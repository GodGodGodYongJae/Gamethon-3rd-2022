using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayfabStemina : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI staminaText;
    [SerializeField] TextMeshProUGUI rechargeText; 
    DateTime nextFreeTicket = new DateTime(); 
    bool isStaminaCapped;                    
    const string VC_Gas = "ST";
    private int AddGas;
    private int CurrentGas;
    // Update is called once per frame
    void Update()
    {
        if(PlayFabClientAPI.IsClientLoggedIn())
        {
            if(isStaminaCapped == false)
            {
                if(nextFreeTicket.Subtract(DateTime.Now).TotalSeconds <=0)
                {
                    rechargeText.text = "Waiting...";
                    GetInventory();
                }
                else
                {
                    var rechargeTime = nextFreeTicket.Subtract(DateTime.Now);
                    rechargeText.text = string.Format("{0:00}:{1:00}", rechargeTime.Minutes, rechargeTime.Seconds);
                }
            }
        }
    }

    public void OnGameStart()
    {
        if(CurrentGas > 0)
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "SubVirtualCurrency",
                FunctionParameter = new { Amount = 1, type = VC_Gas },
                GeneratePlayStreamEvent = true
            },
        cloudResult => { ScenesManager.Instance.OnTitleScene(); },
        error => { });
          
        }
    }
    public void OnTestClick()
    {
        AddGas = 1;
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "AddVirtualCurrency",
            FunctionParameter = new { Amount = AddGas,type = VC_Gas },
            GeneratePlayStreamEvent = true
        },
        cloudResult => { GetInventory(); },
        error => { }
        ); ; ;
    }
    void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest()
        , (result) =>
        {
           
            result.VirtualCurrency.TryGetValue(VC_Gas, out int staminaBalance);
            CurrentGas = staminaBalance;

            if (result.VirtualCurrencyRechargeTimes.TryGetValue(VC_Gas, out VirtualCurrencyRechargeTime rechargeDetails))
            {
                staminaText.text = string.Format("{0}/{1}", staminaBalance.ToString(), rechargeDetails.RechargeMax.ToString());

            if (staminaBalance < rechargeDetails.RechargeMax)
                {
                    nextFreeTicket = DateTime.Now.AddSeconds(rechargeDetails.SecondsToRecharge);
                    var rechargeTime = nextFreeTicket.Subtract(DateTime.Now);

                    rechargeText.text = string.Format("{0:00}:{1:00}", rechargeTime.Minutes, rechargeTime.Seconds);
                    isStaminaCapped = false;
                }
                else
                {
                    rechargeText.text = string.Empty;
                    isStaminaCapped = true;
                }
            }
        }, (error) =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
