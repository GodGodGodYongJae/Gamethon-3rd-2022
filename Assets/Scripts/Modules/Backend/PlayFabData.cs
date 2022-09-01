using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PlayFabData : Singleton<PlayFabData>
{
    // Start is called before the first frame update
    public enum Stat { hp, atk, def, cri, dailyReward, LastStage, atklv, deflv, ClearChapter, end };

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

    public void GetUserShowUpgaradeData(UpgradeShow showUpgrade)
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
            showUpgrade.InvokeData();
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    [HideInInspector]
    public bool isQuestLoad;
    [HideInInspector]
    public string QuestJson;
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

            if (result.Data.ContainsKey("questSystem"))
            {
                QuestJson = result.Data["questSystem"].Value;
                isQuestLoad = true;
            }
           
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

    public void GetAccountUpgradeSyncData(UpgradeShow showUpgrade)
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

               showUpgrade.UpgradeSuccess();
           }, (error) =>
           {
               Debug.Log(error.GenerateErrorReport());
           });
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

    Queue<Tuple<string,string>> SaveQueue = new Queue<Tuple<string, string>>();
    public void SetUserData(string id,string value)
    {
        Tuple<string, string> tuple = new Tuple<string, string>(id, value);
        SaveQueue.Enqueue(tuple);  

    }
    bool isSaveRun;
    private void Update()
    {
        if(isSaveRun.Equals(false) && SaveQueue.Count > 0)
        {
            isSaveRun = true;
            SaveRun(SaveQueue.Dequeue());
        }
    }
    private void SaveRun(Tuple<string, string> tuple)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
                {tuple.Item1, tuple.Item2},
                }
        },
          result => {
              Debug.Log("Successfully updated user data");
              if (SaveQueue.Count > 0)
                  SaveRun(SaveQueue.Dequeue());
              else
                  isSaveRun = false;
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
