using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pe_Skil_DoubleAttack : MonoBehaviour,ISkil
{
    Transform parent;
    Player player;
     float persent = 10f;
    private bool e_limitSelect;
    public bool limitSelect { get { return e_limitSelect; } set { e_limitSelect = value; } }
    
    private bool e_isInit = false;
    public bool isInit { get { return e_isInit; } set { e_isInit = value; } }
    
    private string e_skilname;
    public string skilname { get { return e_skilname; } set { e_skilname = value; } }


    public Sprite e_skilImg;
    public Sprite skilImg { get { return e_skilImg; } }

    private SkillManager.SkilList e_skilList;
    public SkillManager.SkilList skilList { get { return e_skilList; } set { e_skilList = value; } }
    private void Awake()
    {

        skilList = SkillManager.SkilList.DoubleAttack;
        limitSelect = false;
        skilname = "더블 공격!";
    }

    public void init()
    {
        isInit = true;
        parent = this.transform.parent;
        player = parent.transform.GetComponent<Player>();
        //player.AttackEvents.AddListener(DubleAttackEvent);
    }

    private void DubleAttackEvent(Transform arg0)
    {

        float xcount = Random.Range(0, 100);
        if (xcount <= persent)
        {
            //arg0.GetComponent<Enemy>().DamageNum = 2;
        }
    }

    public void overlapSelect()
    {
        if(persent < 100)
        {
            persent += 10f;
            if (persent > 100)
                limitSelect = true;
        }
        
    }


}
