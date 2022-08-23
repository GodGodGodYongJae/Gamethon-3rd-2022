using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeShow : MonoBehaviour
{
    public PlayFabData.Stat levelstat;
    [SerializeField]
    TextMeshProUGUI[] NextText;
    [SerializeField]
    TextMeshProUGUI CostDMText;
    [SerializeField]
    TextMeshProUGUI LevelText;
    [SerializeField]
    TextMeshProUGUI CostRUText;
    [SerializeField]
    ReadCSV_UPGRADE ReadCsv;

    [SerializeField]
    int[] NextIncrease = new int[2];

    // Start is called before the first frame update

    int CostDia = 0;
    int CostRuby = 0;
    int upgradeAtk = 0;
    int upgradeDef = 0;
    int upgradeHp = 0;
    int upgradeCri = 0;
    int defLv = 0;
    int atkLv = 0;
    private void OnEnable()
    {
        if (levelstat == PlayFabData.Stat.atklv)
            AtkShowData();
        else if (levelstat == PlayFabData.Stat.deflv)
            DefShowData();
    }

    void DefShowData()
    {
        foreach (var item in ReadCsv.upgradList)
        {
            if (item.Level == PlayFabData.Instance.PlayerStatus[levelstat])
            {
                for (int i = 0; i < NextText.Length; i++)
                {
                    NextIncrease[i] = item.increase[i];
                }
                upgradeAtk = PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.atk];
                upgradeCri = PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.cri];
                upgradeDef = NextIncrease[0] + PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.def];
                upgradeHp = NextIncrease[1] + PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.hp];
                NextText[0].text = upgradeDef.ToString();
                NextText[1].text = upgradeHp.ToString();

                atkLv = PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.atklv];
                defLv = PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.deflv];
                CostDia = (int)item.DM;
                CostRuby = (int)item.Ruby;
                CostDMText.text = CostDia.ToString();
                CostRUText.text = CostRuby.ToString();
                LevelText.text = item.Level.ToString();
                break;
            }
        }
    }
    void AtkShowData()
    {
        // 0 : atk 1 : cri , 0 : def 1 : hp
            foreach (var item in ReadCsv.upgradList)
            {
                if (item.Level == PlayFabData.Instance.PlayerStatus[levelstat])
                {
                    for (int i = 0; i < NextText.Length; i++)
                    {
                        NextIncrease[i] = item.increase[i];
                    }

                        upgradeAtk = NextIncrease[0] + PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.atk];
                        upgradeCri = NextIncrease[1] + PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.cri];
                        upgradeDef = PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.def];
                        upgradeHp = PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.hp];
                        NextText[0].text = upgradeAtk.ToString();
                        NextText[1].text = upgradeCri.ToString();

                atkLv = PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.atklv];
                defLv = PlayFabData.Instance.PlayerStatus[PlayFabData.Stat.deflv];
                CostDia = (int)item.DM;
                        CostRuby = (int)item.Ruby;
                        CostDMText.text = CostDia.ToString();
                        CostRUText.text = CostRuby.ToString();
                LevelText.text = item.Level.ToString();
                break;
                }
            }
        
    }

    public void OnUpgradeBtn()
    {
        LobbyController.Instance.WaitPannel.SetActive(true);
        if(CostDia <= PlayFabData.Instance.PlayerDiamond && 
            CostRuby <= PlayFabData.Instance.PlayerRuby)
        {
            if (levelstat == PlayFabData.Stat.atklv)
                atkLv++;
            else if (levelstat == PlayFabData.Stat.deflv)
                defLv++;

            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "SubVirtualCurrency",
                FunctionParameter = new { Amount = CostDia, type = "DM" },
                GeneratePlayStreamEvent = true

            },
            cloudResult => {
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "SubVirtualCurrency",
                    FunctionParameter = new { Amount = CostRuby, type = "RU" },
                    GeneratePlayStreamEvent = true

                },
                cloudResult => {
                    PlayFabData.Instance.GetAccountData();
                    UpgradeSuccess();

                },
                error => { Debug.Log(error.GenerateErrorReport()); });
 
            },
            error => { Debug.Log(error.GenerateErrorReport()); });
        }
        else LobbyController.Instance.WaitPannel.SetActive(false);
    }

    void UpgradeSuccess()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"hp", upgradeHp.ToString()},
            {"cri", upgradeCri.ToString() },
            {"atk", upgradeAtk.ToString()},
            {"def", upgradeDef.ToString()},
            {"upgradeAtkLv",atkLv.ToString()},
             {"upgradeDefLv",defLv.ToString()}


        }
        },
       result => { Debug.Log("Successfully updated user data"); 
           PlayFabData.Instance.GetUserData();
           Invoke("InvokeData", 1.5f);
       },
       error => {
           Debug.Log("Got error setting user data Ancestor to Arthur");
           Debug.Log(error.GenerateErrorReport());
       });
    }

    void InvokeData()
    {
        if (levelstat == PlayFabData.Stat.atklv) AtkShowData();
        else if (levelstat == PlayFabData.Stat.deflv) DefShowData();
        LobbyController.Instance.WaitPannel.SetActive(false);
    }
}
