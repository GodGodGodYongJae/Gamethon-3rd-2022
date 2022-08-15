using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public enum SkilList { DoubleAttack,AttackSpeed,Heal,End}
    [System.Serializable]
    public class SkilListDic : SerializableDictionary<SkilList, MonoBehaviour> { };

    public SkilListDic e_SkilList = new SkilListDic();

    public List<ISkil> ShowSkilList()
    {
        List<ISkil> my_skilList = new List<ISkil>();
        foreach (var item in e_SkilList)
        {
            ISkil iskil = item.Value as ISkil;
            if (iskil.limitSelect.Equals(false))
                my_skilList.Add(iskil);
        }
        return my_skilList;

    }
    public void SelectSkil(SkilList skill)
    {
        foreach (var item in e_SkilList)
        {
            if (item.Key == skill)
            {
                AddSkil(item.Value as ISkil);
            }
        }
       
    }

   void AddSkil(ISkil skil)
    {
        if (skil.isInit.Equals(true))
            skil.init();
        else
            skil.overlapSelect();
    }
}
