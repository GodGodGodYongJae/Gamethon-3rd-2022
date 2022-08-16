using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Skill_Meteo : MonoBehaviour,ISkil
{
    Transform parent;
    Player player;

    private int MeteoNum;

    private bool e_limitSelect;
    public bool limitSelect { get { return e_limitSelect; } set { e_limitSelect = value; } }

    private bool e_isInit = false;
    public bool isInit { get { return e_isInit; } set { e_isInit = value; } }

    private string e_skilname;
    public string skilname { get { return e_skilname; } set { e_skilname = value; } }

    private SkillManager.SkilList e_skilList;
    public SkillManager.SkilList skilList { get { return e_skilList; } set { e_skilList = value; } }

    public Sprite e_skilImg;
    public Sprite skilImg { get { return e_skilImg; } }

    private void Awake()
    {
        skilList = SkillManager.SkilList.Meteo;
        limitSelect = false;
        skilname = "╦чев©ю";
        MeteoNum = 0;
        parent = this.transform.parent;
        player = parent.transform.GetComponent<Player>();
        //InvokeRepeating("MeteoReapeat", 0, 5);
        //InvokeRepeating("MeteoReapeat", 0, 2);
    }
    public void init()
    {
        isInit = true;
        CreateMeteor();
    }

    void CreateMeteor()
    {
        MeteoNum++;
        InvokeRepeating("MeteoReapeat", 3, 10);
    }
    public void overlapSelect()
    {
        if(MeteoNum < 5)
        {
            CreateMeteor();
            if (MeteoNum > 5)
                limitSelect = true;
        }
        
    }

     void MeteoReapeat()
    {
        if(player.isDeath.Equals(false))
        {
            Vector3 randPos = new Vector3(Random.Range(-20,20), 0, Random.Range(-20,20));
            ObjectPoolManager.Instance.Get("Meteor 2",randPos,Quaternion.identity);   
        }

    }

}
