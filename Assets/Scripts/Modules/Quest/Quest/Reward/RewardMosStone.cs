using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Quest/Rward/MosStone",fileName ="MosStone")]
public class RewardMosStone : Reward
{
    public override void Give(Quest quest)
    {
        if(quest.RewardMos > 0)
        PlayFabData.Instance.AddAccountData("DM", quest.RewardMos);
        if(quest.RewardRuby > 0)
        PlayFabData.Instance.AddAccountData("RU", quest.RewardMos);
    }

  
}
