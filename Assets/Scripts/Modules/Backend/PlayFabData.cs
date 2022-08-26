using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PlayFabData : Singleton<PlayFabData>
{
    // Start is called before the first frame update
    public enum Stat {hp,atk,def,cri,dailyReward,LastStage,atklv,deflv,ClearChapter,end };

    [HideInInspector]
   public string myPlayFabId;

    public Dictionary<Stat, int> PlayerStatus = new Dictionary<Stat, int>();
    protected override void Awake()
    {
        base.Awake();
        InitStat();
    }

    void InitStat()
    {
        for (int i = 0; i < (int)Stat.end; i++)
        {
            PlayerStatus.Add((Stat)i, 0);
        }

    }
    void SetStat(Stat stat, int num)
    {
        
        PlayerStatus[stat] = num;
        
    }
    public void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabId,
            Keys = null
        }, result => {
            Debug.Log("Got user data:");
            SetStat(Stat.hp, int.Parse(result.Data["hp"].Value));
            SetStat(Stat.atk, int.Parse(result.Data["atk"].Value));
            SetStat(Stat.def, int.Parse(result.Data["def"].Value));
            SetStat(Stat.cri, int.Parse(result.Data["cri"].Value));
            SetStat(Stat.atklv, int.Parse(result.Data["upgradeAtkLv"].Value));
            SetStat(Stat.deflv, int.Parse(result.Data["upgradeDefLv"].Value));
            SetStat(Stat.dailyReward, int.Parse(result.Data["dailyReward"].Value));
            SetStat(Stat.LastStage, int.Parse(result.Data["LastStage"].Value));
            if (!result.Data.ContainsKey("ClearChapter"))
            {
                SetUserData("ClearChapter", "1");
            }
               
            else
                SetStat(Stat.ClearChapter, int.Parse(result.Data["ClearChapter"].Value));

        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
       
    }




    const  string VC_Ruby = "RU";
    const string VC_Diamond = "DM";
    const string VC_Daily = "DR";

    public int PlayerRuby = 0;
    public int PlayerDiamond = 0;
    public int PlayerDailyCount = 0;

    public int WhatAccountShow(string val)
    {
        int r_val;
        switch (val)
        {
            case VC_Ruby:
                r_val = PlayerRuby;
                break;
            case VC_Diamond:
                r_val = PlayerDiamond;
                break;
            case VC_Daily:
                r_val = PlayerDailyCount;
                break;
            default:
                r_val = 0;
                break;
        }
        return r_val;
    }

    public void GetAccountData()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest()
            , (result) =>
            {
                result.VirtualCurrency.TryGetValue(VC_Diamond, out int Count);
                PlayerDiamond = Count;
                result.VirtualCurrency.TryGetValue(VC_Daily, out int Count2);
                PlayerDailyCount = Count2;
                result.VirtualCurrency.TryGetValue(VC_Ruby, out int Count3);
                PlayerRuby = Count3;

            }, (error) =>
            {
                Debug.Log(error.GenerateErrorReport());
            });
    }

    public void SetUserData(string id,string value)
    {
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>() {
                {id, value},
                }
            },
           result => {
               Debug.Log("Successfully updated user data");
               GetUserData();
           },
           error => {
               Debug.Log("Got error setting user data Ancestor to Arthur");
               Debug.Log(error.GenerateErrorReport());
           });

    }
 
    public void AddAccountData(string ID, int num)
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "AddVirtualCurrency",
            FunctionParameter = new { Amount = num, type = ID },
            GeneratePlayStreamEvent = true
        },
       cloudResult => { Debug.Log(num+"저장성공"); GetAccountData(); },
       error => { }
       );
      
    }



}
