using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Skill_AttackSpeed : MonoBehaviour,ISkil
{
    [HideInInspector]
    public float atkSpeed = 1.1f;
    Transform parent;
    Player player;

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

        skilList = SkillManager.SkilList.AttackSpeed;
        limitSelect = false;
        skilname = "공격속도증가";
        parent = this.transform.parent;
        player = parent.transform.GetComponent<Player>();
    }


    public void init()
    {
        isInit = true;
        ChangeAtkSpeed();
    }

    void ChangeAtkSpeed()
    {
        player.atkSpeed = atkSpeed;
    }

    public void overlapSelect()
    {
        if(atkSpeed < 2.5f)
        {
            atkSpeed += 0.1f;
            ChangeAtkSpeed();
            if (atkSpeed > 2.5f)
                limitSelect = true;
        }
        //Debug.Log(atkSpeed);


    }
}
