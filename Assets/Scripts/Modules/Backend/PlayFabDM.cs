using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayFabDM : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI DMText;
    const string VC_DM = "DM";
    public int CurrentDM = 0;
    private void Awake()
    {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            GetDR();
        }
    }
   public void GetDR()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest()
        , (result) =>
        {

            result.VirtualCurrency.TryGetValue(VC_DM, out int _DailyReward);
            CurrentDM = _DailyReward;
            DMText.text = CurrentDM.ToString();

        }, (error) =>
        {
            Debug.Log(error.GenerateErrorReport());
        });

    }
  
}
