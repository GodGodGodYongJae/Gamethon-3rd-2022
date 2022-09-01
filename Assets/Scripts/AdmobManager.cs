using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using UnityEngine.EventSystems;

public class AdmobManager : MonoBehaviour
{
    public bool isTestMode;

    //public Button RewardAdsBtn;


    void Start()
    {
        var requestConfiguration = new RequestConfiguration
           .Builder()
           .SetTestDeviceIds(new List<string>() { "5F052FA6E7A86FBF" }) // test Device ID
           .build();

        MobileAds.SetRequestConfiguration(requestConfiguration);
        //rewardAd = new RewardedAd(isTestMode ? rewardTestID : rewardID);
        //rewardAd.LoadAd(GetAdRequest());
        LoadRewardAd();
    }

    void Update()
    {
        //RewardAdsBtn.interactable = rewardAd.IsLoaded();
        if (Rewardsuccess.Equals(true))
        {
            if(reward != null)
                reward.Action?.Invoke();
            Rewardsuccess = false;
        }
            
    }

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }




    #region 리워드 광고
    const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    const string rewardID = "ca-app-pub-8904224703245079/3707337105";
    RewardedAd rewardAd;

    GameObject clickObj;
    AdmobReward reward;
    bool Rewardsuccess;
    void LoadRewardAd()
    {


        rewardAd = new RewardedAd(isTestMode ? rewardTestID : rewardID);
        rewardAd.LoadAd(GetAdRequest());
        rewardAd.OnUserEarnedReward += (sender, e) =>
        {
            Rewardsuccess = true;

            //LogText.text = "리워드 광고 성공";
        };
    }

    public void ShowRewardAd()
    {
        clickObj = EventSystem.current.currentSelectedGameObject;
        reward = clickObj.GetComponent<AdmobReward>();

        rewardAd.Show();
        LoadRewardAd();
    }
    #endregion
}
