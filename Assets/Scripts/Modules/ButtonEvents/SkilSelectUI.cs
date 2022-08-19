using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilSelectUI : MonoBehaviour
{

    [SerializeField]
    SkillManager skilManager;
    [SerializeField]
    Button[] skilBtn; 

    private List<ISkil> ISkilList = new List<ISkil>();

    private Dictionary<Button, ISkil> skilDic = new Dictionary<Button, ISkil>();
    // Start is called before the first frame update
    void OnEnable()
    {
        RerollSkill();

        //if(skilBtn.Length <= SkilRange)
        //{
            
        //}
 
    }

    public void OnRerollSkill()
    {
        if(PlayFabData.Instance.PlayerDiamond >= 100)
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "SubVirtualCurrency",
                FunctionParameter = new { Amount = 100, type = "DM" },
                GeneratePlayStreamEvent = true

            },
           cloudResult =>
           {
               RerollSkill();

           },
           error =>
           {
               Debug.Log("Got error setting user data Ancestor to Arthur");
               Debug.Log(error.GenerateErrorReport());
           });
        }
    }
     void RerollSkill()
    {
        ISkilList = skilManager.ShowSkilList();
        skilDic.Clear();
        for (int i = 0; i < skilBtn.Length; i++)
        {
            int RandSkil = Random.Range(0, ISkilList.Count);
            if (ISkilList[RandSkil].skilImg != null)
                skilBtn[i].image.sprite = ISkilList[RandSkil].skilImg;
            skilBtn[i].transform.GetChild(0).GetComponent<Text>().text = ISkilList[RandSkil].skilname;

            skilDic.Add(skilBtn[i], ISkilList[RandSkil]);
            ISkilList.RemoveAt(RandSkil);
        }
    }
    public void OnSelectSkil(Button btn)
    {
        foreach (var item in skilDic)
        {
            if(item.Key == btn)
            {
                skilManager.SelectSkil(item.Value.skilList);
            }
        }
    }


}
