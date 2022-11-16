using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LobbyController : Singleton<LobbyController>
{

    
    public GameObject book_noteObj;

    [SerializeField]
    GameObject[] LobbyObj;


    [SerializeField]
    public GameObject WaitPannel;


    private ItemDataBase itemDataBase;
    private List<item> itemList;
    [SerializeField]
    private CharacterSkinGstar testGstarSkin; // 우선 막 코딩.
    [SerializeField]
    private GameObject GstarImageInfo;
    [SerializeField]
    private Image Gstar_img;
    [SerializeField]
    private GameObject GstartNoItemImg;
    protected override void Awake()
    {
        base.Awake();
        int Las = PlayFabData.instance.PlayerStatus[PlayFabData.Stat.LastStage];
        if (Las == 0)
        {
            LobbyObj[0].SetActive(true);
        }
           
        else
            LobbyObj[1].SetActive(true);

        // testPromotain();


        itemDataBase = Resources.Load<ItemDataBase>("ItemDataBase");
        itemList = new List<item>();
        foreach (var item in itemDataBase.Items)
        {
            //Debug.Log(item.ItemCode);
            //RegisterItem(item);
            itemList.Add(item);
        }
    
        GetItemUpdate();
    }
  
    public void OnCouponRegister(GameObject bg)
    {
        string serialCode = "";
        for(int f = 0; f < 3; f++)
        {
            InputField i = bg.transform.GetChild(f).GetComponent<InputField>();
            serialCode += i.text + "-";
        }
        serialCode = serialCode.TrimEnd('-');
        PromotionCode(serialCode);
    }
    void PromotionCode(string serial)
    {
        string couponCode = serial;

        var primaryCatalogName = "GStarCoupon"; // In your game, this should just be a constant matching your primary catalog
        var request = new RedeemCouponRequest
        {
            CatalogVersion = primaryCatalogName,
            CouponCode = couponCode // This comes from player input, in this case, one of the coupon codes generated above
        };
        PlayFabClientAPI.RedeemCoupon(request, LogSuccess => {
            List<ItemInstance> t = LogSuccess.GrantedItems;
            GstarImageInfo.SetActive(true);
            foreach (var item in t)
            {
                foreach (var item2 in itemList)
                {
                    if(item2.ItemCode == item.ItemId)
                    {
                        Gstar_img.sprite = item2.Image;
                    }
                }
            }
            Debug.Log(request.CharacterId);
            PlayFabData.instance.GetUserInventory(()=> { GetItemUpdate(); });
        }, LogFailure => { GstartNoItemImg.SetActive(true); Debug.Log(LogFailure.GenerateErrorReport()); });


    }

    void GetItemUpdate()
    {
        PlayFabData.instance.GetUserInventory(() => {

            List<ItemInstance> itemlist = PlayFabData.instance.userInventory;
            foreach (var item in itemlist)
            {

                foreach (var item2 in itemList)
                {
                    if (item2.ItemCode == item.ItemId)
                        item2.ReportItem(item.ItemInstanceId);
                    testGstarSkin.ChangeSkin();
                }

                Debug.Log(item.ItemId);
                //Debug.Log(item.DisplayName);
                Debug.Log(item.ItemInstanceId);
                //Debug.Log(item.UnitCurrency);
                //Debug.Log(item.UsesIncrementedBy);
            }

        });
        
    }

    #region daily Reward
    const string VC_DR = "DR";


    public void OnReward(DailyRewardBtnEvent DR)
    {

        if (DR.Daily == PlayFabData.instance.PlayerStatus[PlayFabData.Stat.dailyReward] 
            && PlayFabData.instance.PlayerDailyCount > 0)
        {
            WaitPannel.SetActive(true);
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "SubVirtualCurrency",
                FunctionParameter = new { Amount = 1, type = VC_DR },
                GeneratePlayStreamEvent = true

            },
            cloudResult => { SetUserDR(DR); },
            error => { Debug.Log(error.GenerateErrorReport()); });
        }
        else
        {
            WaitPannel.SetActive(false);
        }
    }
 

    void SetUserDR(DailyRewardBtnEvent DR)
    {
        PlayFabData.instance.PlayerStatus[PlayFabData.Stat.dailyReward] += 1;
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"dailyReward",PlayFabData.instance.PlayerStatus[PlayFabData.Stat.dailyReward].ToString() }
        }
        },
        result =>
        {
            Debug.Log("Successfully updated user data");
            if (DR.Gold > 0)
                RewardAdd("DM", DR.Gold);
            if (DR.Gas > 0)
                RewardAdd("ST", DR.Gas);
            if (DR.Ruby > 0)
                RewardAdd("RU", DR.Ruby);

            DR.GetComponent<Image>().color = Color.yellow;
            DR.transform.Find("Complate").gameObject.SetActive(true);
            PlayFabData.instance.PlayerDailyCount = 0;
        },
        error =>
        {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void RewardAdd(string Type, int ammount)
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "AddVirtualCurrency",
            FunctionParameter = new { Amount = ammount, type = Type },
            GeneratePlayStreamEvent = true
            
        },
        cloudResult => {  PlayFabData.instance.GetAccountData(); WaitPannel.SetActive(false); },
        error => { }
        );

    }
    #endregion
}

