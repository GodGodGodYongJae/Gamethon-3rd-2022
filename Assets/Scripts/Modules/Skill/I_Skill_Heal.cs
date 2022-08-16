using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Skill_Heal : MonoBehaviour,ISkil
{

    public float HealPersent = 30;

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
        skilList = SkillManager.SkilList.Heal;
        limitSelect = false;
        skilname = "응급처치";
        parent = this.transform.parent;
        player = parent.transform.GetComponent<Player>();

    }
    public void init()
    {
        isInit = true;
        HealPlayer();
    }

    void HealPlayer()
    {
        player.Heal((int)(player.maxHealth * (HealPersent * 0.1f)));
 
    }
    public void overlapSelect()
    {
        HealPlayer();
    }


}
