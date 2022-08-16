using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISkil 
{

    public SkillManager.SkilList skilList { get; set; }
    public string skilname { get; set; }
    public bool limitSelect { get; set; }

    public bool isInit { get; set; }
    Sprite skilImg { get; }

    public void init();
    public void overlapSelect();
}
